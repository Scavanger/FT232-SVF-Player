using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nelix.Tools;

namespace Nelix.JTAG
{
    public abstract class XSVF : IDisposable
    {
        private const int LIBXSVF_MEM_NUM = 36;

        #region Enums

        public enum Mode
        {
            SVF = 1,
            XSVF = 2,
            SCAN = 3
        };

        public enum TapState : uint
        {
            /* Special States */
            TAP_INIT = 0,
            TAP_RESET = 1,
            TAP_IDLE = 2,
            /* DR States */
            TAP_DRSELECT = 3,
            TAP_DRCAPTURE = 4,
            TAP_DRSHIFT = 5,
            TAP_DREXIT1 = 6,
            TAP_DRPAUSE = 7,
            TAP_DREXIT2 = 8,
            TAP_DRUPDATE = 9,
            /* IR States */
            TAP_IRSELECT = 10,
            TAP_IRCAPTURE = 11,
            TAP_IRSHIFT = 12,
            TAP_IREXIT1 = 13,
            TAP_IRPAUSE = 14,
            TAP_IREXIT2 = 15,
            TAP_IRUPDATE = 16
        };

        public enum Memory : uint
        {
            TDI_DATA = 0,
            TDO_DATA = 1,
            TDO_MASK = 2,
            ADDR_MASK = 3,
            DATA_MASK = 4,
            SVF_COMMANDBUF = 5,
            SVF_SDR_TDI_DATA = 6,
            SVF_SDR_TDI_MASK = 7,
            SVF_SDR_TDO_DATA = 8,
            SVF_SDR_TDO_MASK = 9,
            SVF_SDR_RET_MASK = 10,
            SVF_SIR_TDI_DATA = 11,
            SVF_SIR_TDI_MASK = 12,
            SVF_SIR_TDO_DATA = 13,
            SVF_SIR_TDO_MASK = 14,
            SVF_SIR_RET_MASK = 15,
            SVF_HDR_TDI_DATA = 16,
            SVF_HDR_TDI_MASK = 17,
            SVF_HDR_TDO_DATA = 18,
            SVF_HDR_TDO_MASK = 19,
            SVF_HDR_RET_MASK = 20,
            SVF_HIR_TDI_DATA = 21,
            SVF_HIR_TDI_MASK = 22,
            SVF_HIR_TDO_DATA = 23,
            SVF_HIR_TDO_MASK = 24,
            SVF_HIR_RET_MASK = 25,
            SVF_TDR_TDI_DATA = 26,
            SVF_TDR_TDI_MASK = 27,
            SVF_TDR_TDO_DATA = 28,
            SVF_TDR_TDO_MASK = 29,
            SVF_TDR_RET_MASK = 30,
            SVF_TIR_TDI_DATA = 31,
            SVF_TIR_TDI_MASK = 32,
            SVF_TIR_TDO_DATA = 33,
            SVF_TIR_TDO_MASK = 34,
            SVF_TIR_RET_MASK = 35,
            NUM = 36
        };
        #endregion

        #region Delegates
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DSetup(IntPtr h);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DShutdown(IntPtr h);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DUdelay(IntPtr h, int usecs, int tms, int num_tck);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DGetbyte(IntPtr h);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DSync(IntPtr h);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DPulseTck(IntPtr h, int tms, int tdi, int tdo, int rmask, int sync);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DPulseSck(IntPtr h);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DSetTrst(IntPtr h, int v);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DSetFrequency(IntPtr h, int v);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DReportTapstate(IntPtr h);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DReportDevice(IntPtr h, uint idcode);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DReportStatus(IntPtr h, IntPtr message);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DReportError(IntPtr h, IntPtr file, int line, IntPtr message);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate IntPtr DRealloc(IntPtr h, IntPtr ptr, int size, Memory which);
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        private struct Host
        {
            public IntPtr Setup;
            public IntPtr Shutdown;
            public IntPtr Udelay;
            public IntPtr Getbyte;
            public IntPtr Sync;
            public IntPtr PulseTck;
            public IntPtr PulseSck;
            public IntPtr SetTrst;
            public IntPtr SetFequency;
            public IntPtr ReportTapstate;
            public IntPtr ReportDevice;
            public IntPtr ReportStatus;
            public IntPtr ReportError;
            public IntPtr Realloc;
            public TapState TapState;
            public IntPtr UserData;
        }

        //[StructLayout(LayoutKind.Sequential)]
        //private struct UserData
        //{
        //    public IntPtr File;
        //    public int Verbose;
        //    public int Clockcount;
        //    public int BitcountTdi;
        //    public int BitcountTdo;
        //    public int RetvalI;
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        //    public int[] Retval;
        //}

        #endregion

        #region DllImports
        [DllImport(@"..\..\..\DEBUG\XSVF.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int LibXSFV_Play(IntPtr h, Mode mode);

        [DllImport(@"..\..\..\DEBUG\XSVF.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr LibXSFV_State2str(TapState mode);

        [DllImport(@"..\..\..\DEBUG\XSVF.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr LibXSFV_Mem2str(Memory wich);

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr realloc(IntPtr ptr, IntPtr size);

        #endregion

        #region Events

        public event EventHandler<EventArgs<string>> Verbose;

        #endregion

        private Host _host = new Host();
        private GCHandle _HostHandle;
        private int[] _reallocMaxSize = new int[LIBXSVF_MEM_NUM];

        // Store delegates as fieled, to avoid to be colleted by GC
        private DSetup _dSetup;
        private DShutdown _dShutdown;
        private DUdelay _dUdelay;
        private DGetbyte _dGetByte;
        private DSync _dSync;
        private DPulseSck _dPulseSck;
        private DPulseTck _dPulseTck;
        private DSetTrst _dSetTrst;
        private DSetFrequency _dSetFrequency;
        private DReportError _dReportError;
        private DReportDevice _dReportDevice;
        private DReportStatus _dReportStatus;
        private DReportTapstate _dReportTapstate;
        private DRealloc _dRealloc;

        public int VerboseLevel { get; set; }
        public bool IsOpen { get; protected set; }
        public Stream Stream { get; set; }
        public List<DeviceInfo> DeviceInfos { get; protected set; }
        public int BitCountTDI { get; protected set; }
        public int BitCountTDO { get; protected set; }
        public int Clockcount { get; protected set; }

        public XSVF()
        {
            

            DeviceInfos = new List<DeviceInfo>();
            VerboseLevel = 0;
            IsOpen = false;

            _dSetup = Setup;
            _dShutdown = Shutdown;
            _dUdelay = Udelay;
            _dGetByte = GetByte;
            _dSync = Sync;
            _dPulseSck = PulseSck;
            _dPulseTck = PulseTck;
            _dSetTrst = SetTrst;
            _dSetFrequency = SetFrequency;
            _dReportDevice = ReportDevice;
            _dReportError = ReportError;
            _dReportStatus = ReportStatus;
            _dReportTapstate = ReportTapstate;
            _dRealloc = Realloc;

            _host.Setup = Marshal.GetFunctionPointerForDelegate(_dSetup);
            _host.Shutdown = Marshal.GetFunctionPointerForDelegate(_dShutdown);
            _host.Udelay = Marshal.GetFunctionPointerForDelegate(_dUdelay);
            _host.Getbyte = Marshal.GetFunctionPointerForDelegate(_dGetByte);
            _host.Sync = Marshal.GetFunctionPointerForDelegate(_dSync);
            _host.PulseSck = Marshal.GetFunctionPointerForDelegate(_dPulseSck);
            _host.PulseTck = Marshal.GetFunctionPointerForDelegate(_dPulseTck);
            _host.SetTrst = Marshal.GetFunctionPointerForDelegate(_dSetTrst);
            _host.SetFequency = Marshal.GetFunctionPointerForDelegate(_dSetFrequency);
            _host.ReportDevice = Marshal.GetFunctionPointerForDelegate(_dReportDevice);
            _host.ReportError = Marshal.GetFunctionPointerForDelegate(_dReportError);
            _host.ReportStatus = Marshal.GetFunctionPointerForDelegate(_dReportStatus);
            _host.ReportTapstate = Marshal.GetFunctionPointerForDelegate(_dReportTapstate);
            _host.Realloc = Marshal.GetFunctionPointerForDelegate(_dRealloc);

           _HostHandle = GCHandle.Alloc(_host, GCHandleType.Pinned);

            // A scan first, to get Device Infos
            //Play(Mode.SCAN);
        }


        ~XSVF()
        {
            Dispose(false);
        }


        protected virtual void OnVerbose(EventArgs<string> e)
        {
            Verbose?.Invoke(this, e);
        }

        public string Mem2String(Memory which)
        {
            IntPtr ptrStr = LibXSFV_Mem2str(which);
            return Marshal.PtrToStringAnsi(ptrStr);
        }

        #region Callbacks

        private IntPtr Realloc(IntPtr h, IntPtr ptr, int size, Memory which)
        {
            if (size > _reallocMaxSize[(int)which])
                _reallocMaxSize[(int)which] = size;

            if (VerboseLevel > 3)
                RaiseVerbose("[REALLOC:" + Mem2String(which) + ":" + size + "]");

            // What I have to to ?!?
            return realloc(ptr, (IntPtr)size);
        }

        private void ReportTapstate(IntPtr h)
        {
            if (VerboseLevel >= 3)
            {
                Host host = Marshal.PtrToStructure<Host>(h);
                RaiseVerbose("[" + host.TapState + "]");
            }
        }

        private void ReportStatus(IntPtr h, IntPtr message)
        {
            string messageStr = Marshal.PtrToStringAnsi(message);
            if (VerboseLevel >= 2)
                RaiseVerbose("[Status] " + messageStr);
        }

        private void ReportError(IntPtr h, IntPtr file, int line, IntPtr message)
        {
            string fileName = Marshal.PtrToStringAnsi(file);
            string messageStr = Marshal.PtrToStringAnsi(message);
            throw new XsfvException("xsfvlib Error: [" + fileName + ":" + line + "] " + messageStr);
        }

        private void ReportDevice(IntPtr h, uint idcode)
        {
            if (idcode != 0)
                DeviceInfos.Add(new DeviceInfo(idcode));
        }

        private int SetFrequency(IntPtr h, int v)
        {
            return SetFrequency(v);
        }

        protected virtual int SetFrequency(int v)
        {
            RaiseVerbose("WARNING: Setting JTAG clock frequency to " + v + " ignored!");
            return 0;
        }

        private void SetTrst(IntPtr h, int v)
        {
            SetTrst(v);
        }

        protected virtual void SetTrst(int v)
        {
            RaiseVerbose("WARNING: Setting TRST to " + v + " ignored!");
        }

        private int PulseTck(IntPtr h, int tms, int tdi, int tdo, int rmask, int sync)
        {
            return PulseTck(tms, tdi, tdo, rmask, sync);
        }

        protected virtual int PulseTck(int tms, int tdi, int tdo, int rmask, int sync)
        {
            Tms(tms);

            if (tdi >= 0)
            {
                BitCountTDI++;
                Tdi(tdi);
            }

            int lineTDO = PulseTck();
            int rc = lineTDO >= 0 ? lineTDO : 0;

            if (tdo > 0 && lineTDO >= 0)
            {
                BitCountTDO++;
                if (tdo != lineTDO)
                    rc = -1;
            }

            if (VerboseLevel >= 4)
                RaiseVerbose("[TMS:" + tms + " TDI:" + tdi + " TDI_ARG:" + tdo + " TDO_LINE:" + lineTDO + " RMASK:" + rmask + " RC:" + rc + "]");

            Clockcount++;
            return rc;
        }

        private void PulseSck(IntPtr h)
        {
            PulseSck();
        }

        protected virtual void PulseSck()
        {
            RaiseVerbose("WARNING: Pulsing SCK ignored!");
        }

        private int Sync(IntPtr h)
        {
            return 0;
        }

        private int GetByte(IntPtr h)
        {
            return GetByte();
        }

        protected virtual int GetByte()
        {
            if (Stream == null || !Stream.CanRead)
                throw new IOException("Stream isn't readable.");

            return Stream.ReadByte();
        }

        private void Udelay(IntPtr h, int usecs, int tms, int num_tck)
        {
            Udelay(usecs, tms, num_tck);
        }

        protected virtual void Udelay(int usecs, int tms, int num_tck)
        {
            RaiseVerbose("[Delay:" + usecs + " TMS:" + tms + " NUM_TCK:" + num_tck + "]");

            if (num_tck > 0)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                Tms(tms);
                while (num_tck > 0)
                {
                    PulseTck();
                    num_tck--;
                }
                watch.Stop();
                long ticks = usecs * 10 - watch.ElapsedTicks;

                if (VerboseLevel >= 3)
                    RaiseVerbose("DELAY_AFTER_TCK: " + (ticks > 0 ? ticks * 10 : 0));

                if (ticks > 0)
                    Thread.Sleep(new TimeSpan(ticks));

            }
        }

        private int Shutdown(IntPtr h)
        {
            return Shutdown() ? 1 : 0;
        }

        /// <summary>
        /// This method is called after every Play(), better use Dispose()
        /// </summary>
        /// <returns></returns>
        protected virtual bool Shutdown()
        {
            if (VerboseLevel >= 2)
                RaiseVerbose("[SHUTDOWN]");

            return true;
        }

        private int Setup(IntPtr h)
        {
            if (VerboseLevel >= 2)
                RaiseVerbose("[SETUP]");

            return Setup() ? 1 : 0;
        }

        /// <summary>
        /// This method is called in every Play(), better use Open(). 
        /// </summary>
        /// <returns>TRUE if setup is correct</returns>
        protected virtual bool Setup()
        {
            return true;
        }

        #endregion

        #region Public Methods

        protected abstract void Tms(int val);

        protected abstract void Tdi(int val);

        protected abstract int PulseTck();

        public virtual void Play(Mode mode)
        {
            if (!IsOpen)
                throw new InvalidOperationException("Still closed!");

            LibXSFV_Play(_HostHandle.AddrOfPinnedObject(), mode);
        }

        public virtual void Open()
        {
            if (VerboseLevel >= 2)
                RaiseVerbose("[OPEN]");

            if (!_HostHandle.IsAllocated)
                _HostHandle = GCHandle.Alloc(_host, GCHandleType.Pinned);

            IsOpen = true;
        }

        public virtual void Close()
        {
            if (VerboseLevel >= 2)
                RaiseVerbose("[CLOSE]");

            ((IDisposable)this).Dispose();
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool fDisposing)
        {
            if (fDisposing)
            {

            }

            if (_HostHandle.IsAllocated)
                _HostHandle.Free();

            IsOpen = false;
        }

        protected virtual void RaiseVerbose(string msg)
        {
            OnVerbose(new EventArgs<string>(msg + Environment.NewLine));
        }

        #endregion


    }
}
