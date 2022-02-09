﻿using Sandbox;
using Sandbox.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WordRamble.GameLogic
{
	public partial class ServerConnection : BaseNetworkable
	{
		public enum ServerConnectionState
		{
			Invalid,
			FindingServer,
			GettingDictionaries,
			Fail,
			Done
		}

		[Net, Change( nameof( OnStateChanged ) )]
		public ServerConnectionState State { get; protected set; }

		[Net]
		public Dictionary<string, GameDictionary> Dictionaries { get; internal set; } = new();

		string baseUrl;

		public ServerConnection()
		{
			Event.Register( this );

			SetState( ServerConnectionState.Invalid );
		}

		protected void SetState( ServerConnectionState state )
		{
			State = state;
			Event.Run( "wr.loading", state );
		}

		public void OnStateChanged( ServerConnectionState oldState, ServerConnectionState state )
		{
			Event.Run( "wr.loading", state );
		}

		public async void Connect()
		{
			Host.AssertServer();

			Stream f;
			try
			{
				f = FileSystem.Mounted.OpenRead( "data/servers.txt.json" ); // FIXME: cring, addon upload ignores txt
			}
			catch ( Exception e )
			{
				Log.Error( $"Fatal: cannot find servers.txt ({e})" );
				SetState( ServerConnectionState.Fail );
				Game.Close();
				return;
			}

			using var sr = new StreamReader( f );
			try
			{
				var foundServer = false;
				SetState( ServerConnectionState.FindingServer );
				while ( !foundServer && await sr.ReadLineAsync() is string s )
				{
					baseUrl = s;
					try
					{
						await GetDictionaries();
						foundServer = true;
					}
					catch ( Exception e )
					{
						Log.Info( $"Failed to connect to \"{baseUrl}\", trying another one..." );
#if DEBUG
						Log.Warning( $"URL {baseUrl}/api/dictionary: {e}" );
#endif
					}
				}

				if ( !foundServer )
					throw new Exception();
			}
			catch ( Exception )
			{
				Log.Error( "An error occured while finding a server" );
				SetState( ServerConnectionState.Fail );
				Game.Close();
				return;
			}

			f.Close();

			SetState( ServerConnectionState.Done );
		}

		public async Task GetDictionaries()
		{
			var result = await new Http( new Uri( $"{baseUrl}/api/dictionary" ) ).GetStringAsync();
			SetState( ServerConnectionState.GettingDictionaries );

			foreach ( var d in result.Split( '\n' ) )
			{
				Dictionaries.Add( d, await GetDictionaryDescription( d ) );
			}
		}

		public async Task<GameDictionary> GetDictionaryDescription( string ident )
		{
			using var s = await new Http( new Uri( $"{baseUrl}/api/dictionary/{ident}" ) ).GetStreamAsync();
			using var sr = new StreamReader( s );
			return await GameDictionary.CreateAsync( sr, ident );
		}

		public async Task<string> GetDictionaryWord( string ident )
		{
			using var s = await new Http( new Uri( $"{baseUrl}/api/dictionary/{ident}/word" ) ).GetStreamAsync();
			using var sr = new StreamReader( s );
			return await sr.ReadLineAsync();
		}
	}
}
