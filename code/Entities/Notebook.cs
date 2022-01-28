using Sandbox;

namespace WordRamble.Entities
{
	[Library( "wr_notebook" )]
	[Hammer.EntityTool( "Notebook", "WordRamble" )]
	[Hammer.EditorModel( "models/note.vmdl", CastShadows = true )]
	public class Notebook : ModelEntity
	{
		public static Notebook Instance { get; internal set; } = null;

		public override void Spawn()
		{
			if ( Instance != null )
			{
				Log.Error( "Only one wr_notebook can exist per map!" );
				Delete();
				return;
			}

			Instance = this;

			base.Spawn();
			
			SetModel( "models/note.vmdl" );
		}

		public override void ClientSpawn()
		{
			base.ClientSpawn();

			Instance = this;
		}
	}
}
