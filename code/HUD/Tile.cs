using Sandbox;
using Sandbox.UI;

namespace WordRamble.HUD
{
	public class Tile : Panel
	{
		public Color? BackgroundOverride
		{
			get
			{
				return backgroundOverride;
			}
			set
			{
				backgroundOverride = value;
				OnThemeChange( Hud.CurrentTheme );
			}
		}

		Label letter = null;
		Color? backgroundOverride = null;

		public Tile()
		{
			StyleSheet.Load( "/HUD/Tile.scss" );

			letter = AddChild<Label>();

			OnThemeChange( Hud.CurrentTheme );
		}

		public void SetLetter( char c )
		{
			letter.Text = $"{c}";
		}

		[Event( "wr.theme" )]
		public virtual void OnThemeChange( Theme newTheme )
		{
			Style.FontColor = BackgroundOverride == null ? newTheme.ButtonText : newTheme.TileText;
			Style.BackgroundColor = BackgroundOverride ?? newTheme.Background;
		}
	}
}
