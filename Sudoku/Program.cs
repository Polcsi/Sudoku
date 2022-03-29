using System;
using System.Collections.Generic;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku(36, 6);
            Console.WriteLine(sudoku.checkSquare(0));
            Console.WriteLine(sudoku.checkSquare(1));
            Console.WriteLine(sudoku.checkSquare(2));
            Console.WriteLine(sudoku.checkSquare(3));
            Console.WriteLine(sudoku.checkSquare(4));
            Console.WriteLine(sudoku.checkSquare(5));
            List<string> list = sudoku.missingValues(0);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            _ = Console.ReadKey();
        }
    }
}
