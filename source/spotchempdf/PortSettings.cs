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
    public partial class frmSerialSettings : Form
    {
        public bool OKclicked = false;

        public frmSerialSettings(COMport cfg)
        {
            InitializeComponent();

            portName.DataSource = System.IO.Ports.SerialPort.GetPortNames();
            portName.SelectedIndex = portName.FindStringExact(cfg.name);
            this.baudRate.Text = cfg.baudRate.ToString();
            this.dataBits.Text = cfg.dataBits.ToString();
            this.parity.Text = cfg.parity.ToString();
            this.stopBits.Text = cfg.stopBits.ToString();
            this.flow.Text = cfg.handshake.ToString();
            this.rtsEnable.Checked = cfg.rtsEnable;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OKclicked = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string getPortName()
        {
            return portName.Text;
        }

        public int getBaudRate()
        {
            int br = 0;
            if (!int.TryParse(baudRate.Text, out br))
                br = 0;
            return br;
        }

        public void setParams(string portname, int baudrate)
        {
            portName.Text = portname;
            baudRate.Text = baudrate.ToString();
        }
    }
}
