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
				if (v < m)
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

	}
}
