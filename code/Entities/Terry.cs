using Sandbox;

namespace WordRamble.Entities
{
	[Library( "wr_terry" )]
	[Hammer.EntityTool( "Terry", "WordRamble" )]
	[Hammer.EditorModel( "models/editor/playerstart.vmdl" )]
	public class Terry : Entity
	{
		public static Terry Instance { get; internal set; } = null;

		public Terry()
		{
			Transmit = TransmitType.Always;
		}

		public override void Spawn()
		{
			base.Spawn();

			if ( Instance != null )
			{
				Log.Error( "Only one wr_terry can exist per map!" );
				Delete();
				return;
			}

			Instance = this;
		}

		public override void ClientSpawn()
		{
			base.ClientSpawn();

			Instance = this;
		}
	}
}
