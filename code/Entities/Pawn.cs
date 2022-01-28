using Sandbox;

namespace WordRamble.Entities
{
	public class Pawn : Player
	{
		public class WRController : BasePlayerController
		{
		}

		public class WRAnimator : PawnAnimator
		{
			public override void Simulate()
			{
				//
				// Let the animation graph know some shit
				//
				SetParam( "b_grounded", true );
				SetParam( "b_noclip", false );
				SetParam( "b_sit", true );
				SetParam( "b_swim", false );

				Vector3 aimPos = Notebook.Instance.Position;
				Vector3 lookPos = aimPos;

				//
				// Look in the direction what the player's input is facing
				//
				SetLookAt( "aim_eyes", lookPos );
				SetLookAt( "aim_head", lookPos );
				SetLookAt( "aim_body", aimPos );

				SetParam( "duck", 0 );

				SetParam( "holdtype", 0 );
				SetParam( "aim_body_weight", 0.5f );
			}
		}

		public class WRCamera : Camera
		{
			public enum CameraState
			{
				Invalid,
				ZoomOut,
				ZoomIn
			}

			public CameraState State
			{
				get
				{
					return state;
				}

				internal set
				{
					state = value;

					switch ( state )
					{
						case CameraState.ZoomOut:
							targetDistance = 500f;
							targetAngles = new( 45f, -90f + CameraCenter.Instance.Rotation.Angles().yaw, 0 );
							break;
						case CameraState.ZoomIn:
							targetDistance = 10f;
							targetAngles = new( 90f, Notebook.Instance.Rotation.Angles().yaw, 0 );
							break;
						default:
							break;
					}
					targetRotation = Rotation.From( targetAngles );
				}
			}

			float distance;
			float targetDistance;
			Angles targetAngles;
			Rotation targetRotation;
			CameraState state;

			public WRCamera()
			{
				Reset();
			}

			public override void Update()
			{
				Position = CameraCenter.Instance.Position + Rotation.Backward * distance;
				Rotation = Rotation.Slerp( Rotation, targetRotation, 2f * Time.Delta );

				distance = distance.LerpTo( targetDistance, 2f * Time.Delta );
			}

			public void Reset()
			{
				State = CameraState.ZoomOut;
				distance = targetDistance;

				Rotation = targetRotation;
				Position = CameraCenter.Instance.Position + Rotation.Backward * distance;
			}
		}

		public Clothing.Container Clothing = new();

		public Pawn()
		{
		}

		public Pawn( Client client ) : this()
		{
			Clothing.LoadFromClient( client );
		}

		public override void Respawn()
		{
			Position = Terry.Instance.Position;
			Rotation = Terry.Instance.Rotation;

			SetModel( "models/citizen/citizen.vmdl" );
			Clothing.DressEntity( this );

			Camera = new WRCamera();
			Controller = new WRController();
			Animator = new WRAnimator();
		}
	}
}
