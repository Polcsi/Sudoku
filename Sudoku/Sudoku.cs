using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    class Sudoku
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
                            aboveColumnChars = new string[(i * Settings.Count) / Settings.Count];
                            for (int k = 1; k < aboveColumnChars.Length + 1; ++k)
                            {
                                aboveColumnChars[k - 1] = Settings.Table[(j + (i * Settings.Count) - (k * Settings.Count))];
                            }

                            string randomChar = Settings.Characters[rnd.Next(Settings.Characters.Length)];
                            int avoidInfiniteLoop = 0;
                            while (aboveColumnChars.Contains(randomChar) && avoidInfiniteLoop < 14)
                            {
                                randomChar = Settings.Characters[rnd.Next(Settings.Characters.Length)];
                                avoidInfiniteLoop++;
                            }

                            Settings.Table[j + (i * Settings.Count)] = randomChar;
                        }
                    }
                }

                int add = i;
                if (i % (Settings.Count / 3) == 1)
                {
                    add -= 1;
                }
                else if (i % (Settings.Count / 3) == 2)
                {
                    add -= 2;
                }
                List<string> doubledFirstSquare = doubleValues(add);
                List<string> missingFirstSquare = missingValues(add);
                List<string> doubledSecondSquare = doubleValues(add + 1);
                List<string> missingSecondSquare = missingValues(add + 1);
                //List<string> doubledThirdSquare = doubleValues(add + 2);
                //List<string> missingThirdSquare = missingValues(add + 2);

                for (int k = i * Settings.Count + (3 * 0); k < i * Settings.Count + (3 * 0) + 3; ++k)
                {
                    string doubled = "";
                    foreach (var item in doubledFirstSquare)
                    {
                        doubled += $"{item}";
                    }
                    //Console.WriteLine($"CHECK FIRST COLUMN '{doubled}' "+ doubledFirstSquare.Contains(Settings.Table[k]));
                    if (doubledFirstSquare.Contains(Settings.Table[k]))
                    {
                        for (int l = i * Settings.Count + (3 * 1); l < i * Settings.Count + (3 * 1) + 3; ++l)
                        {
                            //Console.WriteLine($"CHECK MIDDLE COLUMN e: {Settings.Table[l]} {doubledSecondSquare.Contains(Settings.Table[l])}");
                            if(doubledSecondSquare.Contains(Settings.Table[l]) && missingFirstSquare.Contains(Settings.Table[l]))
                            {
                                //Console.WriteLine($"SWAP: {Settings.Table[l]} to {Settings.Table[k]} at {l} and {Settings.Table[k]} to {Settings.Table[l]} at {k}");
                                string ref1 = Settings.Table[l];
                                Settings.Table[l] = Settings.Table[k];
                                Settings.Table[k] = ref1;
                            }
                        }
                    } 
                }
                Actions.DelayAction(70, new Action(() => { draw(i * Settings.Count); }));
            }
            Actions.WriteSudoku(Settings);
        }
        private void draw(int row)
        {
            for (int i = 0; i < Settings.Count; ++i)
            {
                Actions.DelayAction(0, new Action(() => { Console.Write("|" + Settings.Table[i + row]); }));
                if ((i + 1) % Settings.Count == 0)
                {
                    Console.WriteLine("|");
                }
            }
        }
        private bool checkRow(int multiple)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < Settings.Count; ++i)
            {
                if (!values.Contains(Settings.Table[i + multiple]))
                {
                    values.Add(Settings.Table[i + multiple]);
                }
            }
            return values.Count() == Settings.Count;
        }
        private bool checkColumn(int multiple)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < Settings.Count; ++i)
            {
                if (!values.Contains(Settings.Table[i * Settings.Count + multiple]))
                {
                    values.Add(Settings.Table[i * Settings.Count + multiple]);
                }
            }
            return values.Count() == Settings.Count;
        }
        private bool checkSquare(int multiple)
        {
            int startingIndex = getStartingIndex(multiple);
            List<string> values = getValues(startingIndex);

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
                if (row && column && square)
                {
                    valid.Add(true);
                }
            }
            return valid.Count() == Settings.Count;
        }
        private List<string> missingValues(int multiple)
        {
            int startingIndex = getStartingIndex(multiple);
            List<string> values = getValues(startingIndex);
            List<string> missingValues = new List<string>();

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
            int startingIndex = getStartingIndex(multiple);
            List<string> values = new List<string>();
            List<string> doubledValues = new List<string>();

            for (int i = 0; i < Settings.Count / 3; i++)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (i == 0 && !values.Contains(Settings.Table[startingIndex]))
                    {
                        values.Add(Settings.Table[startingIndex]);
                    } else if (i > 0 && values.Contains(Settings.Table[startingIndex]))
                    {
                        doubledValues.Add(Settings.Table[startingIndex]);
                    }
                    startingIndex++;
                }
                startingIndex += Settings.Count - 3;
            }

            return doubledValues;
        }
        private List<string> getValues(int startingIndex)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < Settings.Count / 3; i++)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (!values.Contains(Settings.Table[startingIndex]))
                    {
                        values.Add(Settings.Table[startingIndex]);
                    }
                    startingIndex++;
                }
                startingIndex += Settings.Count - 3;
            }
            return values;
        }
        private int getStartingIndex(int multiple)
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
                    }
                    else
                    {
                        startingIndex = (Settings.Count / 3) * multiple;
                    }
                    break;
                default:
                    break;
            }
            return startingIndex;
        }
    }
}
