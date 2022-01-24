namespace WordRamble.HUD
{
	public struct Theme
	{
		public static readonly Theme DefaultLight = new()
		{
			FontSize = Sandbox.UI.Length.Pixels(32f) ?? 32f,

			Background = new Color32( 255, 255, 255 ).ToColor(),
			Text = new Color32( 33, 33, 33 ).ToColor(),

			ButtonBackground = new Color32( 216, 216, 216, 255 ).ToColor(),
			ButtonText = new Color32( 33, 33, 33 ).ToColor(),

			TileText = new Color32( 255, 255, 255 ).ToColor(),
			TileBackground = new Color32( 211, 214, 218 ).ToColor(),
			Border = new Color32( 33, 33, 33, 20 ).ToColor(),
			Correct = new Color32( 106, 170, 100 ).ToColor(),
			Present = new Color32( 201, 180, 88 ).ToColor(),
			Absent = new Color32( 120, 124, 126 ).ToColor()
		};

		public Sandbox.UI.Length FontSize { get; set; }

		public Color Background { get; set; }
		public Color Text { get; set; }

		public Color ButtonBackground { get; set; }
		public Color ButtonText { get; set; }

		public Color TileText { get; set; }
		public Color TileBackground { get; set; }
		public Color Border { get; set; }
		public Color Correct { get; set; }
		public Color Present { get; set; }
		public Color Absent { get; set; }
	}
}
