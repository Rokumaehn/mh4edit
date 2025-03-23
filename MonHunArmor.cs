using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh4edit
{
    public class MonHunArmor : MonHunEquip
    {
        public byte _upgrade;
        public byte _resistances;
        public byte _defense;
        bool _rusted;
        bool _glow;
        int _numSlots;
        byte _rarity;
        byte _polishReq;



        public byte Upgrade { get { return _upgrade; } set { _upgrade = value; OnPropertyChanged(nameof(Upgrade)); } }
        public byte Resistances { get { return _resistances; } set { _resistances = value; OnPropertyChanged(nameof(Resistances)); } }
        public byte Defense { get { return _defense; } set { _defense = value; OnPropertyChanged(nameof(Defense)); } }
        public bool Polished { get { return !_rusted; } set { _rusted = !value; OnPropertyChanged(nameof(Polished)); } }
        public bool Glow { get { return _glow; } set { _glow = value; OnPropertyChanged(nameof(Glow)); } }
        public int NumSlots { get { return _numSlots; } set { _numSlots = value; OnPropertyChanged(nameof(NumSlots)); } }
        public byte Rarity { get { return _rarity; } set { _rarity = value; OnPropertyChanged(nameof(Rarity)); } }
        public byte PolishReq { get { return _polishReq; } set { _polishReq = value; OnPropertyChanged(nameof(PolishReq)); } }
        

        public MonHunArmor(byte[] equip) : base(equip)
        {
            _upgrade = equip[1];
            _slot1 = BitConverter.ToUInt16(equip, 6);
            _slot2 = BitConverter.ToUInt16(equip, 8);
            _slot3 = BitConverter.ToUInt16(equip, 0xA);
            _resistances = equip[0xC];
            _defense = equip[0xD];
            byte relicOpts = equip[0x10];
            _rusted = (relicOpts & 0x01) != 0;
            _glow = (relicOpts & 0x02) != 0;
            _numSlots = (relicOpts & 0x0C) >> 2;
            _rarity = equip[0x11];
            _polishReq = equip[0x12];
        }

        public override void Reset()
        {
            base.Reset();

            _upgrade = 0;
            _slot1 = 0;
            _slot2 = 0;
            _slot3 = 0;
            _resistances = 0;
            _defense = 0;
            _rusted = false;
            _glow = false;
            _numSlots = 0;
            _rarity = 0;
            _polishReq = 0;
        }

        public override byte[] Serialize()
        {
            byte[] ret = base.Serialize();

            ret[1] = _upgrade;
            ret[0xC] = _resistances;
            ret[0xD] = _defense;
            byte relicOpts = 0;
            if (_rusted) relicOpts |= 0x01;
            if (_glow) relicOpts |= 0x02;
            relicOpts |= (byte)(_numSlots << 2);
            ret[0x10] = relicOpts;
            ret[0x11] = _rarity;
            ret[0x12] = _polishReq;

            return ret;
        }

    }
}
