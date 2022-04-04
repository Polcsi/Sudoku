using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sudoku sudoku = new Sudoku(36, 6);
            //sudoku.fillTable();
            //Console.WriteLine(sudoku.isValid());
            //while (!sudoku.isValid())
            //{
            //    Console.Clear();
            //    sudoku.fillTable();
            //}

            Sudoku sudoku = new Sudoku(81, 9); // to make it work first need to swap double numbers from square in row
            sudoku.fillTable();
            Console.WriteLine(sudoku.isValid());

            _ = Console.ReadKey();
        }
    }
}
