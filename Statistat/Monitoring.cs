using System;
using TransDataLib;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace Statistat
{
    public partial class Monitoring : Form
    {
        public enum SwitchStatus
        {
            On,
            Off
        }

        public static Monitoring Instance { get { return GetInstance(); } }

        private static Monitoring instance;

        public SwitchStatus Status = SwitchStatus.Off;

        private Monitoring()
        {
                InitializeComponent();
        }

        private static Monitoring GetInstance()
        {
            if (instance == null)
            {
                instance = new Monitoring();
                instance.UpdateTimeBox.TextChanged += TextComparer;
                instance.minRN.TextChanged += TextComparer;
                instance.startSwitch.CheckedChanged += SwitchChanged;
                instance.finishSwitch.CheckedChanged += SwitchChanged;
                
            }

            return instance;
        }

        private static void SwitchChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Name == "startSwitch")
            {
                instance.startLot.Enabled = cb.Checked;
            }
            if (cb.Name == "finishSwitch")
            {
                instance.finishLot.Enabled = cb.Checked;
            }
        }

        private static void TextComparer(object sender, EventArgs e)
        {
            TextBox s = (TextBox)sender;
            if (s.Text != null)
            try
            {
                Convert.ToInt64(s.Text);
                instance.sBar.Items["Message"].Text = "";
            }
            catch
            {
                    instance.sBar.Items["Message"].Text = "Ошибка ввода значения"; 
                    s.Text = "";
            }
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            clock.Text = DateTime.Now.ToLongTimeString();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Switcher_Click(object sender, EventArgs e)
        {
            if (instance.Switcher.Text == "Стоп")
            {
                instance.Switcher.Text = "Пуск";
            }
            else
            {
                if (instance.Switcher.Text == "Пуск")
                {
                    instance.Switcher.Text = "Стоп";
                }
            }
            Switch();
        }

        private static void UpdateTimer()
        {
            try
            {
                instance.updateTimer.Stop();
                instance.updateTimer.Interval = Convert.ToInt32(instance.UpdateTimeBox.Text);
                instance.updateTimer.Start();
            }
            catch { }
        }

        private static void Switch()
        {
            switch (instance.Status)
            {
                case SwitchStatus.Off:
                    {
                        instance.Status = SwitchStatus.On;
                        instance.swDisplay.Image = instance.imgList.Images["On.png"];
                        instance.updateTimer.Interval = Convert.ToInt32(instance.UpdateTimeBox.Text);
                        instance.updateTimer.Start();
                        break;
                    }
                case SwitchStatus.On:
                    {
                        instance.updateTimer.Stop();
                        instance.swDisplay.Image = instance.imgList.Images["Off.png"];
                        instance.Status = SwitchStatus.Off;
                        break;
                    }
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            Switch();
            UpdateTable();
            Switch();

        }

        private void UpdateTable()
        {
            #region SQLQuery
            //string command1 = $@"Select
            //        tbpackagevalues.X,
            //        tbpackagevalues.PackNo,
            //        tbgroup.Date,
            //        tbgroup.pgH2,
            //        tbgroup.pgH3,
            //        tbpackages.PackNo
            //    FROM
            //        tbpackagevalues
            //    LEFT JOIN
            //        tbgroup
            //    ON
            //        tbpackagevalues.refNo = tbgroup.refNo
            //    LEFT JOIN
            //        tbpackages
            //    ON
            //        tbpackagevalues.PackNo = tbpackages.PackNo
            //    WHERE
            //        tbpackagevalues._Key = {"TEN_M3"} AND tbpackagevalues.X < {Convert.ToInt32(instance.UpdateTimeBox.Text)}
            //    GROUP BY tbpackages.PackNo";
            #endregion
            #region OleDB
            //OleDbConnection connection = new OleDbConnection();
            //connection.ConnectionString = @"Provider=MySQLProv;Data Source=w1c.aramid.ru;Port=3306;Database=BASE515;Uid=USER515;Pwd=user515user;";
            //connection.Open();
            //OleDbCommand command = new OleDbCommand();
            //command.Connection = connection;
            //string query = command1;
            //command.CommandText = query;

            //OleDbDataAdapter da = new OleDbDataAdapter(command);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;

            //connection.Close();
            // dataGridView1.DataSource = source;
            #endregion

            #region NHibernateQuery
            int min;
            int max;
            if (startLot != null && startSwitch.Checked) min = Convert.ToInt32(startLot.Text); else min = 0;
            if (finishLot != null && finishSwitch.Checked) max = Convert.ToInt32(finishLot.Text); else max = 999999;

            var res = Charon.GetRawSQL(Convert.ToInt32(instance.minRN.Text),min,max);
            dataGridView1.DataSource = res;
            dataGridView1.Columns[0].HeaderText = "Дата";
            dataGridView1.Columns[2].HeaderText = "Номер партии";
            dataGridView1.Columns[3].HeaderText = "Номер машины";
            dataGridView1.Columns[4].HeaderText = "Метка";
            dataGridView1.Columns[5].HeaderText = "Ср. разрывная нагрузка";
            #endregion
        }

        private void Monitoring_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }
    }
}
