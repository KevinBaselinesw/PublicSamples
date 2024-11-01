namespace SodukuSolver;

public partial class BlockNotes : ContentView
{
	public BlockNotes()
	{
		InitializeComponent();
	}

	public void UpdateValues(List<int>? Notes)
	{
		Label[] labels = new Label[9] {Note1, Note2, Note3, Note4, Note5, Note6, Note7, Note8, Note9};

		if (Notes != null ) 
		{
			foreach (Label label in labels)
			{
				label.Text = "";
			}

			foreach (var note in Notes ) 
			{
				labels[note - 1].Text = note.ToString();
			}
		}



	}
}