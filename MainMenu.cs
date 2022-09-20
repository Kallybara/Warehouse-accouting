using System;
using System.Windows.Forms;

namespace Warehouse_accouting
{
	public partial class MainMenu : Form
	{
		public MainMenu()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Arrival ar = new Arrival();
			ar.Show();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			Warehouses_show wS = new Warehouses_show();
			wS.Show();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			MoveProducts mp = new MoveProducts();
			mp.Show();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Writeoff wo = new Writeoff();
			wo.Show();
		}

		private void button5_Click_1(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void MainMenu_Load(object sender, EventArgs e)
		{

		}
	}
}
