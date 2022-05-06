using System;

namespace Sudoku
{
	class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("6x6 Sudoku");
            SudokuMatrix sudokuMatrix = new SudokuMatrix(6);
            sudokuMatrix.FillTable();
            sudokuMatrix.printSudoku();

            Console.WriteLine("6x6 Sudoku");
            Sudoku sudoku = new Sudoku(6);
            sudoku.fillTable();
            //Console.WriteLine(smallSudoku.isValid());
            sudoku.generateGameTable(Level.Easy);
            sudoku.generateGameTable(Level.Medium);
            sudoku.generateGameTable(Level.Hard);

            // 9x9 but really slow
            //Sudoku regularSudoku = new Sudoku(9);
            //regularSudoku.fillTable();
            //Console.WriteLine(regularSudoku.isValid());
            //regularSudoku.generateGameTable(Level.Easy);
            //regularSudoku.generateGameTable(Level.Medium);
            //regularSudoku.generateGameTable(Level.Hard);

            _ = Console.ReadKey();
        }
    }
}
