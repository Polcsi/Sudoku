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
        public static void WriteSudoku(int count, string[] table, string name)
        {
            using(StreamWriter fs = new StreamWriter($"{name}.txt", false))
            {

                string border = "+";
                for (int i = 0; i < count / 3; ++i)
                {
                    border += "-----+";
                }
                for (int i = 0; i < table.Length; ++i)
                {
                    if (i == 0) { fs.WriteLine(border); }
                    fs.Write("|" + table[i]);
                    if ((i + 1) % count == 0)
                    {
                        fs.WriteLine("|");
                    }
                    if(i == ((count * ((count / 3) - 1)) + (count - 1)) || i == ((count * ((count / 3) + ((count / 3) - 1))) + (count - 1)))
                    {
                        fs.WriteLine(border);
                    }
                    if (i == table.Length - 1) { fs.WriteLine(border); }
                }
            }
        }
        public static string stringify(int count)
        {
            return string.Format($"{count}x{count}");
        }
    }
}
