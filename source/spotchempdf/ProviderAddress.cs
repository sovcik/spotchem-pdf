using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spotchempdf
{
    public partial class frmProviderAddress : Form
    {
        public frmProviderAddress()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        public void setData(Provider p)
        {
            providerName.Text = p.name;
            addr1.Text = p.address1;
            addr2.Text = p.address2;
            addr3.Text = p.address3;
            addr4.Text = p.address4;
            contact1.Text = p.contact1;
            contact2.Text = p.contact2;

        }

        public void getData(out Provider p)
        {
            p = new Provider();
            p.name = providerName.Text;
            p.address1 = addr1.Text;
            p.address2 = addr2.Text;
            p.address3 = addr3.Text;
            p.address4 = addr4.Text;
            p.contact1 = contact1.Text;
            p.contact2 = contact2.Text;
        }
    }
}
