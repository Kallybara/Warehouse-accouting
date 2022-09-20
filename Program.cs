using System;
using System.Windows.Forms;

namespace Warehouse_accouting
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Authorization());
		}
	}
}
