using System;
using System.Windows.Forms;

namespace CSharp_FlowchartToCode_DG
{
    public partial class DataBaseChooseForm : Form
    {
        MainForm mainform;
        public DataBaseChooseForm() => InitializeComponent();
        public DataBaseChooseForm(MainForm form)
        {
            InitializeComponent();
            this.mainform = form;
        }

        private void CommonOpen(Action action)
        {
            this.Hide();
            action();
            this.Close();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)=> CommonOpen(() => { new MicrosoftSqlServerLoginForm(this.mainform).ShowDialog(); });

        private void button2_Click(object sender, EventArgs e) => CommonOpen(() => { new MySqlLoginForm(this.mainform).ShowDialog(); });
    }
}
