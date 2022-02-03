using System;
using System.Windows.Forms;
using System.Threading;

namespace Statistat
{
    public partial class IsBuisy : Form
    {
        
        public IsBuisy()
        {
            InitializeComponent();
        }

        public bool IsDone { get; set; } = false;

        public int PBMaxValue { get; set; }

        public int CurentValue { get; set; }




        private void IsBuisy_Load(object sender, EventArgs e)
        {

        }

        public void CheckProgress()
        {
            pb1.Value++;
        }





    }
}
