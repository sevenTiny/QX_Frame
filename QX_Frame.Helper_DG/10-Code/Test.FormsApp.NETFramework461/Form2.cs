using System;
using System.Windows.Forms;

namespace Test.FormsApp
{
    public delegate void ChangeForm1TextBoxValue(string txt);
    public partial class Form2 : Form
    {
        public event ChangeForm1TextBoxValue ChangeTextBoxValue;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ChangeTextBoxValue(this.textBox1.Text);//执行委托实例  
        }
    }
}
