using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Rubez
{
    public partial class Options : Form
    {
        DataBase dataBase = new DataBase();
        public static DataSet test = null;

        public Options()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ipTb.Text = Properties.Settings.Default.ipTbC;
            portTb.Text = Properties.Settings.Default.portTbC;
            loginTb.Text = Properties.Settings.Default.loginTbC;
            passwordTb.Text = Properties.Settings.Default.passwordTbC;
            comboDataTb.Text = Properties.Settings.Default.comboDataTbC;
            comboTableTb.Text = Properties.Settings.Default.comboTableTbC;
            comboColumnTb.Text = Properties.Settings.Default.comboColumnTbC;
            dataTypeCheckBox.Checked = Properties.Settings.Default.dataTypeSwitchC;

        }

        private void connButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ipTbC = ipTb.Text;
            Properties.Settings.Default.portTbC = portTb.Text;
            Properties.Settings.Default.loginTbC = loginTb.Text;
            Properties.Settings.Default.passwordTbC = passwordTb.Text;
            Properties.Settings.Default.Save();
            dataBase.Conn();
            if (dataBase.npgSqlConnection.State == System.Data.ConnectionState.Open)
            {
                dbCb.DataSource = dataBase.ShowDbName();
                dbCb.DisplayMember = "DATNAME";
                dbCb.ValueMember = "DATNAME";
                dataBase.Close();
            }
        }

        private void showTableListButton_Click(object sender, EventArgs e)
        {
            if (dbCb.SelectedIndex > -1)
            {
                comboDataTb.Text = dbCb.SelectedValue.ToString();
                Properties.Settings.Default.comboDataTbC = dbCb.Text;
                Properties.Settings.Default.Save();
            }

            tbCb.DataSource = dataBase.ShowTbName();
            tbCb.DisplayMember = "TABLE_NAME";
            tbCb.ValueMember = "TABLE_NAME";
            dataBase.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbCb.SelectedIndex > -1)
            {
                comboTableTb.Text = tbCb.SelectedValue.ToString();
                Properties.Settings.Default.comboTableTbC = tbCb.Text;
                Properties.Settings.Default.Save();
            }

            columnNameCb.DataSource = dataBase.ShowColumnName(comboTableTb.Text);
            columnNameCb.DisplayMember = "COLUMN_NAME";
            columnNameCb.ValueMember = "COLUMN_NAME";
            dataBase.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (dbCb.SelectedIndex > -1)
            {
                comboDataTb.Text = dbCb.SelectedValue.ToString();
                Properties.Settings.Default.comboDataTbC = dbCb.Text;
                Properties.Settings.Default.Save();
            }
            if (tbCb.SelectedIndex > -1)
            {
                comboTableTb.Text = tbCb.SelectedValue.ToString();
                Properties.Settings.Default.comboTableTbC = tbCb.Text;
                Properties.Settings.Default.Save();
            }
            if (columnNameCb.SelectedIndex > -1)
            {
                comboColumnTb.Text = columnNameCb.SelectedValue.ToString();
                Properties.Settings.Default.comboColumnTbC = columnNameCb.Text;
                Properties.Settings.Default.Save();
            }
            Properties.Settings.Default.dataTypeSwitchC = dataTypeCheckBox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
