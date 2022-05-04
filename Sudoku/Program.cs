using System;

namespace Sudoku
{
	class Program
    {
        static void Main(string[] args)
        {
            //NewSudoku nS6 = new NewSudoku(9);
            //nS6.FillTable();
            //nS6.printSudoku();

            NewSudoku nS = new NewSudoku(6);
            nS.FillTable();
            nS.printSudoku();

            Sudoku smallSudoku = new Sudoku(6);
            smallSudoku.fillTable();
            //Console.WriteLine(smallSudoku.isValid());
            //smallSudoku.generateGameTable(Level.Easy);
            //smallSudoku.generateGameTable(Level.Medium);
            //smallSudoku.generateGameTable(Level.Hard);

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
