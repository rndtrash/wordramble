using Sandbox;
using Sandbox.UI;

namespace WordRamble.HUD
{
	public partial class Hud : HudEntity<RootPanel>
	{
		public static Hud Instance { get; internal set; }

		public static Theme CurrentTheme
		{
			get
			{
				return Instance?.currentTheme ?? Theme.DefaultLight;
			}

			set
			{
				if ( Instance is not Hud hud )
					return;

				hud.currentTheme = value;
				Event.Run( "wr.theme", value );
			}
		}

		Theme currentTheme;

		public Hud()
		{
			Instance = this;

			if ( !IsClient ) return;

			RootPanel.StyleSheet.Load( "/HUD/Hud.scss" );

			CurrentTheme = Theme.DefaultLight;

			RootPanel.AddChild<LoadingPanel>();
			
			// DEBUG
			//DebugShite();
		}

		async void DebugShite()
		{
			var d = RootPanel.AddChild<Panel>();
			d.AddClass( "fullscreen" );
			d.Style.Overflow = OverflowMode.Visible;
			d.Style.FlexWrap = Wrap.Wrap;

			for ( int i = 0; i < 200; i++ )
			{
				d.AddChild<Tile>();
				await Task.DelaySeconds( 0.1f );
			}
		}
	}
}
