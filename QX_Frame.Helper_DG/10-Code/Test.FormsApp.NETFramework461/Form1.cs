using System;
using System.Windows.Forms;

namespace Test.FormsApp.NETFramework461
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ChangeTextBoxValue += (txt) => { this.textBox1.Text = txt; };
            f2.Show();
        }
    }
}
