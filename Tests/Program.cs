using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keio.Utils;

namespace Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			string inputFileName = string.Empty;
			string outputFileName = string.Empty;
			bool flag = false;

			CmdArgs argProcessor = new CmdArgs(
				new List<CmdOptionArgument> {
					{ new CmdOptionArgument(ArgType.Flag,
									  option_names: "f,flag",
									  help: "Set a flag",
									  assign: (dynamic d) => { flag = (bool)d; }) },
					{ new CmdOptionArgument(ArgType.Counter,
									  option_names: "c,count",
									  help: "Increment a counter") },
					{ new CmdOptionArgument(ArgType.Double,
									  option_names: "d,double",
									  short_description: "double",
									  help: "Double precision floating point") },
					{ new CmdOptionArgument(ArgType.Int,
									  option_names: "i,int",
									  short_description: "integer",
									  help: "Basic integer argument") },
					{ new CmdOptionArgument(ArgType.String,
									  option_names: "s,string",
									  short_description: "string",
									  help: "Arbitrary string",
									  assign: (dynamic d) => { inputFileName = (string)d; }) },
				},
				new List<CmdArgument> {
					{ new CmdArgument(ArgType.String,
									  required: true,
									  short_description: "output_file",
									  assign: (dynamic d) => { outputFileName = (string)d; }) },
					{ new CmdArgument(ArgType.String,
									  required: true,
									  short_description: "input_file",
									  assign: (dynamic d) => { outputFileName = (string)d; }) },
					{ new CmdArgument(ArgType.String,
									  short_description: "not_required") }
				}
			);
			
			argProcessor.PrintHelp();
			Console.WriteLine();

			string commandLineArgs = "arg0 -f -c -c -s \"string with space\" arg1 -double 3.141593 c -c -i=0x10";
			string[] a = CmdArgs.SplitArgs(commandLineArgs);

			//string[] a = { "arg0", "-f", "-c", "-c", "-s", "string abcdefg", "arg1", "-double", "3.141593", "c", "-c", "-i=0x10" };
			//string[] remainder = argProcessor.Parse(a);
			string[] remainder;
			if (argProcessor.TryParse(a, out remainder))
			{
				Console.WriteLine("Arguments OK.");
				argProcessor.PrintArgs();
				if (remainder != null)
					Console.WriteLine(string.Join(" ", remainder));
			}
			else
				Console.WriteLine("Arguments bad.");
			Console.WriteLine();

			Console.WriteLine("inputFileName:\t" + inputFileName);
			Console.WriteLine("outputFileName:\t" + outputFileName);
			Console.WriteLine("flag:\t\t" + flag.ToString());
			Console.WriteLine();

			return;

			//                                      0         1         2         3         4         5         6         7         8         9         9         1
			//                                      012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890
			//                                      _test6, test7, test8,
			//                                      1234567890123456789_test6
			Console.WriteLine(TextUtils.WrapString("test1, test2, test3, test4, test5, 12345678901234567890test6, test7, test8,,", 20));
			Console.WriteLine();
			Console.WriteLine(TextUtils.Reformat("     test1,   test2, test3,\ttest4, test5, 12345678901234567890test6, test7, test8,,", 20));

			Console.WriteLine("123456789.0123456789\t" + SIUnits.ToSIUnits(123456789.0123456789));
			Console.WriteLine("123456789\t" + SIUnits.ToSIUnits(123456789));
			Console.WriteLine("0.001\t\t" + SIUnits.ToSIUnits(0.001));
			Console.WriteLine("0.000001\t" + SIUnits.ToSIUnits(0.000001));
			Console.WriteLine("0.000000001 F0\t" + SIUnits.ToSIUnits(0.000000001, "F0"));
			Console.WriteLine("(double)0 u\t" + SIUnits.ToSIUnits((double)0, minimum_unit: "u"));
			Console.WriteLine("-0.0007 m\t" + SIUnits.ToSIUnits(-0.0007, minimum_unit: "m"));
			Console.WriteLine("-0.0007\t\t" + SIUnits.ToSIUnits(-0.0007));
			Console.WriteLine();

			double v;
			SIUnits.ParseSINumber("1.2k", out v);
			Console.WriteLine(v.ToString());
			if (!SIUnits.ParseSINumber("0.007", out v))
				Console.WriteLine("Parse error 0.007");
			Console.WriteLine(v.ToString());
			Console.WriteLine();

			int i;
			if (!TextUtils.ParseInt("0x16DB", out i))
				Console.WriteLine("Parse error TextUtils.ParseInt(\"0x16DB\", i)");
			else
				Console.WriteLine(i.ToString());
			Console.WriteLine();

			Console.WriteLine(BinaryUnits.ToBinaryUnits(256) + "B");
			Console.WriteLine(BinaryUnits.ToBinaryUnits(1024) + "B");
			Console.WriteLine(BinaryUnits.ToBinaryUnits((1024 * 1024 * 2) + 25323) + "B");
			Console.WriteLine(BinaryUnits.ToBinaryUnits(1234567890123456789) + "B");
			Console.WriteLine();

			Console.WriteLine(TextUtils.ToBinString((UInt64)0xB515F5F5F5F5F5F5, 64));
			Console.WriteLine(TextUtils.PadString(TextUtils.ToBinString((UInt32)0xB515F5F5), ' ', 4));
			Console.WriteLine(TextUtils.ToBinString((byte)0x11));
		}
	}
}
