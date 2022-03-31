using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku(36, 6);
            while (!sudoku.isValid())
            {
                Console.Clear();
                sudoku.fillTable();
            }

            _ = Console.ReadKey();
        }
    }
}
