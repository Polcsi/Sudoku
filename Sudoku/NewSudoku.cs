using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class NewSudoku
    {
        private Settings Settings { get; set; }

        public NewSudoku(int N)
        {
            Settings settings = new Settings();
            settings.NewTable = new int[N, N];
            settings.Count = N;
            settings.SRN = (int)Math.Sqrt(N);

            Settings = settings;
        }
        public void FillTable()
        {
            if(Settings.SRN > 2)
            {
                fillDiagonal();
            }
            fillRemainingBlocks(0, Settings.SRN);
        }
        private void fillDiagonal()
        {
            for (int i = 0; i < Settings.Count; i = i + Settings.SRN)
            {
                fillBox(i, i);
            }
        }
        private void fillBox(int row, int col)
        {
            int num;
            for (int i = 0; i < Settings.SRN; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    do
                    {
                        num = Actions.generateRandom(Settings.Count);
                    } while (!ContainsBox(row, col, num));
                    Settings.NewTable[row + i, col + j] = num;
                }
            }
        }
        private bool ContainsBox(int rowStart, int colStart, int num)
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (Settings.NewTable[rowStart + i, colStart + j] == num) return false;
                }
            }
            return true;
        }
        private bool ContainsRow(int rowStart, int num)
        {
            for (int i = 0; i < Settings.Count; ++i)
            {
                if(Settings.NewTable[rowStart, i] == num) return false;
            }
            return true;
        }
        private bool ContainsColumn(int colStart, int num)
        {
            for (int i = 0; i < Settings.Count; ++i)
            {
                if (Settings.NewTable[i, colStart] == num) return false;
            }
            return true;
        }
        private bool CheckIfSafe(int row, int col, int num)
        {
            return (ContainsRow(row, num) && ContainsColumn(col, num) && ContainsBox(row - row % Settings.SRN, col - col % Settings.SRN, num));
        }
        private bool fillRemainingBlocks(int row, int col)
        {
            if (col >= Settings.Count && row < Settings.Count - 1)
            {
                row = row + 1;
                col = 0;
            }
            if (row >= Settings.Count && col >= Settings.Count)
                return true;

            if (row < Settings.SRN)
            {
                if (col < Settings.SRN)
                    col = Settings.SRN;
            }
            else if (row < Settings.Count - Settings.SRN)
            {
                if (col == (int)(row / Settings.SRN) * Settings.SRN)
                    col = col + Settings.SRN;
            }
            else
            {
                if (col == Settings.Count - Settings.SRN)
                {
                    row = row + 1;
                    col = 0;
                    if (row >= Settings.Count)
                        return true;
                }
            }

            for (int i = 1; i <= Settings.Count; i++)
            {
                if (CheckIfSafe(row, col, i))
                {
                    Settings.NewTable[row, col] = i;
                    if (fillRemainingBlocks(row, col + 1))
                        return true;

                    Settings.NewTable[row, col] = 0;
                }
            }
            return false;
        }
        public void printSudoku()
        {
            Actions.drawMatrix(Settings, Settings.NewTable);
        }
    }
}
