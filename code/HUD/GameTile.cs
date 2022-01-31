using Sandbox;
using Sandbox.UI;

namespace WordRamble.HUD
{
	public class GameTile : Tile
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

		public GameTile() : base()
		{
			Type = TileType.Empty;
		}

		public override void OnThemeChange( Theme newTheme )
		{
			base.OnThemeChange( newTheme );

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
