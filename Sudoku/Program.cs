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

            //Sudoku sudoku = new Sudoku(81, 9);
            //sudoku.fillTable();
            _ = Console.ReadKey();
        }
    }
}
