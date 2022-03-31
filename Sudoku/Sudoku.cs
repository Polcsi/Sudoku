using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    class Sudoku : Actions
    {
        private static readonly Random rnd = new Random();
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
        }
        public void fillTable()
        {
            Settings.Table = new string[Settings.Count * Settings.Count];
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
                        string[] aboveColumnChars;
                        for (int j = 0; j < Settings.Count; ++j)
                        {
                            aboveColumnChars = new string[(i * Settings.Count) /  Settings.Count];
                            for (int k = 1; k < aboveColumnChars.Length + 1; ++k)
                            {
                                aboveColumnChars[k - 1] = Settings.Table[(j + (i *  Settings.Count) - (k *  Settings.Count))];
                                //Console.Write($"ABOVE ELEMENT: {Settings.Table[(j + (i *  Settings.Count) - (k *  Settings.Count))]} INDEX: {(j + (i *  Settings.Count) - (k *  Settings.Count))} |k: {k}.| ACTUAL INDEX: {j + (i *  Settings.Count)}  ");
                            }
                            //Console.WriteLine();
                            
                            string randomChar = Settings.Characters[rnd.Next(Settings.Characters.Length)];
                            //string randomChar = missing.Count != 0 ? missing[0] : Settings.Characters[rnd.Next(Settings.Characters.Length)];
                            int avoidInfiniteLoop = 0;
                            while (aboveColumnChars.Contains(randomChar) && avoidInfiniteLoop < 14)
                            { 
                                //Console.Write($"|{randomChar}|");
                                //Console.Write($" {avoidInfiniteLoop} ");
                                randomChar = Settings.Characters[rnd.Next(Settings.Characters.Length)];
                                avoidInfiniteLoop++;
                            }
                            
                            Settings.Table[j + (i *  Settings.Count)] = randomChar;
                        }
                    }
                }

                List<string> doubled = doubleValues(i);
                List<string> missing = missingValues(i);

                if (doubled.Count > 0)
                {
                    for (int j = 0; j < doubled.Count; ++j)
                    {
                        //Console.Write($"Range: {i * 6}-{i * 6 + 2} ");
                        //foreach (var item in missing)
                        //{
                        //    Console.Write(item + " ");
                        //}
                        //foreach (var item in doubled)
                        //{
                        //    Console.Write(item + " ");
                        //}
                        //Console.WriteLine();
                        for (int k = i * Settings.Count; k < i * Settings.Count + Settings.Count / 2; ++k)
                        {
                            //Console.WriteLine(Settings.Table[k] + " " + missing[j]);
                            //Console.WriteLine($"{missing[j]} contains {Settings.Table[k]} | {missing[j].Contains(Settings.Table[k])}");
                            if (missing[j].Contains(Settings.Table[k]))
                            {
                                //Console.WriteLine($"SWAP: {Settings.Table[k]} to {doubled[j]} at {k} i={i}");
                                Settings.Table[k] = doubled[j];
                            }
                        }
                    }
                    for (int j = 0; j < doubled.Count; ++j)
                    {
                        //Console.Write($"Range: {i * 6 + 3}-{i * 6 + 5} ");
                        //foreach (var item in doubled)
                        //{
                        //    Console.Write(item + " ");
                        //}
                        //foreach (var item in missing)
                        //{
                        //    Console.Write(item + " ");
                        //}
                        //Console.WriteLine();
                        for (int k = i * Settings.Count + Settings.Count / 2; k < i * Settings.Count + Settings.Count; ++k)
                        {
                            //Console.WriteLine(Settings.Table[k] + " " + doubled[j]);
                            //Console.WriteLine($"{doubled[j]} contains {Settings.Table[k]} | {doubled[j].Contains(Settings.Table[k])}");
                            if (doubled[j].Contains(Settings.Table[k]))
                            {
                                //Console.WriteLine($"SWAP: {Settings.Table[k]} to {missing[j]} at {k} i={i}");
                                Settings.Table[k] = missing[j];
                            }
                        }
                    }
                }
                Actions.DelayAction(70, new Action(() => { draw(i *  Settings.Count); }));
            }
            Actions.WriteSudoku(Settings);
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
            return values.Count() ==  Settings.Count;
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
            return values.Count() ==  Settings.Count;
        }
        private bool checkSquare(int multiple)
        {
            int startingIndex = 0;
            switch (multiple % (Settings.Count / 3))
            {
                case 0:
                    startingIndex = multiple * (Settings.Count * (Settings.Count / 3)) / (Settings.Count / 3);
                    break;
                case 1:
                    startingIndex = ((multiple - 1) * (Settings.Count * (Settings.Count / 3)) / (Settings.Count / 3)) + 3;
                    break;
                case 2:
                    if (multiple > (Settings.Count / 3))
                    {
                        startingIndex = ((Settings.Count / 3) * 2) + Settings.Count * (multiple - 2);
                    } else
                    {
                        startingIndex = (Settings.Count / 3) * multiple;
                    }
                    break;
                default:
                    break;
            }
            List<string> values = new List<string>();

            for (int i = 0; i < Settings.Count / 3; i++)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (!values.Contains(Settings.Table[startingIndex]))
                    {
                        values.Add(Settings.Table[startingIndex]);
                    }
                    //Console.Write(Settings.Table[startingIndex] + " ");
                    startingIndex++;
                }
                //Console.WriteLine();
                startingIndex += Settings.Count - 3;
            }

            return values.Count() == Settings.Count;
        }
        public bool isValid()
        {
            List<bool> valid = new List<bool>();
            for (int i = 0; i < Settings.Count; ++i)
            {
                bool row = checkRow(i * Settings.Count);
                bool column = checkColumn(i);
                bool square = checkSquare(i);
                if(row && column && square)
                {
                    valid.Add(true);
                }
            }
            return valid.Count() == Settings.Count;
        }
        private List<string> missingValues(int multiple)
        {
            //int startingIndex = multiple % 2 == 0 ? multiple * (Settings.Count * 2) / 2 : ((multiple - 1) * (Settings.Count * 2) / 2) + Settings.Count / 2;

            //List<string> values = new List<string>();
            //List<string> missingValues = new List<string>();

            //for (int i = 0; i < Settings.Count / 2; ++i)
            //{
            //    if (!values.Contains(Settings.Table[startingIndex]))
            //    {
            //        values.Add(Settings.Table[startingIndex]);
            //    }
            //    startingIndex++;
            //}
            //startingIndex += Settings.Count / 2;
            //for (int i = 0; i < Settings.Count / 2; ++i)
            //{
            //    if (!values.Contains(Settings.Table[startingIndex]))
            //    {
            //        values.Add(Settings.Table[startingIndex]);
            //    }
            //    startingIndex++;
            //}
            //foreach (var chars in Settings.Characters)
            //{
            //    if(!values.Contains(chars))
            //    {
            //        missingValues.Add(chars);
            //    }
            //}

            //return missingValues;
            int startingIndex = 0;
            switch (multiple % (Settings.Count / 3))
            {
                case 0:
                    startingIndex = multiple * (Settings.Count * (Settings.Count / 3)) / (Settings.Count / 3);
                    break;
                case 1:
                    startingIndex = ((multiple - 1) * (Settings.Count * (Settings.Count / 3)) / (Settings.Count / 3)) + 3;
                    break;
                case 2:
                    if (multiple > (Settings.Count / 3))
                    {
                        startingIndex = ((Settings.Count / 3) * 2) + Settings.Count * (multiple - 2);
                    }
                    else
                    {
                        startingIndex = (Settings.Count / 3) * multiple;
                    }
                    break;
                default:
                    break;
            }
            List<string> values = new List<string>();
            List<string> missingValues = new List<string>();

            for (int i = 0; i < Settings.Count / 3; i++)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (!values.Contains(Settings.Table[startingIndex]))
                    {
                        values.Add(Settings.Table[startingIndex]);
                    }
                    //Console.Write(Settings.Table[startingIndex] + " ");
                    startingIndex++;
                }
                //Console.WriteLine();
                startingIndex += Settings.Count - 3;
            }

            foreach (var chars in Settings.Characters)
            {
                if (!values.Contains(chars))
                {
                    missingValues.Add(chars);
                }
            }

            return missingValues;
        }
        private List<string> doubleValues(int multiple)
        {
            //int startingIndex = multiple % 2 == 0 ? multiple * (Settings.Count * 2) / 2 : ((multiple - 1) * (Settings.Count * 2) / 2) + Settings.Count / 2;

            //List<string> values = new List<string>();
            //List<string> doubleValues = new List<string>();

            //for (int i = 0; i < Settings.Count / 2; ++i)
            //{
            //    if (!values.Contains(Settings.Table[startingIndex]))
            //    {
            //        values.Add(Settings.Table[startingIndex]);
            //    }
            //    startingIndex++;
            //}
            //startingIndex += Settings.Count / 2;
            //for (int i = 0; i < Settings.Count / 2; ++i)
            //{
            //    if (values.Contains(Settings.Table[startingIndex]))
            //    {
            //        doubleValues.Add(Settings.Table[startingIndex]);
            //    }
            //    startingIndex++;
            //}

            //return doubleValues;
            int startingIndex = 0;
            switch (multiple % (Settings.Count / 3))
            {
                case 0:
                    startingIndex = multiple * (Settings.Count * (Settings.Count / 3)) / (Settings.Count / 3);
                    break;
                case 1:
                    startingIndex = ((multiple - 1) * (Settings.Count * (Settings.Count / 3)) / (Settings.Count / 3)) + 3;
                    break;
                case 2:
                    if (multiple > (Settings.Count / 3))
                    {
                        startingIndex = ((Settings.Count / 3) * 2) + Settings.Count * (multiple - 2);
                    }
                    else
                    {
                        startingIndex = (Settings.Count / 3) * multiple;
                    }
                    break;
                default:
                    break;
            }
            List<string> values = new List<string>();
            List<string> doubledValues = new List<string>();

            for (int i = 0; i < Settings.Count / 3; i++)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (!values.Contains(Settings.Table[startingIndex]))
                    {
                        values.Add(Settings.Table[startingIndex]);
                    }
                    //Console.Write(Settings.Table[startingIndex] + " ");
                    startingIndex++;
                }
                //Console.WriteLine();
                startingIndex += Settings.Count - 3;
            }

            return doubledValues;
        }
    }
}
