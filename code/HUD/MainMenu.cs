using Sandbox;
using Sandbox.UI;

namespace WordRamble.HUD
{
	public class MainMenu : Panel
	{
		public MainMenu()
		{
			StyleSheet.Load( "/HUD/MainMenu.scss" );

			AddClass( "fullscreen fadeanim" );

			OnThemeChange( Hud.CurrentTheme );
		}

		[Event( "wr.theme" )]
		public void OnThemeChange( Theme newTheme )
		{
			//
		}
	}
}
