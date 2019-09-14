using System;
using Interactive.Core.Memory;

namespace Interactive.Core.FinalFantasy8.Memory.Maps
{
    public class CharacterMap
    {
        private readonly IntPtr _baseAddress;

        public CharacterMap(IntPtr baseAddress)
        {
            _baseAddress = baseAddress;
        }

        private MemLoc _currentHp;
        public MemLoc CurrentHp => _currentHp ?? (_currentHp = new MemLoc(_baseAddress, 2));

        private MemLoc _maxHp;
        public MemLoc MaxHp => _maxHp ?? (_maxHp = new MemLoc(IntPtr.Add(_baseAddress, 2), 2));

        private MemLoc _exp;
        public MemLoc Exp => _exp ?? (_exp = new MemLoc(IntPtr.Add(_baseAddress, 4), 4));

        private MemLoc _id;
        public MemLoc Id => _id ?? (_id = new MemLoc(IntPtr.Add(_baseAddress, 8), 1));

        private MemLoc _weaponId;
        public MemLoc WeaponId => _weaponId ?? (_weaponId = new MemLoc(IntPtr.Add(_baseAddress, 9), 1));

        private MemLoc _strength;
        public MemLoc Strength => _strength ?? (_strength = new MemLoc(IntPtr.Add(_baseAddress, 10), 1));

        private MemLoc _vitality;
        public MemLoc Vitality => _vitality ?? (_vitality = new MemLoc(IntPtr.Add(_baseAddress, 11), 1));

        private MemLoc _magic;
        public MemLoc Magic => _magic ?? (_magic = new MemLoc(IntPtr.Add(_baseAddress, 12), 1));

        private MemLoc _spirit;
        public MemLoc Spirit => _spirit ?? (_spirit = new MemLoc(IntPtr.Add(_baseAddress, 13), 1));

        private MemLoc _speed;
        public MemLoc Speed => _speed ?? (_speed = new MemLoc(IntPtr.Add(_baseAddress, 14), 1));

        private MemLoc _luck;
        public MemLoc Luck => _luck ?? (_luck = new MemLoc(IntPtr.Add(_baseAddress, 15), 1));

        private MemLoc _magics;
        public MemLoc Magics => _magics ?? (_magics = new MemLoc(IntPtr.Add(_baseAddress, 16), 64));

        private MemLoc _commands;
        public MemLoc Commands => _commands ?? (_commands = new MemLoc(IntPtr.Add(_baseAddress, 80), 3));

        //quint16 current_HPs;
        //quint16 HPs;
        //quint32 exp;
        //quint8 ID;
        //quint8 weaponID;
        //quint8 STR;
        //quint8 VIT;
        //quint8 MAG;
        //quint8 SPR;
        //quint8 SPD;
        //quint8 LCK;
        //quint16 magies[32];
        //quint8 commands[3];
        quint8 u1;// unused command (padding)
        quint8 abilities[4];
        quint16 gfs;
        quint8 u2;// used unknown value
        quint8 alternative_model;// Seed costume/Galbadia costume
        quint8 j_HP;
        quint8 j_VGR;
        quint8 j_DFS;
        quint8 j_MGI;
        quint8 j_PSY;
        quint8 j_VTS;
        quint8 j_ESQ;
        quint8 j_PRC;
        quint8 j_CHC;
        quint8 j_attEle;
        quint8 j_attMtl;
        quint8 j_defEle[4];
        quint8 j_defMtl[4];
        quint8 u3;// padding ?
        quint16 compatibility[16];
        quint16 kills;
        quint16 KOs;
        quint8 exists;
        quint8 u4;
        quint8 status;
    }
}