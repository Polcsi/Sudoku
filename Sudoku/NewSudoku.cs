using System;

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
            if (Settings.SRN > 2)
            {
                fillDiagonal();
                fillRemainingBlocks(0, Settings.SRN);
            } else
            {
                fillSyncDiagonal();
                fillRemainingBlocksSync(0, 3);
            }
        }
        private void fillDiagonal()
        {
            for (int i = 0; i < Settings.Count; i = i + 3)
            {
                fillBox(i, i);
            }
        }
        private void fillSyncDiagonal()
        {
            int col = 0;
            int row = 0;
            for (int i = 0; i < 6; i += 3)
            {
                if (row == 0 && i == 0)
                {
                    col = i;
                    row = 0;
                } else if (row == 0 && i > 0)
                {
                    col = i;
                    row = 2;
                } else if (i == 6 && row == 2)
                {
                    col = 0;
                    row = 4;
                }
                fillBox(row, col);
            }
        }
        private bool fillRemainingBlocksSync(int row, int col)
        {
            if (col >= Settings.Count && row < Settings.Count - 1) 
            {
                row += 1;
                col = 0;
            }
            if (row >= Settings.Count && col >= Settings.Count)
                return true;

            if (row < Settings.SRN)
            {
                if (col < 3) 
                    col = 3; 
            }
            else if (row >= Settings.SRN && row <= 3) 
            {
                if (col == 3)
                {
                    if(row == 2)
                    {
                        row += 1;
                        col = 0;
                    }
                    else if (row == 3)
                    {
                        
                        row += 1;
                        col = 0;
                    }
                }
            }
            if (row == 5 && col == 6)
            {
                return true;
            }

            for (int i = 1; i <= Settings.Count; i++)
            {
                if (CheckIfSafe(row, col, i))
                {
                    Settings.NewTable[row, col] = i;
                    try
                    {
                        if (fillRemainingBlocksSync(row, col + 1))
                            return true;
                    }
                    catch (Exception)
                    {
                        return true;
                    }

                    Settings.NewTable[row, col] = 0; 
                }
            }
            return false; 
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
        private bool ContainsBox(int row, int colStart, int num)
        {
            try
            {
                int rowStart = row - row % 2;
                for (int i = 0; i < Settings.SRN; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        if (Settings.NewTable[rowStart + i, colStart + j] == num) return false;
                    }
                }
            } catch { }
            
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
            try
            {
                for (int i = 0; i < Settings.Count; ++i)
                {
                    if (Settings.NewTable[i, colStart] == num) return false;
                }
            } catch { }

            return true;
        }
        private bool CheckIfSafe(int row, int col, int num)
        {
            return (ContainsRow(row, num) && ContainsColumn(col, num) && ContainsBox(row, col - col % 3, num));
        }
        private bool fillRemainingBlocks(int row, int col)
        {
            if (col >= Settings.Count && row < Settings.Count - 1) // step to the next row if row length less than Settings.Count - 1 and col >= Settings.Count
            {
                row = row + 1; // increase row by one
                col = 0; // set column to 0
            }
            if (row >= Settings.Count && col >= Settings.Count)
                return true;

            if (row < Settings.SRN)
            {
                if (col < Settings.SRN) // Skip the first three line first three column cause its already filled
                    col = Settings.SRN; // set col to SRN to skip
            }
            else if (row < Settings.Count - Settings.SRN) // Skip the center box rows cause its alread filled
            {
                if (col == (int)(row / Settings.SRN) * Settings.SRN) // if col 3 add col to 3 to skip center
                    col = col + Settings.SRN; // add col + 3
            }
            else
            {
                if (col == Settings.Count - Settings.SRN) // Skip the last box cause its alread filled
                {
                    row = row + 1; // set next row
                    col = 0; // set col first column
                    if (row >= Settings.Count)
                        return true; // if row greater than Settings.Count return true and exit the function
                }
            }

            for (int i = 1; i <= Settings.Count; i++)
            {
                if (CheckIfSafe(row, col, i))
                {
                    Settings.NewTable[row, col] = i;
                    if (fillRemainingBlocks(row, col + 1))
                        return true;

                    Settings.NewTable[row, col] = 0; // Set current index 0 if all number is failed
                }
            }
            return false; // return false if can't solve the sudoku
        }
        public void printSudoku()
        {
            Actions.drawMatrix(Settings, Settings.NewTable);
        }
    }
}
