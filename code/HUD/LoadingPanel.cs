using Sandbox;
using Sandbox.UI;
using WordRamble.GameLogic;

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
				for ( int i = 0; i < letters.Length; i++ )
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

		Panel bottomText;
		Label bottomLabel;
		TimeUntil PlayedIntro = 1.5f;
		bool commitDie = false;

		public LoadingPanel()
		{
			StyleSheet.Load( "/HUD/LoadingPanel.scss" );

			AddClass( "fullscreen fadeanim" );
			AddChild<WordRambleLogo>();
			bottomText = AddChild<Panel>( "lmaobottomtext fadeanim" );
			bottomText.AddChild<Spinner>();
			bottomLabel = bottomText.AddChild<Label>();
			bottomLabel.Text = "Loading...";

			OnThemeChange( Hud.CurrentTheme );
			OnLoadingStateChange( Game.Instance.ServerConnection?.State ?? ServerConnection.ServerConnectionState.Invalid );
		}

		public override void Tick()
		{
			base.Tick();

			if ( commitDie && PlayedIntro < 0 )
			{
				commitDie = false;
				Hud.Instance.OpenMainMenu();
				Delete();
			}
		}

		[Event( "wr.theme" )]
		public void OnThemeChange( Theme newTheme )
		{
			//
		}

		[Event( "wr.loading" )]
		public void OnLoadingStateChange( ServerConnection.ServerConnectionState newState )
		{
			switch ( newState )
			{
				case ServerConnection.ServerConnectionState.FindingServer:
					bottomLabel.Text = "Finding a server...";
					break;
				case ServerConnection.ServerConnectionState.GettingDictionaries:
					bottomLabel.Text = "Finding dictionaries...";
					break;
				case ServerConnection.ServerConnectionState.Done:
					bottomLabel.Text = "Done!";
					commitDie = true;
					break;
				case ServerConnection.ServerConnectionState.Invalid:
				default:
					break;
			}
		}
	}
}
