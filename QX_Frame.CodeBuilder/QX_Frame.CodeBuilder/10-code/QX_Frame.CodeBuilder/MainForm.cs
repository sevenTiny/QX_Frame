using CSharp_FlowchartToCode_DG.CodeCreate;
using QX_Frame.Bantina;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using QX_Frame.Bantina.Extends;
using QX_Frame.Bantina.Options;
using QX_Frame.Bantina.Validation;
using System.Linq;

namespace CSharp_FlowchartToCode_DG
{
    public partial class MainForm : Form
    {
        #region 代码编辑全局变量
        public static Dictionary<string, dynamic> CreateInfoDic = new Dictionary<string, dynamic>();         //存储全部信息的List

        string DataBaseName = "DataBase1";                    //数据库名
        string TableName = "Table1";                          //表名
        List<string> FeildName = new List<string>();          //表字段名称
        List<string> FeildType = new List<string>();          //表字段类型
        List<string> FeildIsNullable = new List<string>();    //表字段可空
        List<string> FeildLength = new List<string>();        //表字段长度
        List<string> FeildDescription = new List<string>();   //表字段说明
        List<string> FeildIsPK = new List<string>();          //表字段是否主键
        List<string> FeildIsIdentity = new List<string>();    //表字段是否自增

        string CodeTxt = "";        //代码字符串，用于输出到文件
        string dir = IO_Helper_DG.DeskTopPath;                  //获取路径
        DataTable DataBaseTable = default(DataTable); //数据库表数据DataTable

        #endregion

        public MainForm() => InitializeComponent();
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // set copyright
                label_Author.Text += Info.Author;
                label_Version.Text += Info.VersionNum;
                label_Description.Text += Info.Description;

                //set config
                comboBox2.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "config", "outputType");//output type

                //set code builder config
                // textBox3.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "usings").Replace('&','\n'); //using
                textBox2.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "namespace");//namespace
                textBox10.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "namespaceCommonPlus");//namespace
                textBox9.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "TableName");//table name
                textBox5.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "class");//class name 
                textBox7.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "ClassNamePlus");//ClassExtends
                textBox8.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "ClassExtends");//ClassExtends
                textBox4.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "fileName");//fileName
                comboBox1.Text = IO_Helper_DG.Ini_SelectStringValue(CommonVariables.configFilePath, "code", "fileNameSuffix", ".txt");//fileNameSuffix

                textBox6.Text = dir + "\\qixiaoCodeBuilder\\";
                colorRichTextBox1.Language = CSharp_FlowchartToCode_DG.Controls.ColorRichTextBox.Languages.SQL;
                colorRichTextBox1.Language = CSharp_FlowchartToCode_DG.Controls.ColorRichTextBox.Languages.SQL;

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.ColumnHeadersHeight = 22;

                timer1.Interval = 50;//progress refresh time
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //write ini config record the opration history
        private void setInitConfigFile()
        {
            //set config
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "config", "outputType", comboBox2.Text);//output type

            //set code builder config
            //IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "usings", textBox3.Text.Replace("\n","&")); //using
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "namespace", textBox2.Text);//namespace
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "namespaceCommonPlus", textBox10.Text);//namespace
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "TableName", textBox9.Text);//table name
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "class", textBox5.Text);//class name
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "ClassNamePlus", textBox7.Text);// ClassExtends
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "ClassExtends", textBox8.Text);// ClassExtends
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "fileName", textBox4.Text);//fileName
            IO_Helper_DG.Ini_Update(CommonVariables.configFilePath, "code", "fileNameSuffix", comboBox1.Text);//fileName
        }

        #region 获取数据库结构的代码

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.treeView1.SelectedNode = e.Node;//select current node
            if (e.Node.Level == 4)
            {
                getTableInfo();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.treeView1.SelectedNode = e.Node;//select current node
            if (e.Button == MouseButtons.Left && e.Node.Level == 4)
            {
                getTableInfo();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point pos = new Point(e.Node.Bounds.X + e.Node.Bounds.Width / 2, e.Node.Bounds.Y + e.Node.Bounds.Height / 2);
                if (e.Node.Level == 3)
                {
                    this.contextMenuStrip_Tables.Show(this.treeView1, pos);
                }
                else if (e.Node.Level == 4)
                {
                    this.contextMenuStrip_Table.Show(this.treeView1, pos);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) => getTableInfo();

        //获取数据库表信息
        public void getTableInfo()
        {
            try
            {
                string database = this.treeView1.SelectedNode.Parent.Parent.Name;
                string table = this.treeView1.SelectedNode.Name;

                this.DataBaseName = database;//当前操作的数据库名
                this.TableName = table;//当前操作的表名

                textBox5.Text = table;//将table的表名赋值给TableName变量，方便后续传值; Model
                textBox9.Text = table;//将table的表名赋值给TableName变量，方便后续传值; Model
                textBox4.Text = table + textBox7.Text.Trim();//fileName

                //if has obtain , get from static variables , it will be quickly
                if (CommonVariables.getServerInfoFinished)
                {
                    DataTable dt = CommonVariables.serverInfo.DataBaseInfos.Where(t => t.DataBaseName.Equals(this.DataBaseName)).FirstOrDefault().Tables.Where(t => t.TableName.Equals(this.TableName)).FirstOrDefault().FieldInfosTable;
                    this.DataBaseTable = dt;//将获取到的表信息保存到全局变量
                    this.dataGridView1.DataSource = dt.DefaultView;
                }
                else
                {
                    string sql = string.Empty;
                    switch (Db_Helper_DG.dataBaseType)
                    {
                        case Opt_DataBaseType.SqlServer:
                            sql = $@"use [{this.DataBaseName}] select syscolumns.name as Field ,systypes.name as FieldType , syscolumns.length as Length,syscolumns.isnullable as Nullable, sys.extended_properties.value as Description  ,IsPK = Case  when exists ( select 1 from sysobjects  inner join sysindexes  on sysindexes.name = sysobjects.name  inner join sysindexkeys  on sysindexes.id = sysindexkeys.id  and  sysindexes.indid = sysindexkeys.indid  where xtype='PK'  and parent_obj = syscolumns.id and sysindexkeys.colid = syscolumns.colid ) then 1 else 0 end ,IsIdentity = Case syscolumns.status when 128 then 1 else 0 end  from syscolumns inner join systypes on(  syscolumns.xtype = systypes.xtype and systypes.name <>'_default_' and systypes.name<>'sysname'  ) left outer join sys.extended_properties on  ( sys.extended_properties.major_id=syscolumns.id and minor_id=syscolumns.colid  ) where syscolumns.id = (select id from sysobjects where name='" + this.TableName + @"') order by syscolumns.colid ";
                            break;
                        case Opt_DataBaseType.MySql:
                            sql = $"select COLUMN_NAME as Field,DATA_TYPE as DataType,SUBSTRING_INDEX(SUBSTRING_INDEX(COLUMN_TYPE,'(',-1),')',1) as Length,iF(IS_NULLABLE='YES',1,0) as Nullable,COLUMN_COMMENT as Description,IF(COLUMN_KEY='PRI',1,0) as IsPK,(SELECT IFNULL(0,1)) as IsIdentity from information_schema.columns where TABLE_SCHEMA='{this.DataBaseName}' AND TABLE_NAME='{this.TableName}'";
                            break;
                        case Opt_DataBaseType.Oracle:
                            sql = "";
                            break;
                        default:
                            break;
                    }
                    DataTable dt = Db_Helper_DG.ExecuteDataTable(sql);
                    this.DataBaseTable = dt;//将获取到的表信息保存到全局变量
                    this.dataGridView1.DataSource = dt.DefaultView;
                }
                foreach (DataGridViewRow row in dataGridView1.Rows) row.Cells[0].Value = true;  //设置初始值为全选中
            }
            catch (Exception) { }
        }

        #endregion

        #region 操作栏按钮点击事件 全选、清空、导出到Excel...

        //Choose DataBase Connection
        private void button2_Click(object sender, EventArgs e) => new DataBaseChooseForm(this).ShowDialog();

        //工具窗口按钮
        private void button1_Click(object sender, EventArgs e) => new ToolsForm().Show();

        //select all mark
        bool isCheckAll = false;
        //select all or clear
        private void button4_Click(object sender, EventArgs e)
        {
            if (isCheckAll)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows) ((DataGridViewCheckBoxCell)row.Cells["Check"]).Value = true;
                isCheckAll = false;
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows) ((DataGridViewCheckBoxCell)row.Cells["Check"]).Value = false;
                isCheckAll = true;
            }
        }

        //Export To Excel
        private void button22_Click(object sender, EventArgs e)
        {
            string filePath = textBox6.Text.Trim();
            string fileComplexPath = $"{ filePath + DataBaseName}.xlsx";
            IO_Helper_DG.CreateDirectoryIfNotExist(filePath);
            new Thread(() =>
             {
                 Office_Helper_DG.DataTableToExcel(fileComplexPath, textBox9.Text.Trim(), this.DataBaseTable);
             }).Start();
            MessageBox.Show("OutPut->" + fileComplexPath);
        }
        //open code dir
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                using (System.Diagnostics.Process.Start("Explorer.exe", textBox6.Text)) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region page2

        //将文本框文件保存到桌面
        private void saveCodeToFile()
        {
            string dirPath = textBox6.Text;
            string fileComplexPath = dirPath + textBox4.Text + comboBox1.Text;
            IO_Helper_DG.CreateDirectoryIfNotExist(dirPath);
            using (FileStream fs = new FileStream(fileComplexPath, FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(CodeTxt);
                sw.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e) => saveCodeToFile();

        //返回代码生成设置页面 也就是首页
        private void button14_Click(object sender, EventArgs e) => this.tabControl1.SelectedTab = tabPage1;//转换到首页

        //全选的按钮 的事件
        private void button12_Click(object sender, EventArgs e) => colorRichTextBox2.SelectAll();//全选

        //复制按钮的事件
        private void button13_Click(object sender, EventArgs e) => colorRichTextBox2.Copy();//复制选择文本

        //left to right
        private void button19_Click(object sender, EventArgs e) => this.colorRichTextBox2.RightToLeft = RightToLeft.No;

        //right to left
        private void button20_Click(object sender, EventArgs e) => this.colorRichTextBox2.RightToLeft = RightToLeft.Yes;

        #endregion

        #region function button

        /// <summary>
        /// set button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e) => ChangeTexBox4();
        private void textBox5_TextChanged(object sender, EventArgs e) => ChangeTexBox4();
        private void textBox7_TextChanged(object sender, EventArgs e) => ChangeTexBox4();
        private void ChangeTexBox4() => textBox4.Text = textBox5.Text.Trim() + textBox7.Text.Trim();

        #endregion

        #region code builder settings
        //common set infoList
        private void SetInfoList()
        {
            CreateInfoDic.Clear();

            FeildName.Clear();
            FeildType.Clear();
            FeildLength.Clear();
            FeildIsNullable.Clear();
            FeildDescription.Clear();
            FeildIsPK.Clear();
            FeildIsIdentity.Clear();

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                this.dataGridView1.EndEdit();//如果DataGridView是可编辑的，将数据提交，否则处于编辑状态的行无法取到
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)row.Cells["Check"];
                if (checkBoxCell.Value.ToBoolean())//查找被选择的数据行 
                {
                    FeildName.Add(row.Cells[1].Value.ToString().Trim());
                    FeildType.Add(row.Cells[2].Value.ToString().Trim());
                    FeildLength.Add(row.Cells[3].Value.ToString().Trim());
                    FeildIsNullable.Add(row.Cells[4].Value.ToString().Trim());
                    FeildDescription.Add(row.Cells[5].Value.ToString().Trim());
                    FeildIsPK.Add(row.Cells[6].Value.ToString().Trim());
                    FeildIsIdentity.Add(row.Cells[7].Value.ToString().Trim());
                }
            }
        }

        //builder common transmit settings
        private void InitCreateInfoDic()
        {
            SetInfoList();//设置信息

            CreateInfoDic.Add("Using", textBox3.Text.Trim());
            CreateInfoDic.Add("NameSpace", textBox2.Text.Trim());
            CreateInfoDic.Add("NameSpaceCommonPlus", textBox10.Text.Trim());
            CreateInfoDic.Add("DataBaseName", DataBaseName);
            CreateInfoDic.Add("TableName", textBox9.Text.Trim());
            CreateInfoDic.Add("Class", textBox5.Text.Trim());
            CreateInfoDic.Add("ClassNamePlus", textBox7.Text.Trim());
            CreateInfoDic.Add("ClassExtends", textBox8.Text.Trim());

            CreateInfoDic.Add("FeildName", FeildName);
            CreateInfoDic.Add("FeildType", FeildType);
            CreateInfoDic.Add("FeildLength", FeildLength);
            CreateInfoDic.Add("FeildIsNullable", FeildIsNullable);
            CreateInfoDic.Add("FeildDescription", FeildDescription);
            CreateInfoDic.Add("FeildIsPK", FeildIsPK);
            CreateInfoDic.Add("FeildIsIdentity", FeildIsIdentity);
        }

        //build bode common component Func
        private void CommonComponent(Func<string> method)
        {
            try
            {
                //progress display
                ProgressDisplay(() =>
                {
                    setInitConfigFile();//record the opration history
                    InitCreateInfoDic();//init create info to dictionary

                    colorRichTextBox2.Text = null;

                    CodeTxt = method();

                    colorRichTextBox2.Text = CodeTxt;    //获取代码
                    if (comboBox2.Text.Equals("To File"))
                    {
                        saveCodeToFile();//save code to file
                    }
                    else
                    {
                        this.tabControl1.SelectedTab = tabPage2;//trasfer to code view
                    }
                });
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        #endregion

        #region Progress Display

        //create:qixiao
        //time:2017-10-2 18:37:43
        //desc:progress display . if action execute not finished , wait display

        private static bool IsProgressFinished = false;
        //Special Effects Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (toolStripProgressBar1.Value < 50)
            {
                toolStripProgressBar1.Value += 10;
            }
            else if (toolStripProgressBar1.Value >= 50 && toolStripProgressBar1.Value < 100)
            {
                if (IsProgressFinished)
                {
                    toolStripProgressBar1.Value += 10;
                }
            }
            else
            {
                this.toolStripStatusLabel1.ForeColor = Color.Green;
                this.toolStripStatusLabel1.Text = "Generate Success!!!";
                this.timer1.Stop();
            }
        }
        //Progress Display
        private void ProgressDisplay(Action action)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            this.toolStripStatusLabel1.ForeColor = Color.Red;
            this.toolStripStatusLabel1.Text = "Generate Watting...";
            this.toolStripProgressBar1.Value = 0;
            IsProgressFinished = false;
            this.timer1.Start();
            action();//execute action
            IsProgressFinished = true;
        }

        #endregion
        //Execute Sql
        private void button23_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageBox.Show("Choose DataBase First !");
            }
            else
            {
                try
                {
                    string executeSql = string.IsNullOrEmpty(colorRichTextBox1.SelectedText) ? colorRichTextBox1.Text : colorRichTextBox1.SelectedText;
                    string sql = string.Empty;
                    switch (Db_Helper_DG.dataBaseType)
                    {
                        case Opt_DataBaseType.SqlServer:
                            sql = $"USE {comboBox3.Text.Trim()};{executeSql}";
                            break;
                        case Opt_DataBaseType.MySql:
                            sql = $"USE {comboBox3.Text.Trim()};{executeSql}";
                            break;
                        case Opt_DataBaseType.Oracle:
                            break;
                        default:
                            break;
                    }
                    DataTable dataTable = Db_Helper_DG.ExecuteDataTable(sql);
                    this.dataGridView2.DataSource = dataTable?.DefaultView;
                }
                catch (Exception)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("Result"));
                    DataRow row = dt.NewRow();
                    row[0] = "SqlStatements Error ! (0 line affected)";
                    dt.Rows.Add(row);
                    this.dataGridView2.DataSource = dt;
                }
            }
        }

        // --- CodeGenerate Opration --------------------------------

        //---Volume Generate ---
        //VolumeGenerateCommonMethod
        private void VolumeGenerateCommon(Action action)
        {
            //check database information obtain operation finished ?
            if (CommonVariables.getServerInfoFinished)
            {
                //MessageBox.Show(this.treeView1.SelectedNode.Name);
                TreeNode currentNode = this.treeView1.SelectedNode;
                foreach (TreeNode nodeItem in currentNode.Nodes)
                {
                    this.treeView1.SelectedNode = nodeItem;
                    getTableInfo();
                    action();
                }
                this.treeView1.SelectedNode = currentNode;
            }
            else
            {
                MessageBox.Show("Access to the detailed database information action not complete , Please Wait ...");
            }
        }
        //MouseButton right menue operation
        private void entityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolumeGenerateCommon(() =>
            {
                comboBox1.Text = ".cs";
                CommonComponent(() => NetEntity.CreateCode(CreateInfoDic));
            });
        }
        private void entityToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VolumeGenerateCommon(() =>
            {
                comboBox1.Text = ".Java";
                CommonComponent(() => JavaEntity.CreateCode(CreateInfoDic));
            });
        }

        //--- BaseGenerate ---
        private void button7_Click_1(object sender, EventArgs e) => CommonComponent(() => SqlServerSqlStatement.CreateCode(CreateInfoDic));

        //--- C#---
        //Entities
        private void button11_Click(object sender, EventArgs e) => CommonComponent(() => NetEntity.CreateCode(CreateInfoDic));
        //Entities With Bantina
        private void button10_Click_1(object sender, EventArgs e) => CommonComponent(() => NetEntityWithBantina.CreateCode(CreateInfoDic));
        //Entity-Instance
        private void button9_Click(object sender, EventArgs e) => CommonComponent(() => NetEntityInstance.CreateCode(CreateInfoDic));
        //Entity-Inst(FO)
        private void button3_Click(object sender, EventArgs e) => CommonComponent(() => NetEntityInstance.CreateCode_otherObject(CreateInfoDic));
        //QueryObject
        private void button18_Click(object sender, EventArgs e) => CommonComponent(() => QX_FrameToQueryObject.CreateCode(CreateInfoDic));
        //Data.Contract
        private void button17_Click(object sender, EventArgs e) => CommonComponent(() => QX_FrameToDataContract.CreateCode(CreateInfoDic));
        //Data.Service
        private void button16_Click(object sender, EventArgs e) => CommonComponent(() => QX_FrameToDataService.CreateCode(CreateInfoDic));
        //Generate Three-Layout-Frame
        private void button15_Click(object sender, EventArgs e)
        {
            //Generate QX_Frame.Data.Entities
            textBox2.Text = "QX_Frame.Data.Entities";
            textBox7.Text = "";
            textBox8.Text = $"Entity<{DataBaseName}, {TableName}>";
            CommonComponent(() => NetEntityWithBantina.CreateCode(CreateInfoDic));
            //Generate QX_Frame.Data.QueryObject
            textBox3.Text = "using QX_Frame.App.Base;\nusing QX_Frame.Data.Entities;\nusing System;\nusing System.Linq.Expressions;";
            textBox2.Text = "QX_Frame.Data.QueryObject";
            textBox7.Text = "QueryObject";
            textBox8.Text = $"WcfQueryObject<{DataBaseName}, {TableName}>";
            CommonComponent(() => QX_FrameToQueryObject.CreateCode(CreateInfoDic));

            //Generate QX_Frame.Data.Service
            textBox3.Text = "using QX_Frame.App.Base;\nusing QX_Frame.Data.Contract;\nusing QX_Frame.Data.Entities;";
            textBox2.Text = "QX_Frame.Data.Service";
            textBox7.Text = "Service";
            string tableNameRelace = TableName.Replace("TB_", "").Replace("tb_", "").Replace("t_", "").Replace("T_", "");
            textBox5.Text = tableNameRelace;
            textBox8.Text = $"WcfService, I{tableNameRelace}Service";
            CommonComponent(() => QX_FrameToDataService.CreateCode(CreateInfoDic));

            string dirPath = textBox6.Text;
            string fileComplexPath = dirPath + "ClassRegister.txt";
            IO_Helper_DG.CreateDirectoryIfNotExist(dirPath);

            using (StreamWriter sw = new StreamWriter(fileComplexPath, true))
            {
                sw.WriteLine($"AppBase.Register(c => new {tableNameRelace}Service());");
                sw.Close();
            }

            //Generate QX_Frame.Data.Contract
            textBox3.Text = "using QX_Frame.Data.Entities;";
            textBox2.Text = "QX_Frame.Data.Contract";
            textBox5.Text = $"I{textBox5.Text.Replace("TB_", "").Replace("tb_", "").Replace("t_", "").Replace("T_", "")}";
            textBox7.Text = "Service";
            textBox8.Text = "";
            CommonComponent(() => QX_FrameToDataContract.CreateCode(CreateInfoDic));
        }
        //REST WebApi Controller
        private void button21_Click(object sender, EventArgs e) => CommonComponent(() => QX_FrameToRESTWebApiController.CreateCode(CreateInfoDic));

        //--- Java ---
        //Java Entities
        private void button29_Click(object sender, EventArgs e) => CommonComponent(() => JavaEntity.CreateCode(CreateInfoDic));
        //JavaEntityInstance
        private void button25_Click(object sender, EventArgs e) => CommonComponent(() => JavaEntityInstance.CreateCode(CreateInfoDic));
        //JavaEntityInstance
        private void button5_Click(object sender, EventArgs e) => CommonComponent(() => JavaEntityInstance.CreateCode_otherObject(CreateInfoDic));

        //--- Web ---
        //Jquery-Ajax
        private void button28_Click(object sender, EventArgs e) => CommonComponent(() => JavascriptAjaxData.CreateCode(CreateInfoDic));
        //Jquery-Ajax-Data
        private void button27_Click(object sender, EventArgs e) => CommonComponent(() => JavascriptAjaxData.CreateCode(CreateInfoDic));
    }
}
