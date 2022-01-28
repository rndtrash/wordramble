using Sandbox;

namespace WordRamble.HUD
{
	public class RootPanel : Sandbox.UI.RootPanel
	{
		public bool SeeThrough
		{
			get
			{
				return seeThrough;
			}

			set
			{
				seeThrough = value;
				OnThemeChange( Hud.CurrentTheme );
			}
		}

		bool seeThrough;

		public RootPanel()
		{
			OnThemeChange( Hud.CurrentTheme );
		}

		[Event( "wr.theme" )]
		public void OnThemeChange( Theme newTheme )
		{
			Style.FontColor = newTheme.Text;
			Style.BackgroundColor = newTheme.Background.WithAlpha( SeeThrough ? 0 : 1 );
			Style.FontSize = newTheme.FontSize;
		}
	}
}
