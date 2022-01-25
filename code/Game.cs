using Sandbox;
using System.Linq;

namespace WordRamble
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class Game : GameBase
	{
		public Game()
		{
			if ( IsServer )
			{
				_ = new HUD.Hud();
			}

			if ( IsClient )
			{
				GameLogic.ServerConnection.Instance.Connect();
			}
		}

		public override CameraSetup BuildCamera( CameraSetup camSetup ) => camSetup;

		public override bool CanHearPlayerVoice( Client source, Client dest ) => false;

		public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason ) { }

		public override void ClientJoined( Client client ) { }

		public override void OnVoicePlayed( long playerId, float level ) { }

		public override void PostLevelLoaded() { }

		public override void Shutdown() { }
	}

}
