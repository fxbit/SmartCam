//#define USE_KALMAN2D

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FxMaths.GUI;
using FxMaths.Vector;
using FxMaths.Matrix;
using FxMaths.Utils;
using System.Threading;
using SmartCam;


namespace MainForm
{

    public class PeopleSimulation
    {
        private int _peopleNum = 0;
        public int PeopleNum { get { return _peopleNum; } }

        public FxVector2f EntrancePosition { get; set; }
        public FxVector2f EntranceDirection { get; set; }

        public List<Person> PersonList;


        private Boolean simulationRun = false;
        private Random rand;
        private Thread simulationThread = null;
        private FxMatrixF map = null;
        private FxMatrixMask mapMask = null;

        public delegate void PeopleSimulationRefresh(PeopleSimulation sim);
        private PeopleSimulationRefresh PsrList;

        /// <summary>
        /// Init simulation with specific number of runtime people.
        /// </summary>
        /// <param name="PeopleNum"></param>
        public PeopleSimulation(int PeopleNum, FxVector2f entrancePosition, FxVector2f entranceDirection, FxMatrixF im)
        {
            _peopleNum = PeopleNum;
            EntrancePosition = entrancePosition;
            EntranceDirection = entranceDirection;

            // init the blob list that contain the people
            PersonList = new List<Person>();

            // init random generator
            rand = new Random();

            // set the map mask
            map = im;
            mapMask = im > 0.8f;
        }

        private void SimulationRun()
        {
            int dt_ms = 50;
            int newPersonCounter = 0;
            FxVector2f filded = new FxVector2f();
            while (simulationRun)
            {
                // if we can add people add them
                if (PersonList.Count < _peopleNum)
                {
                    newPersonCounter++;
                    if (newPersonCounter > 10)
                    {
                        int numOfNewPersons = rand.Next(_peopleNum / 10) + 1;
                        for (int i = 0; i < numOfNewPersons; i++)
                            PersonList.Add(new Person(EntrancePosition, EntranceDirection, 4.0f * rand.Next(1000) / 1000.0f + 1.0f));

                        newPersonCounter = 0;
                    }
                }

                // move the blobs in random directions
                foreach (var person in PersonList)
                {
                    float speedChange = 1 + (rand.Next(100) > 50 ? -0.1f : +0.1f) * rand.Next(1000) / 1000.0f;
                    float directionAngleChange = (rand.Next(100) > 50 ? -1 : +1) * (float)(rand.NextDouble() * Math.PI/8.0f);

                    // move person
                    // check the person position and moving
                    FxVector2f nextPosition = person.Position + person.Speed * person.Direction;
                    float value = map[person.Position];

                    if (nextPosition.x > 0 && nextPosition.y > 0 && nextPosition.x < map.Width && nextPosition.y < map.Height)
                    {
                        float valueNext = map[nextPosition];
                        if (valueNext > 0.9f)
                        {
                            // calculate next position
                            person.Path.Add(nextPosition);

                            // calculate the position with kalman to pe more smooth the transaction
#if USE_KALMAN2D
                            filded.x = person.kalmanX.Update(nextPosition.x,  person.Speed, dt_ms);
                            filded.y = person.kalmanY.Update(nextPosition.y, person.Speed, dt_ms);
#else
                            filded.x = person.kalmanX.Update(nextPosition.x, dt_ms);
                            filded.y = person.kalmanY.Update(nextPosition.y, dt_ms);
#endif
                            person.Position = filded;
                            person.PathKalman.Add(filded);
                        }
                        else
                            directionAngleChange = (rand.Next(100) > 50 ? -1 : +1) * (float)(rand.NextDouble() * Math.PI);
                    }
                    else
                        directionAngleChange = (rand.Next(100) > 50 ? -1 : +1) * (float)(rand.NextDouble() * Math.PI);

                    // update the speed
                    person.Speed *= speedChange;

                    // limit the max speed
                    if (person.Speed > 4f)
                        person.Speed = 4;

                    // rotate the direction
                    person.Direction.Rotation(directionAngleChange);
                }

                // call all upper layers about the refresh states
                PsrList(this);

                // delay the next frame
                Thread.Sleep(dt_ms);
            }
        }


        public void Start(PeopleSimulationRefresh psr)
        {
            if (simulationThread == null)
            {
                // add the callback event
                PsrList += psr;

                // clean the people list
                PersonList.Clear();

                // start the thread
                simulationThread = new Thread(new ThreadStart(SimulationRun));
                simulationRun = true;
                simulationThread.Start();
            }
        }

        public void Stop()
        {
            if(simulationThread!=null)
            {
                simulationRun = false;
                simulationThread.Abort();
                simulationThread.Join();
            }
        }
    }
}
