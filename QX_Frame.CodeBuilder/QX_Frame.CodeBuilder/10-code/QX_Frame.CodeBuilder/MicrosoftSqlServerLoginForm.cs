using CSharp_FlowchartToCode_DG.Entities;
using QX_Frame.Bantina;
using QX_Frame.Bantina.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CSharp_FlowchartToCode_DG
{

    public partial class MicrosoftSqlServerLoginForm : Form
    {
        MainForm mainForm;

        private static string dataBase;
        private static string authentication;
        private static string loginId;
        private static string pwd;

        public MicrosoftSqlServerLoginForm()
        {
            InitializeComponent();
        }
        public MicrosoftSqlServerLoginForm(MainForm form)
        {
            InitializeComponent();
            this.mainForm = form;
            //CheckConnectTypeChoose
            CheckConnectTypeChoose();
            //init configuration

            mainForm.button7.Enabled = true;//SqlServerStatement
            mainForm.button24.Enabled = false;//MySqlStatement
            mainForm.button30.Enabled = false;//OracleSqlStatement

            ReadConfiguration();
        }

        //cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        //connect button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.treeView1.Nodes[0].Nodes.Clear();//clear nodes

                dataBase = comboBox2.Text.Trim();
                authentication = comboBox3.Text.Trim();
                loginId = comboBox4.Text.Trim();
                pwd = textBox1.Text.Trim();

                GetDataBaseInfo();
                WriteConfiguration();

                //Get DataBase Information in background
                Task_Helper_DG.TaskRun(() =>
                {
                    GetDataBaseInfoBack();
                });

                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReadConfiguration()
        {
            string[] serverNameArray = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "sqlserver", "ServerName", ".").Split(',');
            comboBox2.Text = serverNameArray[0];
            comboBox2.Items.Clear();
            foreach (var item in serverNameArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    comboBox2.Items.Add(item);
                }
            }
            comboBox3.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "sqlserver", "Authentication", "Windows Authentication");
            string[] saLoginArray = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "sqlserver", "SaLogin", "sa").Split(',');
            comboBox4.Items.Clear();
            foreach (var item in saLoginArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    comboBox4.Items.Add(item);
                }
            }
            if (comboBox3.Text.Equals("SQL Server Authentication"))
            {
                comboBox4.Text = saLoginArray[0];
                textBox1.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "sqlserver", "SaPassword", "123456");
            }
        }
        private void WriteConfiguration()
        {
            //保存5条信息，默认取第一条，新登录的顶到第一条，删除最后一条，逗号分隔 1,2,3,'',''
            string[] serverNameArray = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "sqlserver", "ServerName", ".").Split(',');
            string[] newServerNameArray = new string[] { comboBox2.Text, "", "", "", "" };//save 5 lines
            int newServerNameIndex = 1;
            foreach (var item in serverNameArray)
            {
                if (!newServerNameArray.Contains(item))
                {
                    newServerNameArray[newServerNameIndex] = item;
                    newServerNameIndex++;
                }
            }
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "sqlserver", "ServerName", string.Join(",", newServerNameArray));
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "sqlserver", "Authentication", comboBox3.Text);
            if (checkBox1.Checked)
            {
                string[] saLoginArray = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "sqlserver", "SaLogin", ".").Split(',');
                string[] newSaLoginArray = new string[] { comboBox4.Text, "", "", "", "" };//save 5 lines
                int newsaLoginIndex = 1;
                foreach (var item in saLoginArray)
                {
                    if (!newSaLoginArray.Contains(item))
                    {
                        newSaLoginArray[newsaLoginIndex] = item;
                        newsaLoginIndex++;
                    }
                }
                IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "sqlserver", "SaLogin", string.Join(",", newSaLoginArray));
                IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "sqlserver", "SaPassword", textBox1.Text);
            }
        }
        //get database info fill mainForm.TreeView1
        private void GetDataBaseInfo()
        {
            if (authentication.Equals("Windows Authentication"))
            {
                CommonVariables.SetCurrentDbConnection($"Data Source={dataBase};Initial Catalog=master;Integrated Security = True", Opt_DataBaseType.SqlServer);
            }
            else if (authentication.Equals("SQL Server Authentication"))
            {
                CommonVariables.SetCurrentDbConnection($"Data Source={dataBase};Initial Catalog=master;Persist Security Info=True; User ID={loginId};Password={pwd};", Opt_DataBaseType.SqlServer);
            }

            string sql = "select name from sys.databases where database_id > 4";//查询sqlserver中的非系统库

            DataTable dataTable = Db_Helper_DG.ExecuteDataTable(sql);

            TreeNode grand = new TreeNode(dataBase);//添加节点服务器地址
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
                string sqlTable = $"use [{root.Name}] SELECT name FROM sysobjects WHERE xtype = 'U' AND OBJECTPROPERTY (id, 'IsMSShipped') = 0 and name <> 'sysdiagrams'";
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

            if (authentication.Equals("Windows Authentication"))
            {
                CommonVariables.SetCurrentDbConnection($"Data Source={dataBase};Initial Catalog=master;Integrated Security = True", Opt_DataBaseType.SqlServer);
            }
            else if (authentication.Equals("SQL Server Authentication"))
            {
                CommonVariables.SetCurrentDbConnection($"Data Source={dataBase};Initial Catalog=master;Persist Security Info=True; User ID={loginId};Password={pwd};", Opt_DataBaseType.SqlServer);
            }

            string sql = "select name from sys.databases where database_id > 4";//查询sqlserver中的非系统库

            DataTable dataTable = Db_Helper_DG.ExecuteDataTable(sql);

            ServerInfo serverInfo = new ServerInfo { ServerName = dataBase };

            List<DataBaseInfo> dataBaseInfos = new List<DataBaseInfo>();

            foreach (DataRow row in dataTable.Rows)
            {
                string dbName = row["name"].ToString();
                DataBaseInfo dataBaseInfo = new DataBaseInfo { DataBaseName = dbName };

                //获取表名
                List<TableInfo> tableInfos = new List<TableInfo>();

                string sqlTable = $"use [{dbName}] SELECT name FROM sysobjects WHERE xtype = 'U' AND OBJECTPROPERTY (id, 'IsMSShipped') = 0 and name <> 'sysdiagrams'";
                DataTable dt2 = Db_Helper_DG.ExecuteDataTable(sqlTable);
                string[] tableNameArray = new string[dt2.Rows.Count];
                foreach (DataRow row2 in dt2.Rows)
                {
                    tableInfos.Add(new TableInfo
                    {
                        TableName = row2[0].ToString(),
                        FieldInfos = Db_Helper_DG.ExecuteList<FieldInfo>($@"use [{dbName}] select syscolumns.name as Field ,systypes.name as FieldType , syscolumns.length as Length,syscolumns.isnullable as Nullable, sys.extended_properties.value as Description  ,IsPK = Case  when exists ( select 1 from sysobjects  inner join sysindexes  on sysindexes.name = sysobjects.name  inner join sysindexkeys  on sysindexes.id = sysindexkeys.id  and  sysindexes.indid = sysindexkeys.indid  where xtype='PK'  and parent_obj = syscolumns.id and sysindexkeys.colid = syscolumns.colid ) then 1 else 0 end ,IsIdentity = Case syscolumns.status when 128 then 1 else 0 end  from syscolumns inner join systypes on(  syscolumns.xtype = systypes.xtype and systypes.name <>'_default_' and systypes.name<>'sysname'  ) left outer join sys.extended_properties on  ( sys.extended_properties.major_id=syscolumns.id and minor_id=syscolumns.colid  ) where syscolumns.id = (select id from sysobjects where name='" + row2[0].ToString() + "') order by syscolumns.colid "),
                        FieldInfosTable = Db_Helper_DG.ExecuteDataTable($@"use [{dbName}] select syscolumns.name as Field ,systypes.name as FieldType , syscolumns.length as Length,syscolumns.isnullable as Nullable, sys.extended_properties.value as Description  ,IsPK = Case  when exists ( select 1 from sysobjects  inner join sysindexes  on sysindexes.name = sysobjects.name  inner join sysindexkeys  on sysindexes.id = sysindexkeys.id  and  sysindexes.indid = sysindexkeys.indid  where xtype='PK'  and parent_obj = syscolumns.id and sysindexkeys.colid = syscolumns.colid ) then 1 else 0 end ,IsIdentity = Case syscolumns.status when 128 then 1 else 0 end  from syscolumns inner join systypes on(  syscolumns.xtype = systypes.xtype and systypes.name <>'_default_' and systypes.name<>'sysname'  ) left outer join sys.extended_properties on  ( sys.extended_properties.major_id=syscolumns.id and minor_id=syscolumns.colid  ) where syscolumns.id = (select id from sysobjects where name='" + row2[0].ToString() + "') order by syscolumns.colid ")

                    });
                }
                dataBaseInfo.Tables = tableInfos;
                dataBaseInfos.Add(dataBaseInfo);
            }
            serverInfo.DataBaseInfos = dataBaseInfos;

            CommonVariables.serverInfo = serverInfo;

            CommonVariables.getServerInfoFinished = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) => CheckConnectTypeChoose();

        private void CheckConnectTypeChoose()
        {
            if (comboBox3.Text.Equals("Windows Authentication"))
            {
                this.comboBox4.Enabled = false;
                this.textBox1.Enabled = false;
                this.checkBox1.Checked = false;
                this.checkBox1.Enabled = false;
            }
            else if (comboBox3.Text.Equals("SQL Server Authentication"))
            {
                this.comboBox4.Enabled = true;
                this.textBox1.Enabled = true;
                this.checkBox1.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks for Using Microsoft SQL Server 2070");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No More Configuration");
        }
    }
}
