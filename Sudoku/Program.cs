using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku(36, 6);
            Console.WriteLine(sudoku.checkSquare(0));
            _ = Console.ReadKey();
        }
    }
}
