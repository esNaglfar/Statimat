using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcellSaver;
using TransDataLib;

namespace Statistat
{
    public partial class MainForm : Form
    {
        int[] MachineNumbers;

        public MainForm()
        {
            InitializeComponent();
        }

        public static Monitoring monitor;

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateTextBoxes())
            {
                IsBuisy frm = new IsBuisy();
                frm.Visible = true;
                
                DataBase db;
                db = Charon.GetQueryByLOTN(MachineNumbers[0], MachineNumbers[1]);
                MessageBox.Show($"Найдено записей: {db.Groups.Count} ");
                frm.PBMaxValue = db.Groups.Count;
                try { ESaver.RecordSet += frm.CheckProgress; } catch { }
                ESaver.Save(db);
                
                frm.Close();
            }
        }



        private int[] CombineTextBoxes()
        {
            MachineNumbers = null;

            try
            {
                MachineNumbers = new int[2];
                MachineNumbers[0] = Convert.ToInt32(textBox1.Text);
                MachineNumbers[1] = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                MachineNumbers = null;
                MessageBox.Show("Убедитесь в правильности ввода номеров партий", "Ошибка ввода номера партии", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return MachineNumbers;
        }

        private bool ValidateTextBoxes()
        {
            if (CombineTextBoxes() != null) return true; else return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Monitoring.Instance.Show();
        }
    }
}
