using Sandbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WordRamble.Extensions;

namespace WordRamble.GameLogic
{
	public static class DataManager
	{
		public struct Guess
		{
			public bool Won;
			public uint Attempt;
			public string WordOfTheDay;
			public DateTime TimeStamp; // Unix Time
			public Tuple<TileType, char>[,] Letters; // 6 attempts by 5 characters

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
						TimeStamp = DateTimeExtension.FromUnixSeconds(long.Parse( await sr.ReadLineAsync() )),
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
			return DictionaryPath( d.Name );
		}

		public static string DictionaryPath( string dictionaryName )
		{
			return $"{new Uri( Game.Instance.ServerConnection.BaseUrl ).Host.FastHash()}-{dictionaryName.Normalize().Replace('/', '-')}";
		}

		public static IEnumerable<DateTime> GetEntries( GameDictionary d )
		{
			var dp = DictionaryPath( d );
			FileSystem.Data.CreateDirectory( dp );

			List<DateTime> entries = new();
			foreach ( var f in FileSystem.Data.FindFile( dp, "*.guess" ) )
			{
				try
				{
					var e = DateTimeExtension.FromUnixSeconds(long.Parse( f[..^".guess".Length] ));
					entries.Add( e );
				} catch ( Exception e )
				{
					Log.Warning( $"Invalid entry name {f} ({e})" );
				}
			}

			return entries;
		}

		public static void AddEntry( string dictionaryName, Guess g )
		{
			var dp = DictionaryPath( dictionaryName );
			FileSystem.Data.CreateDirectory( dp );

			throw new Exception( "TODO: ask to whitelist StreamWriter" );
			//FileSystem.Data.OpenWrite( $"{dp}/" );
		}
	}
}
