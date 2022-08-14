namespace WordRamble.Etc
{
	public readonly struct Icon
	{
		public readonly char Outline;
		public readonly char Solid;
		
		public Icon( char i )
		{
			Outline = Solid = i;
		}

		public Icon( char o, char s )
		{
			Outline = o;
			Solid = s;
		}
	}
}
