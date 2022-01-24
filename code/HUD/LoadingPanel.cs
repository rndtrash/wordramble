using Sandbox;
using Sandbox.UI;

namespace WordRamble.HUD
{
	public class LoadingPanel : Panel
	{
		class WordRambleLogo : Panel
		{
			static char[][] letters => new[]
			{
				new[] {'W', ' '},
				new[] {'O', ' '},
				new[] {'R', 'R'},
				new[] {'D', 'A'},
				new[] {' ', 'M'},
				new[] {' ', 'B'},
				new[] {' ', 'L'},
				new[] {' ', 'E'},
			};

			public WordRambleLogo()
			{
				for (int i = 0; i < letters.Length; i++ )
				{
					var line = letters[i];
					var strip = AddChild<Panel>( "strip spinanim" );

					for ( int j = 0; j < 2; j++ )
					{
						var c = line[j];
						var t = strip.AddChild<Tile>();
						t.Type = c == ' ' ? Tile.TileType.Empty : (j % 2 == 0 ? Tile.TileType.Correct : Tile.TileType.Present);
						t.SetLetter( c );
					}
				}

				OnThemeChange( Hud.CurrentTheme );
			}

			[Event( "wr.theme" )]
			public void OnThemeChange( Theme newTheme ) { }
		}

		public LoadingPanel()
		{
			StyleSheet.Load( "/HUD/LoadingPanel.scss" );

			AddClass( "fullscreen fadeanim" );
			AddChild<WordRambleLogo>();
			var bottomText = AddChild<Panel>("lmaobottomtext fadeanim");
			bottomText.AddChild<Spinner>();
			var label = bottomText.AddChild<Label>();
			label.Text = "Loading...";

			OnThemeChange( Hud.CurrentTheme );
		}

		[Event( "wr.theme" )]
		public void OnThemeChange( Theme newTheme )
		{
			//
		}
	}
}
