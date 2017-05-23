using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keio.Utils
{
	public class SIUnits
	{
		public static bool UseLatin = true;
		public static readonly string[] Prefixes = { "y", "z", "a", "f", "p", "n", "μ", "m", "", "k", "M", "G", "T", "P", "E", "Z", "Y" };
		public static readonly string[] PrefixesLatin = { "y", "z", "a", "f", "p", "n", "u", "m", "", "k", "M", "G", "T", "P", "E", "Z", "Y" };

		// convert a number to a string with SI suffix
		public static string ToSIUnits(int v, double margin = 1.2)
		{
			return ToSIUnits((double)v, "F0", margin);
		}

		public static string ToSIUnits(uint v, double margin = 1.2)
		{
			return ToSIUnits((double)v, "F0", margin);
		}

		public static string ToSIUnits(short v, double margin = 1.2)
		{
			return ToSIUnits((double)v, "F0", margin);
		}

		public static string ToSIUnits(ushort v, double margin = 1.2)
		{
			return ToSIUnits((double)v, "F0", margin);
		}

		public static string ToSIUnits(float v, string format = "F2", double margin = 1.2)
		{
			return ToSIUnits((double)v, format, margin);
		}

		public static string ToSIUnits(double v, string format = "F2", double margin = 1.2, string minimum_unit = "")
		{
			string s = "";
			string[] SelectedPrefixes = PrefixesLatin;
			if (!UseLatin)
				SelectedPrefixes = Prefixes;

			// sanity check
			if (!string.IsNullOrEmpty(minimum_unit) &&
				(!SelectedPrefixes.Contains(minimum_unit.ToString())) &&
				(minimum_unit != "u"))
				throw new ArgumentException("Unknown minimum unit.");

			// handle the special zero case
			if (v == 0)
			{
				if (minimum_unit == string.Empty)
					return v.ToString(format);
				return v.ToString(format) + minimum_unit;
			}

			// handle negative numbers by storing a -ve flag and processing as positive
			bool negative = false;
			if (v < 0)
			{
				negative = true;
				v = 0 - v;
			}

			// minimum prefixes
			bool minimum_found = false;
			if (string.IsNullOrEmpty(minimum_unit))
				minimum_found = true;
			
			int idx = 0;
			for (int exp = -21; exp <= 24; exp += 3)
			{
				// skip over prefixes if they are below the selected minimum
				if (SelectedPrefixes[idx] == minimum_unit)
					minimum_found = true;
				if (!minimum_found)
				{
					idx++;
					continue;
				}

				double m = Math.Pow(10, (double)exp) * margin;		// calculate every time to avoid precision/rounding errors
				if (v <= m)
				{
					m = Math.Pow(10, (double)exp - 3);
					s = (v / m).ToString(format) + SelectedPrefixes[idx];
					if (negative)
						s = "-" + s;
					return s;
				}
				idx++;
			}

			if (negative)
				v = 0 - v;
			return v.ToString(format);
		}

		// parse a string as a base 10 number with optional SI suffix
		public static bool ParseSINumber(string s, out int i)
		{
			i = 0;
			double d;
			if (!ParseSINumber(s, out d))
				return false;
			if (d != (int)d)	// not an integer
				return false;
			i = (int)d;
			return true;
		}

		public static bool ParseSINumber(string s, out float f)
		{
			f = 0;
			double d;
			if (!ParseSINumber(s, out d))
				return false;
			f = (float)d;
			return true;
		}
		
		public static bool ParseSINumber(string s, out double d)
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
				for (int exp = -21; exp <= 24; exp += 3)
				{
					if (suffix == Prefixes[idx])
					{
						multiplier = Math.Pow(10, (double)exp - 3);		// calculate every time to avoid precision/rounding errors
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
