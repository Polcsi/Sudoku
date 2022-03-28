using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sudoku
{
    class Actions
    {
        public static void DelayAction(int millisecond, Action action)
        {
            Thread.Sleep(millisecond);
            action.Invoke();
        }
    }
}
