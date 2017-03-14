using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Nelix.JTAG;
using Nelix.JTAG.FT232;
using Nelix.Tools;

namespace FT232_SVF_Player
{
    public partial class FormMain : Form
    {
        private FT232_XSVF _svfPlayer;
        private FileStream _fs;
        string[] _interfaces;

        private int _progress = 0;


        public FormMain()
        {
            InitializeComponent();

            button_Stop.Enabled = false;
            button_Start.Enabled = false;

            _svfPlayer = new FT232_XSVF();
            _svfPlayer.Error += _svfPlayer_Error;
            _svfPlayer.Verbose += _svfPlayer_Verbose;
            _svfPlayer.ByteRead += _svfPlayer_ByteRead;
            _svfPlayer.TaskCompled += _svfPlayer_TaskCompled;

            string[] pins = Enum.GetValues(typeof(FT232_Pin)).Cast<FT232_Pin>().Select(o => o.ToString()).ToArray();

            comboBox_TCK.Items.AddRange(pins);
            comboBox_TCK.SelectedIndex = 6;
            comboBox_TDI.Items.AddRange(pins);
            comboBox_TDI.SelectedIndex = 0;
            comboBox_TDO.Items.AddRange(pins);
            comboBox_TDO.SelectedIndex = 1;
            comboBox_TMS.Items.AddRange(pins);
            comboBox_TMS.SelectedIndex = 5;

            ScanDevices();
        }

        private void _svfPlayer_Error(object sender, EventArgs<string> e)
        {
            textBox_log.AppendText("ERROR: " + e.Value);
        }

        private void _svfPlayer_TaskCompled(object sender, EventArgs<bool> e)
        {
            button_Start.Enabled = true;
            button_Stop.Enabled = false;
            comboBox_TCK.Enabled = true;
            comboBox_TDI.Enabled = true;
            comboBox_TMS.Enabled = true;
            comboBox_TDO.Enabled = true;
            textBox_fileName.Enabled = true;
            button_loadFile.Enabled = true;
            progressBar.Value = 0;
            _progress = 0;
            _fs.Position = 0;

            if (e.Value)
                textBox_log.AppendText("Finished without errors." + Environment.NewLine);
            else
                textBox_log.AppendText("Finished with errors. See above for details." + Environment.NewLine);
        }

        private void GetDeviceInfos()
        {
            _svfPlayer.Play(XSVF.Mode.SCAN);
            comboBox_devices.Items.AddRange(_svfPlayer.DeviceInfos.Select(d => d.IdCodeString).ToArray());
            if (comboBox_devices.Items.Count >= 1)
                comboBox_devices.SelectedIndex = 0;
        }

        private void _svfPlayer_ByteRead(object sender, EventArgs<long> e)
        {
            int progress = (int)(e.Value / (_fs.Length / 1000));

            if (progress > _progress)
            {
                _progress = progress;
                progressBar.Value = _progress;
            }
        }

        private void _svfPlayer_Verbose(object sender, EventArgs<string> e)
        {

            textBox_log.AppendText(e.Value);
        }

        private void textBox_log_VisibleChanged(object sender, EventArgs e)
        {
            if (textBox_log.Visible)
            {
                textBox_log.SelectionStart = textBox_log.TextLength;
                textBox_log.ScrollToCaret();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.clifford.at/libxsvf/");
        }

        private void comboBox_Interfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_svfPlayer.IsOpen)
                _svfPlayer.Close();

            _svfPlayer.Open(_interfaces[comboBox_Interfaces.SelectedIndex]);
        }

        private void button_rescan_Click(object sender, EventArgs e)
        {
            ScanDevices();
        }

        private void ScanDevices()
        {
            _interfaces = FT232_XSVF.ScanFTDIInterfaces();
            if (_interfaces.Count() >= 1)
            {
                textBox_fileName.Enabled = true;
                button_loadFile.Enabled = true;
                comboBox_devices.Enabled = true;
                comboBox_TCK.Enabled = true;
                comboBox_TDI.Enabled = true;
                comboBox_TMS.Enabled = true;
                comboBox_TDO.Enabled = true;
                comboBox_Interfaces.Enabled = true;
                comboBox_Interfaces.Items.Clear();
                comboBox_Interfaces.Items.AddRange(_interfaces);
                if (comboBox_Interfaces.Items.Count >= 1)
                    comboBox_Interfaces.SelectedIndex = 0;

                GetDeviceInfos();
            }
            else
            {
                comboBox_TCK.Enabled = false;
                comboBox_TDI.Enabled = false;
                comboBox_TMS.Enabled = false;
                comboBox_TDO.Enabled = false;
                textBox_fileName.Enabled = false;
                button_loadFile.Enabled = false;
                comboBox_Interfaces.Items.Clear();
                comboBox_devices.Items.Clear();
                label_Revision.Text = string.Empty;
                label_Manufactor.Text = string.Empty;
                label_Part.Text = string.Empty;
                comboBox_devices.Enabled = false;
                comboBox_Interfaces.Enabled = false;

                textBox_log.AppendText("No Interfaces found." + Environment.NewLine);

            }  
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_svfPlayer.IsOpen)
                _svfPlayer.Close();

            _fs?.Close();
        }

        private void comboBox_TMS_SelectedIndexChanged(object sender, EventArgs e)
        {
            _svfPlayer.TMS = (FT232_Pin)Enum.Parse(typeof(FT232_Pin), comboBox_TMS.SelectedItem.ToString());
        }

        private void comboBox_TDO_SelectedIndexChanged(object sender, EventArgs e)
        {
            _svfPlayer.TDO = (FT232_Pin)Enum.Parse(typeof(FT232_Pin), comboBox_TDO.SelectedItem.ToString());
        }

        private void comboBox_TDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            _svfPlayer.TDI = (FT232_Pin)Enum.Parse(typeof(FT232_Pin), comboBox_TDI.SelectedItem.ToString());
        }

        private void comboBox_TCK_SelectedIndexChanged(object sender, EventArgs e)
        {
            _svfPlayer.TCK = (FT232_Pin)Enum.Parse(typeof(FT232_Pin), comboBox_TCK.SelectedItem.ToString());
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            comboBox_verboseLevel.SelectedIndex = 2;
        }

        private void comboBox_devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_Manufactor.Text = _svfPlayer.DeviceInfos[comboBox_devices.SelectedIndex].ManufactorString;
            label_Part.Text = _svfPlayer.DeviceInfos[comboBox_devices.SelectedIndex].PartString;
            label_Revision.Text = _svfPlayer.DeviceInfos[comboBox_devices.SelectedIndex].RevisionString;
        }

        private void comboBox_verboseLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _svfPlayer.VerboseLevel = comboBox_verboseLevel.SelectedIndex + 1;
        }

        private void button_loadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = false;
            ofd.Filter = "svf files (*.svf)|*.svf|xsvf files (*.xsvf)|*.xsvf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox_fileName.Text = ofd.FileName;
                    _fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                    _svfPlayer.Stream = _fs;
                    textBox_log.AppendText("Open file " + ofd.SafeFileName + Environment.NewLine);
                    button_Start.Enabled = true;

                }
                catch (Exception)
                {
                    textBox_log.AppendText("Can't open file" + Environment.NewLine);
                }
            }
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            button_Start.Enabled = false;
            button_Stop.Enabled = true;
            comboBox_TCK.Enabled = false;
            comboBox_TDI.Enabled = false;
            comboBox_TMS.Enabled = false;
            comboBox_TDO.Enabled = false;
            textBox_fileName.Enabled = false;
            button_loadFile.Enabled = false;
            _svfPlayer.PlayAsync(XSVF.Mode.SVF);
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            _svfPlayer.PlayAsyncCancel();
        }
    }
}
