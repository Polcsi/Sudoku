using System;
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
        public static void WriteSudoku(Settings settings, string name)
        {
            using(StreamWriter fs = new StreamWriter($"{name}.txt", false))
            {

                string border = "+";
                for (int i = 0; i < settings.Count / 3; ++i)
                {
                    border += "-----+";
                }
                for (int i = 0; i < settings.Table.Length; ++i)
                {
                    if (i == 0) { fs.WriteLine(border); }
                    fs.Write("|" + settings.Table[i]);
                    if ((i + 1) % settings.Count == 0)
                    {
                        fs.WriteLine("|");
                    }
                    if(i == ((settings.Count * ((settings.Count / 3) - 1)) + (settings.Count - 1)) || i == ((settings.Count * ((settings.Count / 3) + ((settings.Count / 3) - 1))) + (settings.Count - 1)))
                    {
                        fs.WriteLine(border);
                    }
                    if (i == settings.Table.Length - 1) { fs.WriteLine(border); }
                }
            }
        }
    }
}
