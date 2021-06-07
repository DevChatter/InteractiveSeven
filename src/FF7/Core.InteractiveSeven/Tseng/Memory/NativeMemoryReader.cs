using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using InteractiveSeven.Core.Diagnostics;
using InteractiveSeven.Core.Diagnostics.Memory;
using Microsoft.Extensions.Logging;

namespace DevChatter.InteractiveGames.Core.Seven.Tseng.Memory
{
    public class NativeMemoryReader : IDisposable
    {
        private readonly ProcessConnector _processConnector;
        private readonly ILogger<NativeMemoryReader> _logger;
        private const int ProcessAllAccess = 0x1F0FFF;
        private bool _disposedValue;

        public NativeMemoryReader(ProcessConnector processConnector, ILogger<NativeMemoryReader> logger)
        {
            _processConnector = processConnector;
            _logger = logger;
        }

        ~NativeMemoryReader()
        {
            Dispose(false);
        }

        /// <summary>
        /// The process that memory will be read from when ReadMemory is called
        /// </summary>
        public Process TargetProcess { get; private set; } = null;

        /// <summary>
        /// The handle to the process that was retrieved during the constructor or the last
        /// successful call to the Open method
        /// </summary>
        public IntPtr TargetProcessHandle { get; private set; } = IntPtr.Zero;

        /// <summary>
        /// Closes a handle that was previously obtained by the constructor or a call to the Open method
        /// </summary>
        public void Close()
        {
            if (TargetProcessHandle != IntPtr.Zero)
            {
                var result = CloseHandle(TargetProcessHandle);
                if (!result)
                    throw new ApplicationException("Unable to close process handle. The last error reported was: " + new Win32Exception().Message);
                TargetProcessHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Releases resources and closes any process handles that are still open
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Reads the specified number of bytes from an address in the process's memory.
        /// All memory in the specified range must be available or the method will fail.
        /// Returns Nothing if the method fails for any reason
        /// </summary>
        /// <param name="memoryLocation">The address in the process's virtual memory to start reading from and the number of bytes to read</param>
        public byte[] ReadMemory(MemLoc memoryLocation)
        {
            if (ConfirmProcessConnection())
            {
                var bytes = new byte[memoryLocation.NumBytes + 1];
                var result = ReadProcessMemory(TargetProcessHandle, memoryLocation.Address, bytes, Convert.ToUInt32(memoryLocation.NumBytes), 0);
                return result ? bytes : null;
            }

            return null; // TODO: what to do when not connecting?
        }

        private bool ConfirmProcessConnection()
        {
            try
            {
                if (TargetProcess == null || TargetProcess.HasExited || TargetProcessHandle == IntPtr.Zero)
                {
                    TargetProcess = _processConnector.FF7Process;
                    TargetProcessHandle = OpenProcess(ProcessAllAccess, true, Convert.ToUInt32(TargetProcess.Id));
                    if (TargetProcessHandle == IntPtr.Zero)
                        throw new ApplicationException("Unable to open process. The last error reported was: " + new Win32Exception().Message);
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error connecting to FF7 Process.");
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue && TargetProcessHandle != IntPtr.Zero)
            {
                try
                {
                    CloseHandle(TargetProcessHandle);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error closing process handle.");
                }
            }
            _disposedValue = true;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", EntryPoint = "OpenProcess", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ReadProcessMemory([In] IntPtr hProcess, [In] IntPtr lpBaseAddress, [Out] byte[] lpBuffer, uint nSize, [Out] uint lpNumberOfBytesRead);
    }
}
