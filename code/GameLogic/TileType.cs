namespace WordRamble.GameLogic
{
	public enum TileType
	{
		Empty,
		Absent,
		Present,
		Correct
	}

	public static class TileTypeExtensions
	{
		public static TileType FromCharacter( char c )
		{
			switch ( c )
			{
				case 'e':
					return TileType.Empty;
				case 'a':
					return TileType.Absent;
				case 'p':
					return TileType.Present;
				case 'c':
					return TileType.Correct;

				default:
					throw new System.Exception( $"Invalid character: {c}" );
			}
		}
	}
}
