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

			CmdArgs argProcessor = new CmdArgs() {
				{ new CmdArgument("f,flag", ArgType.Flag, required: true) },
				{ new CmdArgument("c,count", ArgType.Counter) },
				{ new CmdArgument("d,double", ArgType.Double) },
				{ new CmdArgument("i,int", ArgType.Int) },
				{ new CmdArgument("s,string", ArgType.String, assign: (dynamic d) => { inputFileName = (string)d; }) },
				{ new CmdArgument("", ArgType.String, assign: (dynamic d) => { outputFileName = (string)d; }) }
			};
			
			string[] a = { "remainder1", "-f", "-c", "-c", "-s", "string abcdefg", "remainder2", "-double", "3.141593", "c", "-c", "-i=0x10" };
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

			Console.WriteLine("inputFileName:\t" + inputFileName);
			Console.WriteLine("outputFileName:\t" + outputFileName);
			Console.WriteLine();

			Console.WriteLine(SIUnits.ToSIUnits(123456789));
			Console.WriteLine(SIUnits.ToSIUnits(123456789.0123456789));
			Console.WriteLine(SIUnits.ToSIUnits(0.001));
			Console.WriteLine(SIUnits.ToSIUnits(0.000001));
			Console.WriteLine(SIUnits.ToSIUnits(0.000000001, "F0"));

			double v;
			SIUnits.ParseSINumber("1.2k", out v);
			Console.WriteLine(v.ToString());
			Console.WriteLine();

			Console.WriteLine(BinaryUnits.ToBinaryUnits(256) + "B");
			Console.WriteLine(BinaryUnits.ToBinaryUnits(1024) + "B");
			Console.WriteLine(BinaryUnits.ToBinaryUnits((1024 * 1024 * 2) + 25323) + "B");
			Console.WriteLine(BinaryUnits.ToBinaryUnits(1234567890123456789) + "B");
			Console.WriteLine();
		}
	}
}
