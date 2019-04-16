using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace InteractiveSeven.UI
{
    public static class MemoryAccessor
    {
        const int PROCESS_WM_READ = 0x0010;

        const Int32 TopLeftAddr = 0x0091EFC8;
        const Int32 BotLeftAddr = 0x0091EFCC;
        const Int32 TopRightAddr = 0x0091EFD0;
        const Int32 BotRightAddr = 0x0091EFD4;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        public static (byte[] topLeft, byte[] botLeft, byte[] topRight, byte[] botRight) GetCurrentColors(string processName)
        {
            Process process = Process.GetProcessesByName(processName)[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);

            int bytesRead = 0;
            byte[] topLeftBuffer = new byte[3];
            byte[] botLeftBuffer = new byte[3];
            byte[] topRightBuffer = new byte[3];
            byte[] botRightBuffer = new byte[3];

            ReadProcessMemory((int)processHandle, TopLeftAddr, topLeftBuffer, topLeftBuffer.Length, ref bytesRead);
            ReadProcessMemory((int)processHandle, BotLeftAddr, botLeftBuffer, botLeftBuffer.Length, ref bytesRead);
            ReadProcessMemory((int)processHandle, TopRightAddr, topRightBuffer, topRightBuffer.Length, ref bytesRead);
            ReadProcessMemory((int)processHandle, BotRightAddr, botRightBuffer, botRightBuffer.Length, ref bytesRead);

            return (topLeftBuffer, botLeftBuffer, topRightBuffer, botRightBuffer);
        }
    }
}