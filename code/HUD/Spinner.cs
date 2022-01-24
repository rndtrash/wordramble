using Sandbox;
using Sandbox.UI;

namespace WordRamble.HUD
{
	// TODO: use a shader instead of the texture
	public class Spinner : Panel
	{
		TimeSince Creation = 0;

		public Spinner()
		{
			StyleSheet.Load( "/HUD/Spinner.scss" );

			OnThemeChange( Hud.CurrentTheme );
		}

		[Event( "wr.theme" )]
		public void OnThemeChange( Theme newTheme )
		{
			Style.BackgroundTint = newTheme.ButtonBackground;
		}

		public override void Tick()
		{
			base.Tick();

			PanelTransform pt = new();
			pt.AddRotation( 0, 0, Creation * 360 );
			Style.Transform = pt;
		}
	}
}
