using System;
using System.Windows.Forms;

namespace SzondiTest
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class SzondiTestProgram
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			// new MainForm().Show();
			// SzondiCounts.TestMethodWriteReportToFile();
			Application.Run(new SerieForm(1, new SzondiCounts()));
		}
	}
}
