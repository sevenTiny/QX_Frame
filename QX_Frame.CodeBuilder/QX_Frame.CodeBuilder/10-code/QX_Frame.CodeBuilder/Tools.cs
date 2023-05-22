using QX_Frame.Bantina;
using System;
using System.Text;
using System.Windows.Forms;

namespace CSharp_FlowchartToCode_DG
{
    public partial class ToolsForm : Form
    {
        public ToolsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(default(Guid).ToString());
            builder.Append("\n");
            builder.Append(Guid.NewGuid().ToString());
            builder.Append("\n");
            builder.Append(DateTime_Helper_DG.GetCurrentTimeStamp().ToString());
            builder.Append("\n");
            builder.Append(DateTime_Helper_DG.Get_DateTime_Now_24HourType());
            builder.Append("\n");
            builder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            builder.Append("\n");

            richTextBox_OutPut.Text = builder.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox_OutPut.Text = Encrypt_Helper_DG.MD5_Encrypt(richTextBox_Input.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Encrypt_Helper_DG.RSA_Keys keys = Encrypt_Helper_DG.RSA_GetKeys();

            StringBuilder builder = new StringBuilder();

            builder.Append( "RSA_PublicKey ----- ----- ----- ----- ----->\n\n");
            builder.Append(keys.PublicKey);
            builder.Append("\n\nRSA_PrivateKey ----- ----- ----- ----- ----->\n\n");
            builder.Append(keys.PrivateKey);

            richTextBox_OutPut.Text = builder.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            richTextBox_OutPut.SelectAll();
            richTextBox_OutPut.Copy();
            label3.Text = "copied !";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox_OutPut.Text = richTextBox_Input.Text.Length.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox_OutPut.Text = Encrypt_Helper_DG.Base64_Encode(richTextBox_Input.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox_OutPut.Text = Encrypt_Helper_DG.Base64_Decode(richTextBox_Input.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox_OutPut.Text = Info.CopyRight;
        }
    }
}
