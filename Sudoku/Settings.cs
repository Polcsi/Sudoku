namespace Sudoku
{
    class Settings
    {
        public string[] Table { get; set; }
        public string[] GameTable { get; set; }
        public string[] Characters { get; set; }
        public int Count { get; set; }
        public int[,] NewTable { get; set; }
        public int SRN { get; set; } // square root of Count
    }
}
