using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Sudoku
{
    class Actions
    {
        public static void DelayAction(int millisecond, Action action)
        {
            Thread.Sleep(millisecond);
            action.Invoke();
        }
        public static void WriteSudoku(Settings settings)
        {
            using(StreamWriter fs = new StreamWriter("sudoku.txt", false))
            {
                for (int i = 0; i < settings.Table.Length; ++i)
                {
                    fs.Write("|" + settings.Table[i]);
                    if ((i + 1) % settings.Count == 0)
                    {
                        fs.WriteLine("|");
                    }
                }
            }
        }
    }
}
