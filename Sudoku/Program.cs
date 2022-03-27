using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudoku = new Sudoku();
            sudoku.draw();
            Console.WriteLine($"Check First Column: {sudoku.checkColumn(0)}");
            Console.WriteLine($"Check Second Column: {sudoku.checkColumn(1)}");
            Console.WriteLine($"Check Third Column: {sudoku.checkColumn(2)}");
            Console.WriteLine($"Check Fourth Column: {sudoku.checkColumn(3)}");
            Console.WriteLine($"Check Fifth Column: {sudoku.checkColumn(4)}");
            Console.WriteLine($"Check Last Column: {sudoku.checkColumn(5)}");
            Console.WriteLine($"Check First Row: {sudoku.checkRow(0)}");
            Console.WriteLine($"Check Second Row: {sudoku.checkRow(6)}");
            Console.WriteLine($"Check third Row: {sudoku.checkRow(12)}");
            Console.WriteLine($"Check fourth Row: {sudoku.checkRow(18)}");
            Console.WriteLine($"Check Fifth Row: {sudoku.checkRow(24)}");
            Console.WriteLine($"Check Last Row: {sudoku.checkRow(30)}");
            Console.ReadKey();
        }
    }
}
