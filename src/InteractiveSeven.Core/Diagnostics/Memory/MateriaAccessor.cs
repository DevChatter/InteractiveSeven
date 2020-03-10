﻿using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics.Memory.Model;
using InteractiveSeven.Core.Settings;
using System;
using System.Linq;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class MateriaAccessor : IMateriaAccessor
    {
        private readonly IMemoryAccessor _memory;
        private const ushort InvCapacity = 200;
        private const int ItemSize = 4;
        private static readonly IntPtr FirstAddress = new IntPtr(0xDC04B4);
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public MateriaAccessor(IMemoryAccessor memory)
        {
            _memory = memory;
        }

        public void AddMateria(byte materiaId, uint experience = 0)
        {
            var scanResult = _memory.ScanMem(Settings.ProcessName,
                FirstAddress, ItemSize, InvCapacity, IsEmpty);

            if (scanResult.BaseAddrOffset == -1) return;

            IntPtr address = IntPtr.Add(FirstAddress, scanResult.BaseAddrOffset);
            var materiaSlot = new MateriaSlot(materiaId, experience);
            _memory.WriteMem(Settings.ProcessName, address, materiaSlot.AsBytes());

            bool IsEmpty(byte[] bytes) => bytes.All(b => b == byte.MaxValue);
        }

        public void RemoveWeaponMateria(CharNames charNames, int keep = 0)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charNames);
            byte[] emptyRow = { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue };

            for (int i = ItemSize * keep; i < charMemLoc.WeaponMateria.NumBytes; i += ItemSize)
            {
                IntPtr address = IntPtr.Add(charMemLoc.WeaponMateria.Address, i);
                var bytes = new byte[ItemSize];
                _memory.ReadMem(Settings.ProcessName, address, bytes);
                var materia = new MateriaSlot(bytes);
                AddMateria(materia.MateriaId, materia.Experience);
                _memory.WriteMem(Settings.ProcessName, address, emptyRow);
            }
        }

        public void RemoveArmletMateria(CharNames charNames, int keep = 0)
        {
            CharMemLoc charMemLoc = CharMemLoc.ByName(charNames);
            byte[] emptyRow = { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue };

            for (int i = ItemSize * keep; i < charMemLoc.ArmorMateria.NumBytes; i += ItemSize)
            {
                IntPtr address = IntPtr.Add(charMemLoc.ArmorMateria.Address, i);
                var bytes = new byte[ItemSize];
                _memory.ReadMem(Settings.ProcessName, address, bytes);
                var materia = new MateriaSlot(bytes);
                AddMateria(materia.MateriaId, materia.Experience);
                _memory.WriteMem(Settings.ProcessName, address, emptyRow);
            }
        }

        public bool HasMateria(byte materiaId) => (FindOffset(materiaId) > -1);

        private int FindOffset(byte materiaId)
        {
            var scanResult = _memory.ScanMem(Settings.ProcessName,
                FirstAddress, ItemSize, InvCapacity, IsItem);

            return scanResult.BaseAddrOffset;

            bool IsItem(byte[] bytes) => new MateriaSlot(bytes).MateriaId == materiaId;
        }

        public bool DropMateria(byte materiaId)
        {
            var addressOffset = FindOffset(materiaId);
            if (addressOffset < 0)
            {
                return false;
            }

            IntPtr address = IntPtr.Add(FirstAddress, addressOffset);
            _memory.WriteMem(Settings.ProcessName, address,
                new[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue });

            return true;
        }

        public void RemoveAllMateria()
        {
            int inventoryTotalBytes = (InvCapacity * ItemSize);
            byte[] bytes = new byte[inventoryTotalBytes];
            for (int i = 0; i < inventoryTotalBytes; i++)
            {
                bytes[i] = byte.MaxValue;
            }
            _memory.WriteMem(Settings.ProcessName, FirstAddress, bytes);
        }
    }
}