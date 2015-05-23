//#define USE_KALMAN2D

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FxMaths.Vector;
using FxMaths.Utils;
using FxMaths.Matrix;
using System.Diagnostics;

namespace SmartCam
{
    public class Person
    {
        #region Properties
        public FxVector2f Direction;
        public FxVector2f Position;
        public float Speed;
        public List<FxVector2f> Path;
        public List<FxVector2f> PathKalman;

        public FxVector2f Target;

        public Stopwatch waitTime;
        public long waitTimeMs;
        public Boolean waitInTarget = false;

#if USE_KALMAN2D
        public FxKalman2D kalmanX;
        public FxKalman2D kalmanY;
#else
        public FxKalman1D kalmanX;
        public FxKalman1D kalmanY;
#endif

        #endregion


        

        #region Constructor
        public Person()
        {
            Target = new FxVector2f();
            Position = new FxVector2f(); Direction = new FxVector2f(); Speed = 0.0f;
            Path = new List<FxVector2f>();
            PathKalman = new List<FxVector2f>();
#if USE_KALMAN2D
            kalmanX = new FxKalman2D(0.01f, 0.01f, 0.1f, 200, 0);
            kalmanY = new FxKalman2D(0.01f, 0.01f, 0.1f, 200, 0);
#else
            kalmanX = new FxKalman1D(0.01f, 0.01f, 0.1f, 200, 0);
            kalmanY = new FxKalman1D(0.01f, 0.01f, 0.1f, 200, 0);
#endif
        }

        public Person(FxVector2f Position, FxVector2f Direction, float Speed)
        {
            this.Position = Position; this.Direction = Direction; this.Speed = Speed;
            this.Target = new FxVector2f(0,0);
            this.Direction.Normalize();
            Path = new List<FxVector2f>();
            PathKalman = new List<FxVector2f>();
#if USE_KALMAN2D
            kalmanX = new FxKalman2D(0.01f, 0.1f, 0.1f, 200, Position.x);
            kalmanY = new FxKalman2D(0.01f, 0.1f, 0.1f, 200, Position.y);
#else
            kalmanX = new FxKalman1D(0.01f, 0.0001f, 0.1f, 200, Position.x);
            kalmanY = new FxKalman1D(0.01f, 0.0001f, 0.1f, 200, Position.y);
#endif
        }
        #endregion
    }
}
