using Sandbox;
using Sandbox.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WordRamble.GameLogic
{
	public class ServerConnection
	{
		public enum ServerConnectionState
		{
			Invalid,
			FindingServer,
			GettingDictionaries,
			Fail,
			Done
		}

		public ServerConnectionState State
		{
			get
			{
				return state;
			}
			protected set
			{
				state = value;
				Event.Run( "wr.loading", state );
			}
		}

		public static ServerConnection Instance
		{
			get
			{
				if ( instance == null )
					instance = new();
				return instance;
			}
		}

		public Dictionary<string, GameDictionary> Dictionaries = new();

		static ServerConnection instance;
		ServerConnectionState state;
		string baseUrl;

		public ServerConnection()
		{
			Event.Register( this );

			State = ServerConnectionState.Invalid;
		}

		public async void Connect()
		{
			Stream f;
			try
			{
				f = FileSystem.Mounted.OpenRead( "config/servers.txt" );
			}
			catch ( Exception e )
			{
				Log.Error( $"Fatal: cannot find servers.txt ({e})" );
				State = ServerConnectionState.Fail;
				Local.Client.Kick();
				return;
			}

			State = ServerConnectionState.FindingServer;

			using var sr = new StreamReader( f );
			try
			{
				var foundServer = false;
				State = ServerConnectionState.FindingServer;
				while ( !foundServer && await sr.ReadLineAsync() is string baseUrl )
				{
					foundServer = await GetDictionaries();
					if ( !foundServer )
					{
						Log.Info( $"Failed to connect to \"{baseUrl}\", trying another one..." );
					}
				}

				if ( !foundServer )
					throw new Exception();
			}
			catch ( Exception )
			{
				Log.Error( "An error occured while finding a server" );
				State = ServerConnectionState.Fail;
				Local.Client.Kick();
				return;
			}

			f.Close();

			State = ServerConnectionState.Done;
		}

		public async Task<bool> GetDictionaries()
		{
			try
			{
				var result = await new Http( new Uri( $"{baseUrl}/api/dictionary" ) ).GetStringAsync();
				State = ServerConnectionState.GettingDictionaries;
				foreach ( var d in result.Split( '\n' ) )
				{
					Dictionaries.Add( d, await GetDictionaryDescription( d ) );
				}
			}
			catch ( Exception e )
			{
				Log.Error( $"{e}" );
				return false;
			}

			return true;
		}

		public async Task<GameDictionary> GetDictionaryDescription( string ident )
		{
			using var s = await new Http( new Uri( $"{baseUrl}/api/dictionary/{ident}" ) ).GetStreamAsync();
			using var sr = new StreamReader( s );
			return await GameDictionary.CreateAsync( sr );
		}
	}
}
