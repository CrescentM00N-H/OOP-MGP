using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euchre.Views
{
    public partial class frmEuchre : Form
    {
        public frmEuchre()
        {
            InitializeComponent();
        }

        private void frmEuchre_Load(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings settingsForm = new frmSettings();
            settingsForm.Show();        // Show the new form
        }
    }
}
