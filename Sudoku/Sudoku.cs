﻿using System;
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
            int result = 0;
            for (int i = 0; i < count; ++i)
            {
                settings.Characters[i] = $"{i + 1}";
                result += i+1;
            }
            settings.Result = result;
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
                            List<string> missing = missingValues(i);
                            string randomChar = Settings.Characters[rnd.Next(Settings.Characters.Length)];
                            //string randomChar = missing.Count != 0 ? missing[0] : Settings.Characters[rnd.Next(Settings.Characters.Length)];
                            while (aboveColumnChars.Contains(randomChar))
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
            int startingIndex = multiple % 2 == 0 ? multiple * (Settings.Count * 2) / 2 : ((multiple - 1) * (Settings.Count * 2) / 2) + Settings.Count / 2;

            List<string> values = new List<string>();

            for (int i = 0; i < Settings.Count / 2; ++i)
            {
                if (!values.Contains(Settings.Table[startingIndex]))
                {
                    values.Add(Settings.Table[startingIndex]);
                }
                //Console.Write(Settings.Table[startingIndex] + " ");
                startingIndex++;
            }
            startingIndex += Settings.Count / 2;
            for (int i = 0; i < Settings.Count / 2; ++i)
            {
                if (!values.Contains(Settings.Table[startingIndex]))
                {
                    values.Add(Settings.Table[startingIndex]);
                }
                //Console.Write(Settings.Table[startingIndex] + " ");
                startingIndex++;
            }

            return values.Count() ==  Settings.Count;
        }
        public List<string> missingValues(int multiple)
        {
            int startingIndex = multiple % 2 == 0 ? multiple * (Settings.Count * 2) / 2 : ((multiple - 1) * (Settings.Count * 2) / 2) + Settings.Count / 2;

            List<string> values = new List<string>();
            List<string> missingValues = new List<string>();

            for (int i = 0; i < Settings.Count / 2; ++i)
            {
                if (!values.Contains(Settings.Table[startingIndex]))
                {
                    values.Add(Settings.Table[startingIndex]);
                }
                startingIndex++;
            }
            startingIndex += Settings.Count / 2;
            for (int i = 0; i < Settings.Count / 2; ++i)
            {
                if (!values.Contains(Settings.Table[startingIndex]))
                {
                    values.Add(Settings.Table[startingIndex]);
                }
                startingIndex++;
            }
            foreach (var chars in Settings.Characters)
            {
                if(!values.Contains(chars))
                {
                    missingValues.Add(chars);
                }
            }

            return missingValues;
        }
    }
}
