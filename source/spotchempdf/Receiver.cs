
using System.IO;
using System.IO.Ports;
using log4net;

namespace spotchempdf
{
    class SerialReceiver
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SerialReceiver));
        SerialPort sp;
        IBufferProcessor processor;

        public void OpenSerial(COMport port)
        {
            sp = new SerialPort(port.name);

            sp.BaudRate = port.baudRate;
            sp.Parity = port.parity;
            sp.StopBits = port.stopBits;
            sp.DataBits = port.dataBits;
            sp.Handshake = port.handshake;
            sp.RtsEnable = port.rtsEnable;

            sp.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            try
            {
                sp.Open();
                log.Debug("Serial port "+port.name+" open");
            
            }
            catch
            {
                log.Error("Serial port "+port.name+" failed to open");
            }

        }

        public void setBufferProcessor(IBufferProcessor bp)
        {
            processor = bp;
        }

        private void DataReceivedHandler(
                           object sender,
                           SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            int read;
            int offset = 0;
            byte[] buffer = new byte[10000];
            int toRead = sp.BytesToRead;
            if (toRead > buffer.Length)
                toRead = buffer.Length;

            log.Debug("Data Received: " + toRead);

            while (toRead > 0 && (read = sp.Read(buffer, offset, toRead)) > 0)
            {
                offset += read;
                toRead -= read;
            }
            if (toRead > 0) throw new EndOfStreamException();

            if (processor != null)
                processor.processBuffer(buffer, offset);
        }

        public bool isOpen()
        {
            return sp.IsOpen;
        }

        public string getStatusString()
        {
            return sp.PortName + " = " + sp.BaudRate + "-" + sp.DataBits +sp.Parity.ToString()[0] + "-" + sp.StopBits;
        }

        public void Close()
        {
            sp.Close();
        }
    }
}
