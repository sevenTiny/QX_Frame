using CSharp_FlowchartToCode_DG.Entities;
using QX_Frame.Bantina;
using QX_Frame.Bantina.Extends;
using QX_Frame.Bantina.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_FlowchartToCode_DG
{
    public partial class MySqlLoginForm : Form
    {
        MainForm mainForm;

        private static string serverName;
        private static string port;
        private static string loginId;
        private static string pwd;

        public MySqlLoginForm()
        {
            InitializeComponent();
        }
        public MySqlLoginForm(MainForm form)
        {
            InitializeComponent();
            this.mainForm = form;
            ReadConfiguration();
        }
        private void MySqlLoginForm_Load(object sender, EventArgs e)
        {
            this.button1.Focus();
        }
        private void WindowClose()
        {
            this.Close();
            this.Dispose();
        }

        private void ReadConfiguration()
        {
            textBox1.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "mysql", "ServerName", "localhost");
            textBox2.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "mysql", "Port", "3306");
            textBox3.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "mysql", "Login", "");
            textBox4.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "mysql", "Password", "");
        }
        private void WriteConfiguration()
        {
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "mysql", "ServerName", textBox1.Text.Trim());
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "mysql", "Port", textBox2.Text.Trim());
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "mysql", "Login", textBox3.Text.Trim());
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "mysql", "Password", textBox4.Text.Trim());
        }

        //Connect Test
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonVariables.SetCurrentDbConnection($"Data Source={textBox1.Text.Trim()};User ID={textBox3.Text.Trim()};Password={textBox4.Text.Trim()}", Opt_DataBaseType.MySql);

                string sql = "select COUNT(0) from information_schema.columns";//查询sqlserver中的非系统库

                if (Db_Helper_DG.ExecuteScalar(sql).ToInt() > 0)
                {
                    MessageBox.Show("Success!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connect Faild !");
            }
        }
        //Connect
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.treeView1.Nodes[0].Nodes.Clear();//clear nodes

                serverName = textBox1.Text.Trim();
                port = textBox2.Text.Trim();
                loginId = textBox3.Text.Trim();
                pwd = textBox4.Text.Trim();

                GetDataBaseInfo();

                mainForm.button7.Enabled = false;//SqlServerStatement
                mainForm.button24.Enabled = true;//MySqlStatement
                mainForm.button30.Enabled = false;//OracleSqlStatement

                WriteConfiguration();

                Task_Helper_DG.TaskRun(() =>
                {
                    GetDataBaseInfoBack();
                });

                WindowClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //cancel
        private void button2_Click(object sender, EventArgs e) => WindowClose();

        private void GetDataBaseInfo()
        {
            CommonVariables.SetCurrentDbConnection($"Data Source={serverName};User ID={loginId};Password={pwd}", Opt_DataBaseType.MySql);

            string sql = "select DISTINCT(TABLE_SCHEMA) as name from information_schema.columns";//查询sqlserver中的非系统库

            DataTable dataTable = Db_Helper_DG.ExecuteDataTable(sql);

            TreeNode grand = new TreeNode(serverName);//添加节点服务器地址
            grand.ImageIndex = 1;
            mainForm.treeView1.Nodes[0].Nodes.Add(grand);

            mainForm.comboBox3.Items.Clear();//清空SqlPower里面的数据库下拉框数据

            foreach (DataRow row in dataTable.Rows)
            {
                string dbName = row["name"].ToString();
                TreeNode root = new TreeNode(dbName);//创建节点
                root.Name = dbName;
                root.ImageIndex = 2;
                grand.Nodes.Add(root);
                TreeNode biao = new TreeNode("Tables");
                biao.Name = "Tables";
                biao.ImageIndex = 3;
                root.Nodes.Add(biao);

                mainForm.comboBox3.Items.Add(dbName);//添加SqlPower里面的数据库下拉框数据

                //获取表名
                string sqlTable = $"select DISTINCT(TABLE_NAME) from information_schema.columns where TABLE_SCHEMA='{dbName}'";
                DataTable dt2 = Db_Helper_DG.ExecuteDataTable(sqlTable);
                string[] tableNameArray = new string[dt2.Rows.Count];
                foreach (DataRow row2 in dt2.Rows)
                {
                    TreeNode biaovalue = new TreeNode(row2[0].ToString());
                    biaovalue.Name = row2[0].ToString();
                    biaovalue.ImageIndex = 4;
                    biao.Nodes.Add(biaovalue);
                }
            }
            //Nodes Expand
            grand.Expand();
            mainForm.treeView1.Nodes[0].Expand();
        }

        /// <summary>
        /// if connect succeed execute this method get info in background
        /// </summary>
        private void GetDataBaseInfoBack()
        {
            CommonVariables.getServerInfoFinished = false;

            CommonVariables.SetCurrentDbConnection($"Data Source={serverName};User ID={loginId};Password={pwd}", Opt_DataBaseType.MySql);

            string sql = "select DISTINCT(TABLE_SCHEMA) as name from information_schema.columns";//查询sqlserver中的非系统库

            DataTable dataTable = Db_Helper_DG.ExecuteDataTable(sql);

            ServerInfo serverInfo = new ServerInfo { ServerName = serverName };

            List<DataBaseInfo> dataBaseInfos = new List<DataBaseInfo>();

            foreach (DataRow row in dataTable.Rows)
            {
                string dbName = row["name"].ToString();
                DataBaseInfo dataBaseInfo = new DataBaseInfo { DataBaseName = dbName };

                //获取表名
                List<TableInfo> tableInfos = new List<TableInfo>();

                string sqlTable = $"select DISTINCT(TABLE_NAME) from information_schema.columns where TABLE_SCHEMA='{dbName}'";
                DataTable dt2 = Db_Helper_DG.ExecuteDataTable(sqlTable);
                string[] tableNameArray = new string[dt2.Rows.Count];
                foreach (DataRow row2 in dt2.Rows)
                {
                    tableInfos.Add(new TableInfo
                    {
                        TableName = row2[0].ToString(),
                        FieldInfos = Db_Helper_DG.ExecuteList<FieldInfo>($"select COLUMN_NAME as Field,DATA_TYPE as DataType,SUBSTRING_INDEX(SUBSTRING_INDEX(COLUMN_TYPE,'(',-1),')',1) as Length,iF(IS_NULLABLE='YES',1,0) as Nullable,COLUMN_COMMENT as Description,IF(COLUMN_KEY='PRI',1,0) as IsPK,(SELECT IFNULL(0,1)) as IsIdentity from information_schema.columns where TABLE_SCHEMA='{dbName}' AND TABLE_NAME='{row2[0].ToString()}'"),
                        FieldInfosTable = Db_Helper_DG.ExecuteDataTable($"select COLUMN_NAME as Field,DATA_TYPE as DataType,SUBSTRING_INDEX(SUBSTRING_INDEX(COLUMN_TYPE,'(',-1),')',1) as Length,iF(IS_NULLABLE='YES',1,0) as Nullable,COLUMN_COMMENT as Description,IF(COLUMN_KEY='PRI',1,0) as IsPK,(SELECT IFNULL(0,1)) as IsIdentity from information_schema.columns where TABLE_SCHEMA='{dbName}' AND TABLE_NAME='{row2[0].ToString()}'")
                    });
                }
                dataBaseInfo.Tables = tableInfos;
                dataBaseInfos.Add(dataBaseInfo);
            }
            serverInfo.DataBaseInfos = dataBaseInfos;

            CommonVariables.serverInfo = serverInfo;

            CommonVariables.getServerInfoFinished = true;
        }
    }
}
