using System;

namespace Sudoku
{
	class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("6x6 Sudoku");
            NewSudoku nS = new NewSudoku(6);
            nS.FillTable();
            nS.printSudoku();

            Console.WriteLine("6x6 Sudoku");
            Sudoku smallSudoku = new Sudoku(6);
            smallSudoku.fillTable();
            //Console.WriteLine(smallSudoku.isValid());
            smallSudoku.generateGameTable(Level.Easy);
            smallSudoku.generateGameTable(Level.Medium);
            smallSudoku.generateGameTable(Level.Hard);

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
