using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

using FTD2XX_NET;
using Nelix.JTAG;
using Nelix.Tools;

namespace Nelix.JTAG.FT232
{
    public class FT232_XSVF : XSVF
    {
        private const string DEVICE_NAME = "FT232R";
        private const uint BAUDRATE = 9600;
        private const int LATENCY = 2;

        private FTDI _ftdi;
        private bool _hasSetupCHanged;
        private string _currentDeviceName;
        private byte _ftReg;
        private CancellationTokenSource _cancelTokenSource;
        private TaskScheduler _taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private bool _isAsync;

        private FT232_Pin _tms;
        private FT232_Pin _tdi;
        private FT232_Pin _tck;
        private FT232_Pin _tdo;
        private byte _divisor;

        public event EventHandler<EventArgs<long>> ByteRead;
        public event EventHandler<EventArgs<string>> Error;
        public event EventHandler<EventArgs<bool>> TaskCompled;


        #region Properties

        public byte Divisor
        {
            get { return _divisor; }
            set
            {
                if (value != _divisor)
                {
                    _divisor = value;
                    UpdateDivisor();
                    _hasSetupCHanged = true;
                }
            }
        }

        public FT232_Pin TMS
        {
            get { return _tms; }
            set
            {
                if (value != _tms)
                {
                    _tms = value;
                    _hasSetupCHanged = true;
                }
            }
        }

        public FT232_Pin TDI
        {
            get { return _tdi; }
            set
            {
                if (value != _tdi)
                {
                    _tdi = value;
                    _hasSetupCHanged = true;
                }
            }
        }

        public FT232_Pin TCK
        {
            get { return _tck; }
            set
            {
                if (value != _tck)
                {
                    _tck = value;
                    _hasSetupCHanged = true;
                }

            }
        }

        public FT232_Pin TDO
        {
            get { return _tdo; }
            set
            {
                if (value != _tdo)
                {
                    _tdo = value;
                    _hasSetupCHanged = true;
                }
            }
        }

        #endregion

        public FT232_XSVF()
        {
            _ftdi = new FTDI();
            _ftReg = 0;
            _currentDeviceName = DEVICE_NAME;
            _cancelTokenSource = new CancellationTokenSource();
            _isAsync = false;

            TMS = FT232_Pin.DSR;
            TDI = FT232_Pin.TXD;
            TDO = FT232_Pin.RXD;
            TCK = FT232_Pin.DCD;

            _hasSetupCHanged = false;
        }

        ~FT232_XSVF()
        {
            Dispose(false);
        }

        public override void Open()
        {
            if (IsOpen)
            {
                RaiseVerbose("FT232 device already open");
                return;
            }

            SetupFTDI();
            base.Open();
        }

        public void Open(string deviceName)
        {
            if (string.IsNullOrWhiteSpace(deviceName))
                throw new ArgumentNullException("deviceName");

            if (IsOpen)
            {
                RaiseVerbose("FT232 device already open");
                return;
            }

            _currentDeviceName = deviceName;

            Open();
        }

        public override void Close()
        {
            if (_ftdi.IsOpen)
                _ftdi.Close();

            IsOpen = false;
            base.Close();
            RaiseVerbose("Close device " + _currentDeviceName);
        }

        public override void Play(Mode mode)
        {
            if (_hasSetupCHanged)
            {
                RaiseVerbose("Setup has changed. Reopen FTDI device.");
                Close();
                Open();
            }

            base.Play(mode);
        }

        public async void PlayAsync(Mode mode)
        {
            _isAsync = true;
            _cancelTokenSource = new CancellationTokenSource();
            try
            {
                await Task.Run(() => Play(mode), _cancelTokenSource.Token);
                OnTaskCompleded(new EventArgs<bool>(true));
            }
            catch (TaskCanceledException)
            {
                RaiseVerbose("[Canceld]");
                OnTaskCompleded(new EventArgs<bool>(true));
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                OnTaskCompleded(new EventArgs<bool>(false));
            }
            finally
            {
                _isAsync = false;
            }

        }

        public void PlayAsyncCancel()
        {
            _cancelTokenSource.Cancel();
        }



        protected override void RaiseVerbose(string msg)
        {
            var eventArg = new EventArgs<string>(msg + Environment.NewLine);
            RaiseEvent(() => OnVerbose(eventArg));
        }

        private void RaiseError(string msg)
        {
            var eventArg = new EventArgs<string>(msg + Environment.NewLine);
            RaiseEvent(() => OnError(eventArg));
        }

        private void RaiseEvent(Action action)
        {
            if (_isAsync)
            {
                Task.Factory.StartNew(
                    action,
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    _taskScheduler)
                    .Wait();
            }
            else
                action.Invoke();
        }

        protected override int GetByte()
        {
            if (_cancelTokenSource.Token.IsCancellationRequested)
                throw new TaskCanceledException();

            RaiseEvent(() => OnByteRead(new EventArgs<long>(Stream.Position)));

            return base.GetByte();
        }

        protected override void Tdi(int val)
        {
            if (val > 0)
                _ftReg |= MaskTDI();
            else
                _ftReg &= (byte)~MaskTDI();
        }

        protected override void Tms(int val)
        {
            if (val > 0)
                _ftReg |= MaskTMS();
            else
                _ftReg &= (byte)~MaskTMS();
        }


        protected override int PulseTck()
        {
            if (!IsOpen)
                throw new InvalidOperationException("Device is closed.");

            if (_cancelTokenSource.Token.IsCancellationRequested)
                throw new TaskCanceledException();

            uint numBytes = 0;
            uint bytesWritten = 0;
            uint bytesRead = 0;

            byte[] bufferIn = new byte[3];
            byte[] bufferOut = new byte[3];

            bufferOut[0] = (byte)(_ftReg & (byte)~MaskTCK());
            bufferOut[1] = (byte)(_ftReg | MaskTCK());
            bufferOut[2] = bufferOut[0];

            if (_ftdi.GetRxBytesAvailable(ref numBytes) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Can't get quenue status.");

            if (numBytes != 0)
                throw new IOException("FT232 device queue is not zero");

            if (_ftdi.Write(bufferOut, 3, ref bytesWritten) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Write error.");

            if (_ftdi.Read(bufferIn, 3, ref bytesRead) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Read error.");

            if (bytesRead != 3 || bytesWritten != 3)
                throw new IOException("FT232 write read mismatch. (" + bytesRead + ", " + bytesWritten + ")");

            return (bufferIn[2] & MaskTDO()) > 0 ? 1 : 0;
        }

        #region Private members

        private void UpdateDivisor()
        {
            if (_ftdi.SetDivisor(_divisor) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Can't set divisor.");
        }

        private void SetupFTDI()
        {

            if (_ftdi.OpenByDescription(_currentDeviceName) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Can't open device: " + _currentDeviceName);

            if (_ftdi.SetBaudRate(BAUDRATE) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Can't set baudrate to:" + BAUDRATE);

            if (_ftdi.SetLatency(LATENCY) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Can't set latency size to " + LATENCY);

            // Reset 
            if (_ftdi.SetBitMode(0, 0) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Can't reset controller.");

            if (_ftdi.SetBitMode(MaskIO(), FTDI.FT_BIT_MODES.FT_BIT_MODE_SYNC_BITBANG) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Can't set bitbang mode.");

            RaiseVerbose("Open device " + _currentDeviceName);
        }

        private byte MaskTMS()
        {
            return ((byte)(1 << (byte)_tms));
        }

        private byte MaskTDI()
        {
            return ((byte)(1 << (byte)_tdi));
        }

        private byte MaskTCK()
        {
            return ((byte)(1 << (byte)_tck));
        }

        private byte MaskTDO()
        {
            return ((byte)(1 << (byte)_tdo));
        }

        private byte MaskIO()
        {
            return (byte)(MaskTMS() | MaskTDI() | MaskTCK());
        }

        protected virtual void OnByteRead(EventArgs<long> e)
        {
            ByteRead?.Invoke(this, e);
        }

        protected virtual void OnError(EventArgs<string> e)
        {
            Error?.Invoke(this, e);
        }

        protected virtual void OnTaskCompleded(EventArgs<bool> e)
        {
            TaskCompled?.Invoke(this, e);
        }

        #endregion

        //public void SetupFTDI()
        //{
        //    if (!_ftdi.IsOpen)
        //        throw new InvalidOperationException("FTDI Device is closed.");

        //    uint numBytes = 0;
        //    uint bytesWritten = 0;
        //    byte[] buffer = new byte[256];

        //    if (_ftdi.GetRxBytesAvailable(ref numBytes) != FTDI.FT_STATUS.FT_OK)
        //        throw new IOException("Can't get quenue status.");

        //    if (_ftdi.InTransferSize(128) != FTDI.FT_STATUS.FT_OK)
        //        throw new IOException("Can't set transfer size to 128.");

        //    if (_ftdi.SetLatency(2) != FTDI.FT_STATUS.FT_OK)
        //        throw new IOException("Can't set latency size to 2.");

        //   if ( _ftdi.SetBitMode(0, 0) != FTDI.FT_STATUS.FT_OK)
        //        throw new IOException("Can't reset controller.");

        //    byte bTemp = 0;

        //    bTemp |=  (byte)Math.Pow(2, TMS);
        //    bTemp |= (byte)Math.Pow(2, TDI);
        //    bTemp |= (byte)Math.Pow(2, TCK);

        //    if (_ftdi.SetBitMode(bTemp, FTDI.FT_BIT_MODES.FT_BIT_MODE_SYNC_BITBANG) != FTDI.FT_STATUS.FT_OK)
        //        throw new IOException("Can't set bitbang mode.");


        //    if (Divisor > 255)
        //        Divisor = 255;

        //    if (Divisor < 0)
        //        Divisor = 0;

        //   if (_ftdi.SetDivisor((ushort)Divisor) != FTDI.FT_STATUS.FT_OK)
        //        throw new IOException("Can't set divisor.");

        //    bTemp &=  (byte)~(byte)(Math.Pow(2, TMS));
        //    bTemp &= (byte)~(byte)(Math.Pow(2, TDI));
        //    bTemp &= (byte)~(byte)(Math.Pow(2, TCK));

        //   if (_ftdi.Write(buffer, 1, ref bytesWritten) != FTDI.FT_STATUS.FT_OK)
        //        throw new IOException("Can't write default pin states.");

        //    isSetup = true;
        //}

        #region Static members
        public static string[] ScanFTDIInterfaces(string customString = null)
        {
            FTDI ftdi = new FTDI();
            uint deviceCount = 0;
            FTDI.FT_DEVICE_INFO_NODE[] deviceInfo;

            if (ftdi.GetNumberOfDevices(ref deviceCount) != FTDI.FT_STATUS.FT_OK)
                throw new IOException("Can't get number of devices.");

            if (deviceCount > 0)
            {
                deviceInfo = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
                if (ftdi.GetDeviceList(deviceInfo) != FTDI.FT_STATUS.FT_OK)
                    throw new IOException("Can't get device description.");

                string deviceName;

                if (!String.IsNullOrWhiteSpace(customString))
                    deviceName = customString;
                else
                    deviceName = DEVICE_NAME;

                return deviceInfo
                    .Where(d => d.Description.Contains(deviceName))
                    .Select(d => d.Description)
                    .ToArray();
            }
            else
                return new string[0];
        }

        #endregion
    }
}
