using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Tests;

namespace WordRamble.HUD
{
	public class MainMenu : Panel
	{
		Panel Navigation;

		public MainMenu()
		{
			StyleSheet.Load( "/HUD/MainMenu.scss" );

			AddClass( "fullscreen fadeanim" );

			Navigation = AddChild<Panel>( "topbar blur" );

			var virtualScroll = AddChild<VirtualScrollPanel>();
			virtualScroll.Layout.ItemWidth = 192;
			virtualScroll.Layout.ItemHeight = 192 + 32;
			virtualScroll.Layout.AutoColumns = true;

			virtualScroll.OnCreateCell = ( panel, data ) =>
			{
				if ( data is not GameLogic.GameDictionary dict )
					return;

				panel.AddClass( "selectable" );
				Tile c = panel.AddChild<Tile>();

				c.BackgroundOverride = new ColorHsv( dict.H, dict.S, dict.V );
				c.SetLetter( dict.Glyph.ToCharArray()[0] );

				panel.AddChild<Label>( "bt" ).Text = dict.Name;
			};

			virtualScroll.Data.AddRange( Game.Instance.ServerConnection.Dictionaries.Values );

			// DEBUG
			//(Local.Hud as RootPanel).SeeThrough = true;

			OnThemeChange( Hud.CurrentTheme );
		}

		[Event( "wr.theme" )]
		public void OnThemeChange( Theme newTheme )
		{
			Navigation.Style.BackgroundColor = newTheme.Background.WithAlpha( 0.5f );
		}
	}
}
