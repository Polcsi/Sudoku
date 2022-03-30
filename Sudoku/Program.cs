using System;
using System.Collections.Generic;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku(36, 6);
            while(!sudoku.isValid())
            {
                Console.Clear();
                sudoku.fillTable();
            }
            Console.WriteLine(sudoku.isValid());
            
            _ = Console.ReadKey();
        }
    }
}
