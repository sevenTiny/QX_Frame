using System;
using System.Windows.Forms;
using QX_Frame.App.Form;
using QX_Frame.Data.Service;
using QX_Frame.Data.Entities;
using QX_Frame.Data.QueryObject;

namespace QX_Frame.FormApp
{
    public partial class Form1 : FormBase
    {
        public Form1()
        {
            new ClassRegisters();// class register 
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }
    }
}
