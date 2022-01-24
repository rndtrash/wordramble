using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WordRamble.GameLogic
{
	public struct GameDictionary
	{
		public string Name;
		public string Description;
		public string Glyph;
		public float H;
		public float S;
		public float L;
		public int Size;
		public HashSet<string> Words;

		private GameDictionary( string name, string description, string glyph, float h, float s, float l, int size )
		{
			Name = name;
			Description = description;
			Glyph = glyph;
			H = h;
			S = s;
			L = l;
			Size = size;
			Words = null;
		}

		public static async Task<GameDictionary> CreateAsync( StreamReader sr )
		{
			return new GameDictionary(
				await ReadStringAsync( sr ),
				await ReadStringAsync( sr ),
				await ReadStringAsync( sr ),
				await ReadFloatAsync( sr ),
				await ReadFloatAsync( sr ),
				await ReadFloatAsync( sr ),
				await ReadIntAsync( sr )
				);
		}

		public async void LoadDictionary( StreamReader sr )
		{
			if ( Words != null )
				return;

			Words = new( Size );
			while ( await sr.ReadLineAsync() is string word )
			{
				if ( !Words.Add( word ) )
					Log.Info( "Warning: found a repeating word." );
			}
		}

		public void DisposeDictionary()
		{
			if ( Words == null )
				return;

			Words.Clear();
			Words = null;
		}

		static string ReadString( StreamReader reader )
		{
			if ( reader.ReadLine() is not string s )
				throw new Exception( "Not a valid dictionary" );

			return s;
		}

		static float ReadFloat( StreamReader reader )
		{
			if ( reader.ReadLine() is not string s )
				throw new Exception( "Not a valid dictionary" );

			return float.Parse( s );
		}

		static int ReadInt( StreamReader reader )
		{
			if ( reader.ReadLine() is not string s )
				throw new Exception( "Not a valid dictionary" );

			return int.Parse( s );
		}

		static async Task<string> ReadStringAsync( StreamReader reader )
		{
			if ( await reader.ReadLineAsync() is not string s )
				throw new Exception( "Not a valid dictionary" );

			return s;
		}

		static async Task<float> ReadFloatAsync( StreamReader reader )
		{
			if ( await reader.ReadLineAsync() is not string s )
				throw new Exception( "Not a valid dictionary" );

			return float.Parse( s );
		}

		static async Task<int> ReadIntAsync( StreamReader reader )
		{
			if ( await reader.ReadLineAsync() is not string s )
				throw new Exception( "Not a valid dictionary" );

			return int.Parse( s );
		}
	}
}
