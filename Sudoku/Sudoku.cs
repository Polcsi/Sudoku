using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    class Sudoku
    {
        private static Random rnd = new Random();
        private Settings Settings { get; set; }
        public Sudoku(int table, int count)
        {
            Settings settings = new Settings();
            settings.Table = new string[table];
            settings.Count = count;
            settings.Characters = new string[count];
            for (int i = 0; i < count; ++i)
            {
                settings.Characters[i] = $"{i + 1}"; 
            }
            Settings = settings;
            fillTable();
        }
        private void fillTable()
        {
            for (int i = 0; i < Settings.Count; ++i)
            {
                while (!checkRow(i * Settings.Count))
                {
                    if (i < 1)
                    {
                        for (int j = 0; j < Settings.Count; ++j)
                        {
                            Settings.Table[j + (i * Settings.Count)] = Settings.Characters[rnd.Next(Settings.Characters.Length)];
                        }
                    }
                    else
                    {
                        string[] aboveChars;
                        for (int j = 0; j < Settings.Count; ++j)
                        {
                            aboveChars = new string[(i * Settings.Count) /  Settings.Count];
                            for (int k = 1; k < aboveChars.Length + 1; ++k)
                            {
                                aboveChars[k - 1] = Settings.Table[(j + (i *  Settings.Count) - (k *  Settings.Count))];
                                //Console.Write($"ABOVE ELEMENT: {Settings.Table[(j + (i *  Settings.Count) - (k *  Settings.Count))]} INDEX: {(j + (i *  Settings.Count) - (k *  Settings.Count))} |k: {k}.| ACTUAL INDEX: {j + (i *  Settings.Count)}  ");
                            }
                            //Console.WriteLine();
                            string randomChar = Settings.Characters[rnd.Next(Settings.Characters.Length)];
                            while (aboveChars.Contains(randomChar))
                            {
                                randomChar = Settings.Characters[rnd.Next(Settings.Characters.Length)];
                            }
                            Settings.Table[j + (i *  Settings.Count)] = randomChar;
                        }
                    }
                }
                Actions.DelayAction(50, new Action(() => { draw(i *  Settings.Count); }));
            }
        }
        private void draw(int row)
        {
            for (int i = 0; i <  Settings.Count; ++i)
            {
                Actions.DelayAction(0, new Action(() => { Console.Write("|" + Settings.Table[i + row]); }));
                if ((i + 1) %  Settings.Count == 0)
                {
                    Console.WriteLine("|");
                }
            }
        }
        private bool checkRow(int multiple)
        {
            List<string> values = new List<string>();
            for (int i = 0; i <  Settings.Count; ++i)
            {
                if (!values.Contains(Settings.Table[i + multiple]))
                {
                    values.Add(Settings.Table[i + multiple]);
                }
            }
            return values.Count() ==  Settings.Count ? true : false;
        }
        private bool checkColumn(int multiple)
        {
            List<string> values = new List<string>();
            for (int i = 0; i <  Settings.Count; ++i)
            {
                if (!values.Contains(Settings.Table[i *  Settings.Count + multiple]))
                {
                    values.Add(Settings.Table[i *  Settings.Count + multiple]);
                }
            }
            return values.Count() ==  Settings.Count ? true : false;
        }
        public bool checkSquare(int multiple)
        {
            List<string> values = new List<string>(6);
            for (int i = 0; i < Settings.Count; ++i)
            {
                if (!values.Contains(Settings.Table[i]) && i <= 2)
                {
                    values.Add(Settings.Table[i]);
                }
                else if (!values.Contains(Settings.Table[i + (3 * multiple)]) && i > 2)
                {
                    values.Add(Settings.Table[i + (3 * multiple)]);
                    //Console.WriteLine(Settings.Table[i + (3 * multiple)]);
                }
                Console.WriteLine(Settings.Table[i + (3 * multiple)]);
            }

            //if (!values.Contains(Settings.Table[0]))
            //{
            //    values.Add(Settings.Table[0]);
            //}
            //if (!values.Contains(Settings.Table[1]))
            //{
            //    values.Add(Settings.Table[1]);
            //}
            //if (!values.Contains(Settings.Table[2]))
            //{
            //    values.Add(Settings.Table[2]);
            //}
            //if (!values.Contains(Settings.Table[6]))
            //{
            //    values.Add(Settings.Table[6]);
            //}
            //if (!values.Contains(Settings.Table[7]))
            //{
            //    values.Add(Settings.Table[7]);
            //}
            //if (!values.Contains(Settings.Table[8]))
            //{
            //    values.Add(Settings.Table[8]);
            //}

            return values.Count() ==  Settings.Count ? true : false;
        }
    }
}
