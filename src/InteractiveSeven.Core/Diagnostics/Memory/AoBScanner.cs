using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class AoBScanner
    {
        private readonly string _processName;
        public AoBScanner(string processName)
        {
            _processName = processName;
        }

        [DllImport("kernel32.dll")]
        protected static extern bool ReadProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        protected static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION64 lpBuffer, int dwLength);

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION64
        {
            public ulong BaseAddress;
            public ulong AllocationBase;
            public int AllocationProtect;
            public int __alignment1;
            public ulong RegionSize;
            public int State;
            public int Protect;
            public int Type;
            public int __alignment2;
        }

        protected List<MEMORY_BASIC_INFORMATION64> MemoryRegion { get; set; }

        protected void MemInfo(IntPtr pHandle)
        {
            IntPtr address = new();
            for (int i = 0; i < 1000; i++)
            {
                MEMORY_BASIC_INFORMATION64 memInfo = new();
                int memDump = VirtualQueryEx(pHandle, address, out memInfo, Marshal.SizeOf(memInfo));
                if (memDump == 0)
                {
                    break;
                }

                if ((memInfo.State & 0x1000) != 0 && (memInfo.Protect & 0x100) == 0)
                {
                    MemoryRegion.Add(memInfo);
                }

                address = new IntPtr((long)memInfo.BaseAddress + (int)memInfo.RegionSize);
            }
        }
        protected IntPtr Scan(byte[] sIn, byte[] sFor)
        {
            int[] sBytes = new int[256];
            int pool = 0;
            int end = sFor.Length - 1;
            for (int i = 0; i < 256; i++)
            {
                sBytes[i] = sFor.Length;
            }

            for (int i = 0; i < end; i++)
            {
                sBytes[sFor[i]] = end - i;
            }

            while (pool <= sIn.Length - sFor.Length)
            {
                for (int i = end; sIn[pool + i] == sFor[i]; i--)
                {
                    if (i == 0)
                    {
                        return new IntPtr(pool);
                    }
                }

                pool += sBytes[sIn[pool + end]];
            }
            return IntPtr.Zero;
        }

        public ulong AoBScan(byte[] pattern)
        {
            Process game = Process.GetProcessesByName(_processName).Single();
            if (game.Id == 0)
            {
                return 0;
            }

            MemoryRegion = new List<MEMORY_BASIC_INFORMATION64>();
            MemInfo(game.Handle);
            for (int i = 0; i < MemoryRegion.Count; i++)
            {
                byte[] buff = new byte[MemoryRegion[i].RegionSize];
                ReadProcessMemory(game.Handle.ToInt32(), (long)MemoryRegion[i].BaseAddress, buff, (int)MemoryRegion[i].RegionSize, 0);

                IntPtr result = Scan(buff, pattern);
                if (result != IntPtr.Zero)
                {
                    return MemoryRegion[i].BaseAddress + (ulong)result.ToInt32();
                }
            }
            return 0;
        }
    }
}
