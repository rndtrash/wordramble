using Sandbox;

namespace WordRamble.HUD
{
	public class RootPanel : Sandbox.UI.RootPanel
	{
		public RootPanel()
		{
			OnThemeChange( Hud.CurrentTheme );
		}

		[Event( "wr.theme" )]
		public void OnThemeChange( Theme newTheme )
		{
			Style.FontColor = newTheme.Text;
			Style.BackgroundColor = newTheme.Background;
			Style.FontSize = newTheme.FontSize;
		}
	}
}
