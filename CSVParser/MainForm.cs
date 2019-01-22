using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSVParser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeLabel();
        }
        void InitializeLabel ()
        {
			string read = FileReader.Read("TASK/file01.csv");
			string[][] file1 = read.ParseLines();
			read = FileReader.Read("TASK/file02.csv");
			string[][] file2 = read.ParseLines();

			string[][] output = ParserCSV.ParseValuesCSV (file1, file2);


			int sizeX = 100;
			int sizeY = 50;

			SetBounds (0, 0, sizeX * output [0].Length, sizeY * (output.Length + 1));

			int y = 0;
            foreach (var vs in output)
            {
				int x = 0;
                foreach (var v in vs)
                {
					new TControl<Label>(this, v, new Point(x * sizeX, y * sizeY));
					x++;
                }
				y++;
            }
        }
    }
	class TControl<T> where T : Control
    {
		T component;

        public TControl (Form form, string text, Point position)
        {
			component = (T)typeof(T).GetConstructor (new Type[0]).Invoke (new object[0]);
            component.AutoSize = true;
            component.Text = text;
			component.SetBounds (position.X, position.Y, component.Bounds.Width, component.Bounds.Height);
			form.Controls.Add (component);
        }
    }
    class FileReader
    {
        public static string Read (string name)
        {
            string path = Application.StartupPath + '/' + name;
            StreamReader reader = new StreamReader(path);
            string resoult = reader.ReadToEnd();
            reader.Close();
            return resoult;
        }
    }
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
