using Sandbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WordRamble.GameLogic
{
	public static class DataManager
	{
		public struct Guess
		{
			public bool Won;
			public uint Attempt;
			public string WordOfTheDay;
			public DateTime TimeStamp;
			public Tuple<TileType, char>[,] Letters; // 6 by 5

			public async static Task<Tuple<TileType, char>[,]> ReadLetters( StreamReader sr )
			{
				Tuple<TileType, char>[,] result = new Tuple<TileType, char>[6, 5];

				for ( var i = 0; i < 6; i++ )
				{
					var s = await sr.ReadLineAsync();
					for ( var j = 0; j < 5; j++ )
					{
						result[i, j] = new( TileTypeExtensions.FromCharacter( s[j * 2] ), s[j * 2 + 1] );
					}
				}

				return result;
			}

			public async static Task<Guess> Read( Stream s )
			{
				using ( StreamReader sr = new( s ) )
				{
					return new Guess()
					{
						Won = int.Parse( await sr.ReadLineAsync() ) == 1 ? true : false,
						Attempt = uint.Parse( await sr.ReadLineAsync() ),
						WordOfTheDay = await sr.ReadLineAsync(),
						TimeStamp = DateTime.Parse( await sr.ReadLineAsync() ).ToLocalTime(),
						Letters = await ReadLetters( sr )
					};
				}
			}

			public async Task Write( Stream s )
			{
				throw new Exception( "TODO: ask to whitelist StreamWriter" );
				/*using ( StreamWriter sw = new( s ) )
				{
					//
				}*/
			}
		}

		public static string DictionaryPath( GameDictionary d )
		{
			return $"{new Uri( Game.Instance.ServerConnection.BaseUrl ).Host.FastHash()}-{d.Name}".NormalizeFilename();
		}

		public static IEnumerable<int> GetEntries( GameDictionary d )
		{
			var dp = DictionaryPath( d );
			FileSystem.Data.CreateDirectory( dp );

			List<int> entries = new();
			foreach ( var f in FileSystem.Data.FindFile( dp, "*.guess" ) )
			{
				var e = f[..^".guess".Length].ToInt( -1 );
				if ( e == 0 )
					Log.Warning( $"Invalid entry name {f}" );
				else
					entries.Add( e );
			}

			return entries;
		}
	}
}
