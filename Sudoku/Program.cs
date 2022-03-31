using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku(81, 9);
            //while(!sudoku.isValid())
            //{
            //    Console.Clear();
            //    sudoku.fillTable();
            //}

            sudoku.fillTable();
            Console.WriteLine(sudoku.checkEverySqureType(0));
            Console.WriteLine(sudoku.checkEverySqureType(1));
            Console.WriteLine(sudoku.checkEverySqureType(2));
            Console.WriteLine(sudoku.checkEverySqureType(3));

            _ = Console.ReadKey();
        }
    }
}
