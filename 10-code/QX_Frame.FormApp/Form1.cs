using System;
using System.Windows.Forms;
using QX_Frame.App.Form;
using QX_Frame.Data.Service.QX_Frame;
using QX_Frame.Data.Entities.QX_Frame;
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
            using (var fact = Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
                tb_userAccount userAccount = channel.QuerySingle(new UserAccountQueryObject()).Cast<tb_userAccount>();
                MessageBox.Show(userAccount.loginId);
            }

        }
    }
}
