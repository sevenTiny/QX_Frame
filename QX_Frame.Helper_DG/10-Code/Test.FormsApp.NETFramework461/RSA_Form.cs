using QX_Frame.Helper_DG;
using System;
using System.Windows.Forms;

namespace Test.FormsApp
{
    public partial class RSA_Form : Form
    {
        public RSA_Form()
        {
            InitializeComponent();
        }

        //Generate RSA_Keys
        private void button1_Click(object sender, EventArgs e)
        {
            Encrypt_Helper_DG.RSA_Keys rsaKey = Encrypt_Helper_DG.RSA_GetKeys();
            textBox1.Text = rsaKey.PublicKey;
            textBox2.Text = rsaKey.PrivateKey;
        }

        //public Encrypt
        private void button2_Click(object sender, EventArgs e)=> richTextBox2.Text = Encrypt_Helper_DG.RSA_Encrypt(richTextBox1.Text, textBox1.Text.Trim());

        //Public Decrypt
        private void button3_Click(object sender, EventArgs e)=>richTextBox1.Text = Encrypt_Helper_DG.RSA_Decrypt(richTextBox2.Text, textBox2.Text.Trim());

        //Private Encrypt
        private void button5_Click(object sender, EventArgs e)=> richTextBox4.Text = Encrypt_Helper_DG.RSA_Encrypt(richTextBox3.Text, textBox2.Text.Trim());

        //Private Decrypt
        private void button4_Click(object sender, EventArgs e)=> richTextBox3.Text = Encrypt_Helper_DG.RSA_Decrypt(richTextBox4.Text, textBox2.Text.Trim());

        //RSA_Signature
        private void button6_Click(object sender, EventArgs e) => richTextBox6.Text = Encrypt_Helper_DG.RSA_GetHashDescription(richTextBox5.Text);

        //RSA_SignatureVerify
        private void button7_Click(object sender, EventArgs e) => richTextBox5.Text = Encrypt_Helper_DG.RSA_Signature(textBox2.Text.Trim(), richTextBox6.Text);

        private void button8_Click(object sender, EventArgs e)=> MessageBox.Show(Encrypt_Helper_DG.RSA_SignatureVerify(textBox1.Text.Trim(), richTextBox6.Text, richTextBox5.Text) == true ? "validate signature success" : "validate signature faild !!!");
    }
}
