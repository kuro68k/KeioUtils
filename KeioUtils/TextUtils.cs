using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keio.Utils
{
	public class TextUtils
	{
		// compact whitespace so that multiple spaces are reduce to just one
		public static string CompactWhitespace(string s)
		{
			string comp = "";
			bool last_char_was_space = false;

			for (int i = 0; i < s.Length; i++)
			{
				if (!Char.IsWhiteSpace(s[i]))
				{
					comp += s[i];
					last_char_was_space = false;
				}
				else
				{
					if (!last_char_was_space)
						comp += ' ';
					last_char_was_space = true;
				}
			}

			return comp;
		}

		// checks if a string is a C format number (decminal, 0b, 0x)
		public static bool IsCNumeric(string s)
		{
			s = s.ToUpper().Trim();
			if (s.StartsWith("0X"))
			{
				for (int i = 2; i < s.Length; i++)
				{
					if (!Char.IsDigit(s[i]) &&
						s[i] != 'A' && s[i] != 'B' && s[i] != 'C' && s[i] != 'D' && s[i] != 'E' && s[i] != 'F')
						return false;
				}
			}
			else if (s.StartsWith("0B"))
			{
				for (int i = 2; i < s.Length; i++)
				{
					if (s[i] != '0' && s[i] != '1')
						return false;
				}
			}
			else if (s.StartsWith("0"))
			{
				for (int i = 1; i < s.Length; i++)
				{
					if (!Char.IsNumber(s[i]) &&
						s[i] != '9')
						return false;
				}
			}
			else
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (!Char.IsNumber(s[i]))
						return false;
				}
			}
			return true;
		}

		// parse a C format int (decimal, 0b, 0x)
		public static bool ParseInt(string s, out int i)
		{
			i = 0;
			int _base = 10;
			
			s.Trim();
			s = s.ToUpper();

			if (s.StartsWith("0B"))
			{
				_base = 2;
				s = s.Substring(2);
			}
			else if (s.StartsWith("0X"))
			{
				_base = 16;
				s = s.Substring(2);
			}

			try
			{
				i = Convert.ToInt32(s, _base);
			}
			catch
			{
				return false;
			}
			
			return true;
		}

		// pad a string at regular intervals
		public static string PadString(string s, string padding, int spacing)
		{
			string output = "";
			int i = 0;
			while (i < s.Length)
			{
				output += s.Substring(i, spacing);
				i += spacing;
				if (i < s.Length)
					output += padding;
			}
			return output;
		}

		public static string PadString(string s, char padding, int spacing)
		{
			return PadString(s, padding.ToString(), spacing);
		}

		// convert a number to binary string
		public static string ToBinString(UInt64 u64, int digits)
		{
			if (digits > 64)
				throw new Exception("Too many digits (max 64)");

			UInt64 mask = 1;
			var sb = new StringBuilder(new string('0', digits));
			for (int i = 0; i < digits; i++)
			{
				if ((u64 & mask) != 0)
					sb[digits - i - 1] = '1';
				mask <<= 1;
			}
			return sb.ToString();
		}

		public static string ToBinString(UInt64 v)
		{
			return ToBinString((UInt64)v, 64);
		}

		public static string ToBinString(UInt32 v)
		{
			return ToBinString((UInt64)v, 32);
		}

		public static string ToBinString(UInt16 v)
		{
			return ToBinString((UInt64)v, 16);
		}

		public static string ToBinString(byte v)
		{
			return ToBinString((UInt64)v, 8);
		}

		public static string ToBinString(Int64 v)
		{
			return ToBinString((UInt64)v, 64);
		}

		public static string ToBinString(Int32 v)
		{
			return ToBinString((UInt64)v, 32);
		}

		public static string ToBinString(Int16 v)
		{
			return ToBinString((UInt64)v, 16);
		}

		public static string ToBinString(sbyte v)
		{
			return ToBinString((UInt64)v, 8);
		}
	}
}
