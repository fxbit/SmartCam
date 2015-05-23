using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleForm
{
    public static partial class Extensions
    {
        //---------------------------------------------------------------------

        public static void Invoke(this Control control, Action action)
        {
            if (control.InvokeRequired) control.Invoke(new MethodInvoker(action), null);
            else action.Invoke();
        }

        //---------------------------------------------------------------------

    }
}
