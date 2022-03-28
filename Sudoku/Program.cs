using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku(36, 6);
            Console.WriteLine(sudoku.checkSquare(1));
            Console.WriteLine(sudoku.checkSquare(2));
            Console.WriteLine(sudoku.checkSquare(3));
            _ = Console.ReadKey();
        }
    }
}
