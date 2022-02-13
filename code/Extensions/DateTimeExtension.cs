using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordRamble.Extensions
{
	public static class DateTimeExtension
	{
		public static DateTime FromUnixSeconds(long s)
		{
			return new DateTime( 1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc ).AddSeconds( s );
		}
	}
}
