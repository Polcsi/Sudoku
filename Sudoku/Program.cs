using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            new Sudoku(81, 9);
            _ = Console.ReadKey();
        }
    }
}
