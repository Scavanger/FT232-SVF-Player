using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nelix.JTAG
{
    public class DeviceInfo
    {
        public uint IdCode { get; private set; }
        public uint Revision { get; private set; }
        public uint Part { get; private set; }
        public uint Manufactor { get; private set; }
        public string IdCodeString { get; private set; }
        public string RevisionString { get; private set; }
        public string PartString { get; private set; }
        public string ManufactorString { get; private set; }

        public DeviceInfo(uint idcode)
        {
            IdCode = idcode;
            Revision = (idcode >> 28) & 0xf;
            Part = (idcode >> 12) & 0xffff;
            Manufactor = (idcode >> 1) & 0x7ff;
            IdCodeString = IdCode.ToString("X8");
            RevisionString = Revision.ToString("X1");
            PartString = Part.ToString("X4");
            ManufactorString = Manufactor.ToString("X3");
        }
    }
}
