using Sandbox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WordRamble.GameLogic
{
	public class GameDictionary : BaseNetworkable, INetworkSerializer
	{
		public string Ident { get; internal set; }
		public string Name { get; internal set; }
		public string Description { get; internal set; }
		public string Glyph { get; internal set; }
		public float H { get; internal set; }
		public float S { get; internal set; }
		public float V { get; internal set; }
		public int Size { get; internal set; }
		public HashSet<string> Words { get; internal set; }

		public GameDictionary() { }

		private GameDictionary( string ident, string name, string description, string glyph, float h, float s, float v, int size )
		{
			Ident = ident;
			Name = name;
			Description = description;
			Glyph = glyph;
			H = h;
			S = s;
			V = v;
			Size = size;
			Words = null;
		}

		public static async Task<GameDictionary> CreateAsync( StreamReader sr, string ident )
		{
			return new GameDictionary(
				ident,
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

		public void Read( ref NetRead read )
		{
			Name = read.ReadString();
			Description = read.ReadString();
			Glyph = read.ReadString();
			H = (float)read.ReadObject();
			S = (float)read.ReadObject();
			V = (float)read.ReadObject();
			Size = (int)read.ReadObject();
		}

		public void Write( NetWrite write )
		{
			write.WriteUtf8( Name );
			write.WriteUtf8( Description );
			write.WriteUtf8( Glyph );
			write.WriteObject( H );
			write.WriteObject( S );
			write.WriteObject( V );
			write.WriteObject( Size );
		}
	}
}
