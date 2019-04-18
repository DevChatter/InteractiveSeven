using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace InteractiveSeven.Core.Memory
{
    public class MemoryAccessor : IMemoryAccessor
    {
        private const int PROCESS_WM_READ = 0x0010;
        private const int PROCESS_WM_WRITE = 0x0020;
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;

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
            Process process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null) return;

            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            ReadProcessMemory(processHandle, address, buffer, buffer.Length, out int bytesRead);

            CloseHandle(processHandle);
        }

        public void WriteMem(string processName, IntPtr address, byte[] bytes)
        {
            Process process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null) return;

            IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, process.Id);

            WriteProcessMemory(processHandle, address, bytes, bytes.Length, out int bytesWritten);

            CloseHandle(processHandle);
        }
    }
}