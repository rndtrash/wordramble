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
			virtualScroll.Layout.ItemHeight = 192;
			virtualScroll.Layout.AutoColumns = true;

			var debugCount = 0;
			virtualScroll.OnCreateCell = ( panel, data ) =>
			{
				if ( data is not GameLogic.GameDictionary dict )
					return;

				var c = panel.AddChild<Panel>();

				c.Style.Width = c.Style.Height = Length.Fraction( 1 );
				c.Style.Padding = Length.Pixels( 8 );
				c.Style.BackgroundColor = new ColorHsv( dict.H, dict.S, dict.V );

				var l = c.AddChild<Label>();
				l.Text = $"{debugCount++}";
				l.Style.FontColor = Color.White;
				l.Style.FontSize = Length.Pixels( 24 );
			};

			for ( var i = 0; i < 120; i++ )
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
