using Sandbox;

namespace WordRamble.Entities
{
	[Library( "wr_cameracenter" )]
	[Hammer.DrawAngles]
	[Hammer.EntityTool( "Camera center", "WordRamble" )]
	[Hammer.BoxSize( 2f )]
	public class CameraCenter : Entity
	{
		public static CameraCenter Instance { get; internal set; } = null;

		public CameraCenter()
		{
			Transmit = TransmitType.Always;
		}

		public override void Spawn()
		{
			base.Spawn();

			if ( Instance != null )
			{
				Log.Error( "Only one wr_cameracenter can exist per map!" );
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
