using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public enum Level { Easy = 2, Medium = 10, Hard = 20}
    class Sudoku
    {
        private static readonly Random rnd = new Random();
        private Settings Settings { get; set; }
        public Sudoku(int count)
        {
            Settings settings = new Settings();
            settings.Table = new string[count * count];
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
                START:
                    string[] aboveColumnChars;
                    int add = i;
                    if (i % (Settings.Count / 3) == 1)
                    {
                        add -= 1;
                    }
                    else if (i % (Settings.Count / 3) == 2)
                    {
                        add -= 2;
                    }
                    for (int j = 0; j < Settings.Count; ++j)
                    {

                        if (j == 3)
                        {
                            add += 1;
                        }
                        if (j == 3 + 3)
                        {
                            add += 1;
                        }
                        List<string> missing = missingValues(add);
                        //string s = "";
                        //foreach (var item in missing)
                        //{
                        //    s += $"{item}";
                        //}
                        //Console.WriteLine($"MISSING {add} {s}");
                        aboveColumnChars = new string[(i * Settings.Count) / Settings.Count];
                        for (int k = 1; k < aboveColumnChars.Length + 1; ++k)
                        {
                            aboveColumnChars[k - 1] = Settings.Table[(j + (i * Settings.Count) - (k * Settings.Count))];
                        }
                        //foreach (var item in aboveColumnChars)
                        //{
                        //    Console.Write(item + " ");
                        //}
                        //Console.WriteLine($"at {j + (i * Settings.Count)}");

                        List<string> possibleValues = calculatePossibleValues(aboveColumnChars.ToList(), getSquareValues(add));
                        string randomChar = missing[rnd.Next(missing.Count)];
                        int avoidInfiniteLoop = 0;
                        while (aboveColumnChars.Contains(randomChar) && missing.Contains(randomChar) && avoidInfiniteLoop < 30)
                        {
                            randomChar = missing[rnd.Next(missing.Count)];
                            avoidInfiniteLoop++;
                            //if (!aboveColumnChars.Contains(randomChar) || avoidInfiniteLoop > 30)
                            //{
                            //    break;
                            //}
                            //else
                            //{
                            //    continue;
                            //}
                        }
                        if (aboveColumnChars.Contains(randomChar))
                        {
                            for (int k = j; k > 0; --k)
                            {
                                Settings.Table[k + (i * Settings.Count)] = null;
                            }
                            j = 0;
                            goto START;
                        }

                        Settings.Table[j + (i * Settings.Count)] = randomChar;
                    }
                }
                Actions.DelayAction(70, new Action(() => { draw(Settings.Table, i * Settings.Count); }));
            }
            Actions.WriteSudoku(Settings.Count, Settings.Table, $"sudoku{Actions.stringify(Settings.Count)}");
        }
        private void draw(string[] table, int row)
        {
            string border = "+";
            for (int i = 0; i < Settings.Count / 3; ++i)
            {
                border += "-----+";
            }
            if (row == 0) { Console.WriteLine(border); }
            for (int i = 0; i < Settings.Count; ++i)
            {
                Actions.DelayAction(0, new Action(() => { Console.Write("|" + table[i + row]); }));
                if ((i + 1) % Settings.Count == 0)
                {
                    Console.WriteLine("|");
                }
            }
            if(row == (Settings.Count * ((Settings.Count / 3) - 1)) || row == (Settings.Count * ((Settings.Count / 3) + ((Settings.Count / 3) - 1))))
            {
                Console.WriteLine(border);
            }
            if(row == (Settings.Count * Settings.Count) - Settings.Count) { Console.WriteLine(border); }
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
        private List<string> getSquareValues(int multiple)
        {
            int startingIndex = getStartingIndex(multiple);
            List<string> values = getValues(startingIndex);

            return values;
        }
        private List<string> calculatePossibleValues(List<string> aboveElements, List<string> squareValues)
        {
            List<string> possibleValues = new List<string>();
            for (int i = 0; i < Settings.Count; ++i)
            {
                string character = Settings.Characters[i];
                if(!aboveElements.Contains(character) && !squareValues.Contains(character))
                {
                    possibleValues.Add(character);
                }
            }

            return possibleValues;
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
        public void generateGameTable(Level level)
        {
            Settings.GameTable = Settings.Table;
            int miss = Settings.Count + (int)level;
            for (int i = 0; i < miss; ++i)
            {
                Settings.GameTable[rnd.Next(Settings.Count * Settings.Count)] = " ";
            }
            Console.WriteLine($"{Actions.stringify(Settings.Count)} {level} Sudoku");
            for (int i = 0; i < Settings.Count; ++i)
            {
                draw(Settings.GameTable, i * Settings.Count);
            }
            Actions.WriteSudoku(Settings.Count, Settings.GameTable, $"{Actions.stringify(Settings.Count)}-{level}");
        }
    }
}
