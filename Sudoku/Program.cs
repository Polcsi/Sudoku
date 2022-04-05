using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku(36, 6);
            sudoku.fillTable();
            Console.WriteLine(sudoku.isValid());

            Sudoku sudoku1 = new Sudoku(81, 9);
            sudoku1.fillTable();
            Console.WriteLine(sudoku1.isValid());

            _ = Console.ReadKey();
        }
    }
}
