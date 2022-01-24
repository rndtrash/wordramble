using Sandbox;
using Sandbox.UI;

namespace WordRamble.HUD
{
	public class Tile : Panel
	{
		public enum TileType
		{
			Empty,
			Absent,
			Present,
			Correct
		}

		public TileType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;

				OnThemeChange( Hud.CurrentTheme );
			}
		}

		TileType type;
		Label letter;

		public Tile()
		{
			StyleSheet.Load( "/HUD/Tile.scss" );

			AddClass( "fadeanim spinanim" );
			letter = AddChild<Label>();

			Type = TileType.Empty;
		}

		public void SetLetter( char c )
		{
			letter.Text = $"{c}";
		}

		[Event( "wr.theme" )]
		public void OnThemeChange( Theme newTheme )
		{
			switch ( Type )
			{
				case TileType.Absent:
					Style.FontColor = newTheme.TileText;
					Style.BackgroundColor = newTheme.Absent;
					break;
				case TileType.Present:
					Style.FontColor = newTheme.TileText;
					Style.BackgroundColor = newTheme.Present;
					break;
				case TileType.Correct:
					Style.FontColor = newTheme.TileText;
					Style.BackgroundColor = newTheme.Correct;
					break;
				default:
					Style.FontColor = newTheme.Text;
					Style.BackgroundColor = newTheme.TileBackground;
					break;
			}
		}
	}
}
