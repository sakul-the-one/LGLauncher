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

namespace LGLauncher
{
    public partial class CreateInstallationPath : Form
    {
        Form1 Daddy;
        public CreateInstallationPath(Form1 daddy)
        {
            InitializeComponent();
            Daddy = daddy;  
        }

        private void createInstallFileButton_Click(object sender, EventArgs e)
        {
           // FileStream fs = new FileStream(string );

            Daddy.UpdateList();
            this.Close();
        }
    }
}
