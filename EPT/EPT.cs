using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPT.classes;

namespace EPT
{
    public partial class EPT : Form
    {
        public EPT()
        {
            InitializeComponent();
        }
        seaClass v = new seaClass();

        private void calculateBT_Click(object sender, EventArgs e)
        {
            //Pick up values from textboxes
            v.zdk = double.Parse(zdkTB.Text);
            //v.test = double.Parse(testTB.Text);

            //results
            bool success = v.seaCalculation(v);
            if (success == true)
            {
                testTB.Text = Convert.ToString(v.zdk);
            }
            else
            {
                MessageBox.Show("cos poszlo zle");
            }
        }
    }
}
