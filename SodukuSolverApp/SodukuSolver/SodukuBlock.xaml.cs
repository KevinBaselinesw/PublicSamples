using static SodukuSolver.PuzzleData;

namespace SodukuSolver;

public partial class SodukuBlock : ContentView
{
	public SodukuBlock()
	{
		InitializeComponent();
	}

	internal void UpdateValues(int blockNumber, PuzzleData puzzleData, List<int>[,] Notes)
	{
		var blockValues = puzzleData.GetBlockValues(blockNumber);

		Entry[] BlockEntries = new Entry[9] {EntryA, EntryB, EntryC, EntryD, EntryE, EntryF, EntryG, EntryH, EntryI};
		BlockNotes[] blockNotes = new BlockNotes[9] {NotesA, NotesB, NotesC, NotesD, NotesE, NotesF, NotesG, NotesH, NotesI};

		if (blockValues != null && blockValues.Length == 9) 
		{
			for (int i = 0; i < blockValues.Length; i++) 
			{
				if (Notes == null || blockValues[i] > 0)
				{
                    BlockEntries[i].IsVisible = true;
                    blockNotes[i].IsVisible = false;

                    BlockEntries[i].Text = blockValues[i].ToString();
                }
				else
				{
					BlockIndex blockIndex = PuzzleData.blockIndexes[blockNumber];

					blockNotes[i].UpdateValues(Notes[blockIndex.RowIndex + (i/3), blockIndex.ColumnIndex + (i % 3)]);
                    BlockEntries[i].IsVisible = false;
                    blockNotes[i].IsVisible = true;
                }
            }

		}
	}
}