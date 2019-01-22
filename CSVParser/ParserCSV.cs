using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSVParser
{
    
	public static class ParserCSV
	{
		public static string[] ParseLine (this string origin)
		{
			return origin.Split (',').Where ((string s) => !string.IsNullOrEmpty (s)).ToArray ();
		}
		public static string[][] ParseLines(this string origin)
		{
			return origin.Split('\n').Select((string s) => s.ParseLine()).ToArray();
		}
		public static string[][] ParseValuesCSV (string[][] file1, string[][] file2)
		{
			string[][] output = (from f1 in file1
				join f2 in file2 on f1.FirstOrDefault () equals f2.FirstOrDefault ()
				select f1.Concat (f2).ToArray ()).ToArray ();

			for (int i = 0; i < output.Length; i++) {
				var output1 = output[i].Take (4);
				var output2 = output[i].Skip (7).Take(3);
				output [i] = output1.Concat (output2).ToArray ();
			}
			return output;
		}
	}
}
