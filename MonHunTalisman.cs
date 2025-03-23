using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh4edit
{
    public class MonHunTalisman : MonHunEquip
    {
        int _numSlots;
        ushort _slot0;
        ushort _slot1;
        ushort _slot2;
        ushort _skill1id;
        ushort _skill1amount;
        ushort _skill2id;        
        ushort _skill2amount;


        public int NumSlots { get { return _numSlots; } set { _numSlots = value; OnPropertyChanged(nameof(NumSlots)); } }
        public ushort Slot0 { get { return _slot0; } set { _slot0 = value; OnPropertyChanged(nameof(Slot0)); } }
        public ushort Slot1 { get { return _slot1; } set { _slot1 = value; OnPropertyChanged(nameof(Slot1)); } }
        public ushort Slot2 { get { return _slot2; } set { _slot2 = value; OnPropertyChanged(nameof(Slot2)); } }
        public ushort Skill1ID { get { return _skill1id; } set { _skill1id = value; OnPropertyChanged(nameof(Skill1ID)); } }
        public ushort Skill1Amount { get { return _skill1amount; } set { _skill1amount = value; OnPropertyChanged(nameof(Skill1Amount)); } }
        public ushort Skill2ID { get { return _skill2id; } set { _skill2id = value; OnPropertyChanged(nameof(Skill2ID)); } }
        public ushort Skill2Amount { get { return _skill2amount; } 
        set { _skill2amount = value; OnPropertyChanged(nameof(Skill2Amount)); } }

        public MonHunTalisman(byte[] equip) : base(equip)
        {
            _numSlots = equip[1];
            _slot0 = BitConverter.ToUInt16(equip, 6);
            _slot1 = BitConverter.ToUInt16(equip, 8);
            _slot2 = BitConverter.ToUInt16(equip, 0xA);
            _skill1id = BitConverter.ToUInt16(equip, 0xC);
            _skill1amount = BitConverter.ToUInt16(equip, 0xE);
            _skill2id = BitConverter.ToUInt16(equip, 0x10);
            _skill2amount = BitConverter.ToUInt16(equip, 0x12);
        }

        public override void Reset()
        {
            base.Reset();

            _numSlots = 0;
            _slot0 = 0;
            _slot1 = 0;
            _slot2 = 0;
            _skill1id = 0;
            _skill1amount = 0;
            _skill2id = 0;
            _skill2amount = 0;
        }

        public override byte[] Serialize()
        {
            byte[] ret = base.Serialize();

            ret[1] = (byte)_numSlots;

            Array.Copy(BitConverter.GetBytes(_slot0), 0, ret, 6, 2);
            Array.Copy(BitConverter.GetBytes(_slot1), 0, ret, 8, 2);
            Array.Copy(BitConverter.GetBytes(_slot2), 0, ret, 0xA, 2);
            
            Array.Copy(BitConverter.GetBytes(_skill1id), 0, ret, 0x0C, 2);
            Array.Copy(BitConverter.GetBytes(_skill1amount), 0, ret, 0x0E, 2);
            Array.Copy(BitConverter.GetBytes(_skill2id), 0, ret, 0x10, 2);
            Array.Copy(BitConverter.GetBytes(_skill2amount), 0, ret, 0x12, 2);

            return ret;
        }
    }
}
