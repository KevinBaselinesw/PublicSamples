using System.Diagnostics;

namespace SodukuSolver
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            PuzzleData puzzle = new PuzzleData();

            var r0 = puzzle.GetRowValues(0);
            var r1 = puzzle.GetRowValues(1);
            var r2 = puzzle.GetRowValues(2);
            var r3 = puzzle.GetRowValues(3);
            var r4 = puzzle.GetRowValues(4);
            var r5 = puzzle.GetRowValues(5);
            var r6 = puzzle.GetRowValues(6);
            var r7 = puzzle.GetRowValues(7);
            var r8 = puzzle.GetRowValues(8);

            var c0 = puzzle.GetColumnValues(0);
            var c1 = puzzle.GetColumnValues(1);
            var c2 = puzzle.GetColumnValues(2);
            var c3 = puzzle.GetColumnValues(3);
            var c4 = puzzle.GetColumnValues(4);
            var c5 = puzzle.GetColumnValues(5);
            var c6 = puzzle.GetColumnValues(6);
            var c7 = puzzle.GetColumnValues(7);
            var c8 = puzzle.GetColumnValues(8);

            var b0 = puzzle.GetBlockValues(0);
            var b1 = puzzle.GetBlockValues(1);
            var b2 = puzzle.GetBlockValues(2);
            var b3 = puzzle.GetBlockValues(3);
            var b4 = puzzle.GetBlockValues(4);
            var b5 = puzzle.GetBlockValues(5);
            var b6 = puzzle.GetBlockValues(6);
            var b7 = puzzle.GetBlockValues(7);
            var b8 = puzzle.GetBlockValues(8);

            for (int rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                {
                    int blockIndex = 0;

                    if (rowIndex < 3)
                        blockIndex = 0;
                    else if (rowIndex < 6)
                        blockIndex = 3;
                    else if (rowIndex < 9)
                        blockIndex = 6;

                    blockIndex += (columnIndex / 3);
                    Debug.WriteLine($"{rowIndex}, {columnIndex}, {blockIndex}");
                }
            }

            var notes = puzzle.GetPuzzleNotes();

            return;
            
        }
    }

}
