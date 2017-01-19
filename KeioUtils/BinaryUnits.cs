using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Keio.Utils
{
	public class BinaryUnits
	{
		public static readonly string[] Prefixes = { "K", "M", "G", "T", "P", "E", "Z", "Y" };

		public static string ToBinaryUnits(long v, string format = "F3", double margin = 1.0)
		{
			string s = "";

			int idx = -1;
			for (int exp = 0; exp <= 8; exp ++)
			{
				BigInteger m = (BigInteger)(Math.Pow(1024, (double)exp + 1) * margin);		// calculate every time to avoid precision/rounding errors
				if (v <= m)
				{
					m = (long)Math.Pow(1024, (double)exp);
					if (idx >= 0)
					{
						double d = (double)v / (double)m;
						s = d.ToString(format) + Prefixes[idx];
					}
					else
						s = v.ToString("D");
					return s;
				}
				idx++;
			}

			return v.ToString(format);
		}

		public static bool ParseBinaryUnitNumber(string s, out double d)
		{
			d = 0;

			double multiplier = 1;

			// find suffix
			bool number_found = false;
			string suffix = null;
			for (int i = 0; i < s.Length; i++)
			{
				if (char.IsDigit(s[i]) || (s[i] == '.'))
				{
					number_found = true;
				}
				else
				{
					suffix = s.Substring(i).Trim();
					s = s.Substring(0, i);
				}
			}
			if (!number_found)	// no numeric part
				return false;

			if ((suffix != null) && (suffix.Length > 1))	// there is a suffix but it is too long
				return false;

			if (suffix != null)
			{
				int idx = 0;
				for (int exp = 1; exp <= 8; exp ++)
				{
					if (suffix == Prefixes[idx])
					{
						multiplier = Math.Pow(1024, (double)exp);
						break;
					}
					idx++;
				}
			}

			if (double.TryParse(s, out d))
			{
				d *= multiplier;
				return true;
			}

			return false;
		}
	}
}
