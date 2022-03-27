﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sudoku
{
    internal class Sudoku
    {
        public Sudoku()
        {
            fillTable();
        }
        private Random rnd = new Random();
        private string[] table = new string[36];
        private readonly string[] characters = new string[6] { "1", "2", "3", "4", "5", "6"};
        private void fillTable()
        {
            for (int i = 0; i < 6; i++)
            {
                while (!checkRow(i * 6))
                {
                    if (i < 1)
                    {
                        for (int j = 0; j < 6; ++j)
                        {
                            table[j + (i * 6)] = characters[rnd.Next(characters.Length)];
                            Actions.DelayAction(50, new Action(() => { draw(); }));
                        }
                    }
                    else
                    {
                        string[] aboveChars;
                        for (int j = 0; j < 6; ++j)
                        {
                            aboveChars = new string[(i * 6) / 6];
                            for (int k = 1; k < aboveChars.Length + 1; ++k)
                            {
                                aboveChars[k - 1] = table[(j + (i * 6) - (k * 6))];
                                //Console.Write($"ABOVE ELEMENT: {table[(j + (i * 6) - (k * 6))]} INDEX: {(j + (i * 6) - (k * 6))} |k: {k}.| ACTUAL INDEX: {j + (i * 6)}  ");
                            }
                            //Console.WriteLine();
                            string randomChar = characters[rnd.Next(characters.Length)];
                            while (aboveChars.Contains(randomChar))
                            {
                                randomChar = characters[rnd.Next(characters.Length)];
                            }
                            table[j + (i * 6)] = randomChar;
                            Actions.DelayAction(100, new Action(() => { draw(); }));
                        }
                    }
                    
                }
            }
        }
        private void draw()
        {
            Console.Clear();
            for (int i = 0; i < table.Length; ++i)
            {
                Console.Write(table[i]);
                if ((i + 1) % 6 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
        private bool checkRow(int multiple)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < 6; ++i)
            {
                if (!values.Contains(table[i + multiple]))
                {
                    values.Add(table[i + multiple]);
                }
            }
            return values.Count() == 6 ? true : false;
        }
        private bool checkColumn(int multiple)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < 6; ++i)
            {
                if (!values.Contains(table[i * 6 + multiple]))
                {
                    values.Add(table[i * 6 + multiple]);
                }
            }
            return values.Count() == 6 ? true : false;
        }
        public bool checkSquare(int multiple)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < 6; ++i)
            {
                if (!values.Contains(table[i]) && i <= 2)
                {
                    values.Add(table[i]);
                } else if (!values.Contains(table[i]) && i > 2)
                {

                }
            }

            return values.Count() == 6 ? true : false;
        }
    }
}
