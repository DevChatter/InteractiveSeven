using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class MemoryAccessor : IMemoryAccessor
    {
        private readonly ILogger<MemoryAccessor> _logger;
        private const int PROCESS_WM_READ = 0x0010;
        private const int PROCESS_WM_WRITE = 0x0020;
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        public MemoryAccessor(ILogger<MemoryAccessor> logger)
        {
            _logger = logger;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern Int32 CloseHandle(IntPtr hProcess);


        public void ReadMem(string processName, IntPtr address, byte[] buffer)
        {
            try
            {
                Process process = Process.GetProcessesByName(processName).FirstOrDefault();
                if (process == null) return;

                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                ReadProcessMemory(processHandle, address, buffer, buffer.Length, out int _);

                CloseHandle(processHandle);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to Read Game Memory");
            }
        }

        public ScanResult ScanMem(string processName, IntPtr startAddr,
            ushort itemSize, uint capacity, Func<byte[], bool> isMatch)
        {
            try
            {
                Process process = Process.GetProcessesByName(processName).FirstOrDefault();
                if (process == null) return new ScanResult(-1, null);

                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

                int offset = 0;
                byte[] buffer = new byte[0];
                uint capacityInBytes = capacity * itemSize;
                for (; offset < capacityInBytes; offset += itemSize)
                {
                    IntPtr address = IntPtr.Add(startAddr, offset);
                    buffer = new byte[itemSize];

                    ReadProcessMemory(processHandle, address, buffer, itemSize, out int _);

                    if (isMatch(buffer))
                    {
                        break;
                    }
                }

                CloseHandle(processHandle);

                return offset < capacityInBytes ? new ScanResult(offset, buffer) : new ScanResult(-1, null);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to Scan Memory");
            }

            return new ScanResult(-1, null);
        }

        public void WriteMem(string processName, IntPtr address, byte[] bytes)
        {
            try
            {
                Process process = Process.GetProcessesByName(processName).FirstOrDefault();
                if (process == null) return;

                IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);

                WriteProcessMemory(processHandle, address, bytes, bytes.Length, out int _);

                CloseHandle(processHandle);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to Write to Game Memory");
            }
        }
    }
}