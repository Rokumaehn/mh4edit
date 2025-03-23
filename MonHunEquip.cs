using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace mh4edit
{
    public class MonHunEquip : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum Kinds
        {
            None = 0,
            Armor = 1,
            Talisman = 2,
            Weapon = 3,
        }

        protected byte _type = 0;
        protected ushort _id = 0;
        protected byte[] _raw = new byte[28];
        protected ushort _slot1;
        protected ushort _slot2;
        protected ushort _slot3;

        protected byte _typeLast = 0;
        public int Type
        {
            get { return _type; }
            set
            {
                _typeLast = _type;
                _type = (byte)value;
                OnPropertyChanged("Type");
                OnPropertyChanged("TypeString");
            }
        }

        
        
        public string TypeString
        {
            get { return types[_type]; }
        }

        public Kinds Kind
        {
            get
            {
                return _type switch
                {
                    1 => Kinds.Armor,
                    2 => Kinds.Armor,
                    3 => Kinds.Armor,
                    4 => Kinds.Armor,
                    5 => Kinds.Armor,
                    6 => Kinds.Talisman,
                    7 => Kinds.Weapon,
                    8 => Kinds.Weapon,
                    9 => Kinds.Weapon,
                    10 => Kinds.Weapon,
                    11 => Kinds.Weapon,
                    12 => Kinds.Weapon,
                    13 => Kinds.Weapon,
                    14 => Kinds.Weapon,
                    15 => Kinds.Weapon,
                    16 => Kinds.Weapon,
                    17 => Kinds.Weapon,
                    18 => Kinds.Weapon,
                    19 => Kinds.Weapon,
                    20 => Kinds.Weapon,
                    _ => Kinds.None,
                };
            }
        }
        public Kinds KindLast
        {
            get
            {
                return _typeLast switch
                {
                    1 => Kinds.Armor,
                    2 => Kinds.Armor,
                    3 => Kinds.Armor,
                    4 => Kinds.Armor,
                    5 => Kinds.Armor,
                    6 => Kinds.Talisman,
                    7 => Kinds.Weapon,
                    8 => Kinds.Weapon,
                    9 => Kinds.Weapon,
                    10 => Kinds.Weapon,
                    11 => Kinds.Weapon,
                    12 => Kinds.Weapon,
                    13 => Kinds.Weapon,
                    14 => Kinds.Weapon,
                    15 => Kinds.Weapon,
                    16 => Kinds.Weapon,
                    17 => Kinds.Weapon,
                    18 => Kinds.Weapon,
                    19 => Kinds.Weapon,
                    20 => Kinds.Weapon,
                    _ => Kinds.None,
                };
            }
        }


        public bool WasRelic {get; private set;} = false;
        public int ID
        {
            get { return _id; }
            set 
            { 
                if(this is MonHunWeapon weap)
                {
                    if(weap.IsRelic)
                    {
                        WasRelic = true;
                    }
                    else
                    {
                        WasRelic = false;
                    }
                }

                _id = (ushort)value;
                OnPropertyChanged("ID");
                OnPropertyChanged("IDString");

                if(this is MonHunWeapon weap2)
                {
                    if(WasRelic && !weap2.IsRelic)
                    {
                        WasRelic = true;
                        this.Reset();
                    }
                }
            }
        }

        public ushort Slot1 { get { return (ushort)(_slot1 & 0x7FFF); } set { _slot1 = (ushort)((value & 0x7FFF) | (_slot1 & 0x8000)); OnPropertyChanged(nameof(Slot1)); } }
        public bool Slot1Fixed { get { return (_slot1 & 0x8000) != 0; } set { _slot1 = (ushort)((_slot1 & 0x7FFF) | (value ? 0x8000 : 0x0)); OnPropertyChanged(nameof(Slot1Fixed)); } }
        public ushort Slot2 { get { return (ushort)(_slot2 & 0x7FFF); } set { _slot2 = (ushort)((value & 0x7FFF) | (_slot2 & 0x8000)); OnPropertyChanged(nameof(Slot2)); } }
        public bool Slot2Fixed { get { return (_slot2 & 0x8000) != 0; } set { _slot2 = (ushort)((_slot2 & 0x7FFF) | (value ? 0x8000 : 0x0)); OnPropertyChanged(nameof(Slot2Fixed)); } }
        public ushort Slot3 { get { return (ushort)(_slot3 & 0x7FFF); } set { _slot3 = (ushort)((value & 0x7FFF) | (_slot3 & 0x8000)); OnPropertyChanged(nameof(Slot3)); } }
        public bool Slot3Fixed { get { return (_slot3 & 0x8000) != 0; } set { _slot3 = (ushort)((_slot3 & 0x7FFF) | (value ? 0x8000 : 0x0)); OnPropertyChanged(nameof(Slot3Fixed)); } }


        public virtual void Reset()
        {
            _raw[1] = 0;
            for(int i=4; i < 28; i++)
                _raw[i] = 0;
            _slot1 = 0;
            _slot2 = 0;
            _slot3 = 0;
        }



        public string IDString
        {
            get
            {
                switch (_type)
                {
                    case 0:
                        return "(None)";
                    case 1:
                        if (MonHunEquipStatic.allEqpChest.Length > _id)
                            return MonHunEquipStatic.allEqpChest[_id];
                        return _id.ToString();
                    case 2:
                        if (MonHunEquipStatic.allEqpArms.Length > _id)
                            return MonHunEquipStatic.allEqpArms[_id];
                        return _id.ToString();
                    case 3:
                        if (MonHunEquipStatic.allEqpWaist.Length > _id)
                            return MonHunEquipStatic.allEqpWaist[_id];
                        return _id.ToString();
                    case 4:
                        if (MonHunEquipStatic.allEqpLegs.Length > _id)
                            return MonHunEquipStatic.allEqpLegs[_id];
                        return _id.ToString();
                    case 5:
                        if (MonHunEquipStatic.allEqpHeads.Length > _id)
                            return MonHunEquipStatic.allEqpHeads[_id];
                        return _id.ToString();
                    case 6:
                        if (MonHunEquipStatic.allEqpTalisman.Length > _id)
                            return MonHunEquipStatic.allEqpTalisman[_id];
                        return _id.ToString();
                    case 7:
                        if (MonHunEquipStatic.allEqpGreatsword.Length > _id)
                            return MonHunEquipStatic.allEqpGreatsword[_id];
                        return _id.ToString();
                    case 8:
                        if (MonHunEquipStatic.allEqpSns.Length > _id)
                            return MonHunEquipStatic.allEqpSns[_id];
                        return _id.ToString();
                    case 9:
                        if (MonHunEquipStatic.allEqpHammer.Length > _id)
                            return MonHunEquipStatic.allEqpHammer[_id];
                        return _id.ToString();
                    case 10:
                        if (MonHunEquipStatic.allEqpLance.Length > _id)
                            return MonHunEquipStatic.allEqpLance[_id];
                        return _id.ToString();
                    case 11:
                        if (MonHunEquipStatic.allEqpLbg.Length > _id)
                            return MonHunEquipStatic.allEqpLbg[_id];
                        return _id.ToString();
                    case 12:
                        if (MonHunEquipStatic.allEqpHbg.Length > _id)
                            return MonHunEquipStatic.allEqpHbg[_id];
                        return _id.ToString();
                    case 13:
                        if (MonHunEquipStatic.allEqpLongswods.Length > _id)
                            return MonHunEquipStatic.allEqpLongswods[_id];
                        return _id.ToString();
                    case 14:
                        if (MonHunEquipStatic.allEqpSwitchAxe.Length > _id)
                            return MonHunEquipStatic.allEqpSwitchAxe[_id];
                        return _id.ToString();
                    case 15:
                        if (MonHunEquipStatic.allEqpGunlance.Length > _id)
                            return MonHunEquipStatic.allEqpGunlance[_id];
                        return _id.ToString();
                    case 16:
                        if (MonHunEquipStatic.allEqpBow.Length > _id)
                            return MonHunEquipStatic.allEqpBow[_id];
                        return _id.ToString();
                    case 17:
                        if (MonHunEquipStatic.allEqpDualblade.Length > _id)
                            return MonHunEquipStatic.allEqpDualblade[_id];
                        return _id.ToString();
                    case 18:
                        if (MonHunEquipStatic.allEqpHuntinghorn.Length > _id)
                            return MonHunEquipStatic.allEqpHuntinghorn[_id];
                        return _id.ToString();
                    case 19:
                        if (MonHunEquipStatic.allEqpInsectglaive.Length > _id)
                            return MonHunEquipStatic.allEqpInsectglaive[_id];
                        return _id.ToString();
                    case 20:
                        if (MonHunEquipStatic.allEqpChargeblade.Length > _id)
                            return MonHunEquipStatic.allEqpChargeblade[_id];
                        return _id.ToString();
                    default:
                        break;
                }
                return "";
            }
        }


        public static List<MonHunEquip> equipTypes = new List<MonHunEquip>();



        public static List<MonHunEquip> equipChest = new List<MonHunEquip>();
        public static List<MonHunEquip> equipArms = new List<MonHunEquip>();
        public static List<MonHunEquip> equipWaist = new List<MonHunEquip>();
        public static List<MonHunEquip> equipLegs = new List<MonHunEquip>();
        public static List<MonHunEquip> equipHead = new List<MonHunEquip>();
        public static List<MonHunEquip> equipTalisman = new List<MonHunEquip>();
        public static List<MonHunEquip> equipGreatsword = new List<MonHunEquip>();
        public static List<MonHunEquip> equipSns = new List<MonHunEquip>();
        public static List<MonHunEquip> equipHammer = new List<MonHunEquip>();
        public static List<MonHunEquip> equipLance = new List<MonHunEquip>();
        public static List<MonHunEquip> equipLbg = new List<MonHunEquip>();
        public static List<MonHunEquip> equipHbg = new List<MonHunEquip>();
        public static List<MonHunEquip> equipLongsword = new List<MonHunEquip>();
        public static List<MonHunEquip> equipSwitchaxe = new List<MonHunEquip>();
        public static List<MonHunEquip> equipGunlance = new List<MonHunEquip>();
        public static List<MonHunEquip> equipBow = new List<MonHunEquip>();
        public static List<MonHunEquip> equipDualblade = new List<MonHunEquip>();
        public static List<MonHunEquip> equipHuntinghorn = new List<MonHunEquip>();
        public static List<MonHunEquip> equipInsectglaive = new List<MonHunEquip>();
        public static List<MonHunEquip> equipChargeblade = new List<MonHunEquip>();
        
        public static BitmapImage[] images = new BitmapImage[21];
        static MonHunEquip()
        {
            for(int i=0; i < MonHunEquipStatic.allEqpChest.Length; i++)
            {
                equipChest.Add(new MonHunEquip(1, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpArms.Length; i++)
            {
                equipArms.Add(new MonHunEquip(2, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpWaist.Length; i++)
            {
                equipWaist.Add(new MonHunEquip(3, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpLegs.Length; i++)
            {
                equipLegs.Add(new MonHunEquip(4, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpHeads.Length; i++)
            {
                equipHead.Add(new MonHunEquip(5, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpTalisman.Length; i++)
            {
                equipTalisman.Add(new MonHunEquip(6, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpGreatsword.Length; i++)
            {
                equipGreatsword.Add(new MonHunEquip(7, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpSns.Length; i++)
            {
                equipSns.Add(new MonHunEquip(8, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpHammer.Length; i++)
            {
                equipHammer.Add(new MonHunEquip(9, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpLance.Length; i++)
            {
                equipLance.Add(new MonHunEquip(10, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpLbg.Length; i++)
            {
                equipLbg.Add(new MonHunEquip(11, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpHbg.Length; i++)
            {
                equipHbg.Add(new MonHunEquip(12, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpLongswods.Length; i++)
            {
                equipLongsword.Add(new MonHunEquip(13, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpSwitchAxe.Length; i++)
            {
                equipSwitchaxe.Add(new MonHunEquip(14, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpGunlance.Length; i++)
            {
                equipGunlance.Add(new MonHunEquip(15, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpBow.Length; i++)
            {
                equipBow.Add(new MonHunEquip(16, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpDualblade.Length; i++)
            {
                equipDualblade.Add(new MonHunEquip(17, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpHuntinghorn.Length; i++)
            {
                equipHuntinghorn.Add(new MonHunEquip(18, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpInsectglaive.Length; i++)
            {
                equipInsectglaive.Add(new MonHunEquip(19, (ushort)i));
            }
            for(int i=0; i < MonHunEquipStatic.allEqpChargeblade.Length; i++)
            {
                equipChargeblade.Add(new MonHunEquip(20, (ushort)i));
            }

            

            images[0] = null;
            images[1] = new(new Uri("images/Equip_Chest.png", UriKind.Relative));
            images[2] = new(new Uri("images/Equip_Arms.png", UriKind.Relative));
            images[3] = new(new Uri("images/Equip_Waist.png", UriKind.Relative));
            images[4] = new(new Uri("images/Equip_Legs.png", UriKind.Relative));
            images[5] = new(new Uri("images/Equip_Head.png", UriKind.Relative));
            images[6] = new(new Uri("images/Equip_Talisman.png", UriKind.Relative));
            images[7] = new(new Uri("images/Equip_Greatsword.png", UriKind.Relative));
            images[8] = new(new Uri("images/Equip_SwordAndShield.png", UriKind.Relative));
            images[9] = new(new Uri("images/Equip_Hammer.png", UriKind.Relative));
            images[10] = new(new Uri("images/Equip_Lance.png", UriKind.Relative));
            images[11] = new(new Uri("images/Equip_LightBowgun.png", UriKind.Relative));
            images[12] = new(new Uri("images/Equip_HeavyBowgun.png", UriKind.Relative));
            images[13] = new(new Uri("images/Equip_Longsword.png", UriKind.Relative));
            images[14] = new(new Uri("images/Equip_Switchaxe.png", UriKind.Relative));
            images[15] = new(new Uri("images/Equip_Gunlance.png", UriKind.Relative));
            images[16] = new(new Uri("images/Equip_Bow.png", UriKind.Relative));
            images[17] = new(new Uri("images/Equip_Dualblades.png", UriKind.Relative));
            images[18] = new(new Uri("images/Equip_HuntingHorn.png", UriKind.Relative));
            images[19] = new(new Uri("images/Equip_InsectGlaive.png", UriKind.Relative));
            images[20] = new(new Uri("images/Equip_ChargeBlade.png", UriKind.Relative));

            equipTypes.Add(new MonHunEquip(0));
            equipTypes.Add(new MonHunEquip(1));
            equipTypes.Add(new MonHunEquip(2));
            equipTypes.Add(new MonHunEquip(3));
            equipTypes.Add(new MonHunEquip(4));
            equipTypes.Add(new MonHunEquip(5));
            equipTypes.Add(new MonHunEquip(6));
            equipTypes.Add(new MonHunEquip(7));
            equipTypes.Add(new MonHunEquip(8));
            equipTypes.Add(new MonHunEquip(9));
            equipTypes.Add(new MonHunEquip(10));
            equipTypes.Add(new MonHunEquip(11));
            equipTypes.Add(new MonHunEquip(12));
            equipTypes.Add(new MonHunEquip(13));
            equipTypes.Add(new MonHunEquip(14));
            equipTypes.Add(new MonHunEquip(15));
            equipTypes.Add(new MonHunEquip(16));
            equipTypes.Add(new MonHunEquip(17));
            equipTypes.Add(new MonHunEquip(18));
            equipTypes.Add(new MonHunEquip(19));
            equipTypes.Add(new MonHunEquip(20));
        }

        public virtual ImageSource GetImage()
        {
            // if (ID == 0)
            // {
            //     return null;
            // }
            return images[_type];
        }

        public MonHunEquip(byte type)
        {
            _type = type;
        }

        public MonHunEquip(byte type, ushort id)
        {
            _type = type;
            _id = id;
        }

        public MonHunEquip(byte[] equip)
        {
            _raw = new byte[28];
            Array.Copy(equip, 0, _raw, 0, _raw.Length);

            _type = equip[0];
            if (_type == 0)
            {
                _raw = new byte[28];
                for (int i = 0; i < _raw.Length; i++) _raw[i] = 0;
                return;
            }
            _id = BitConverter.ToUInt16(equip, 2);
            _slot1 = BitConverter.ToUInt16(equip, 6);
            _slot2 = BitConverter.ToUInt16(equip, 8);
            _slot3 = BitConverter.ToUInt16(equip, 0xA);
        }



        public static MonHunEquip Create(byte[] equip)
        {
            MonHunEquip obj = null;

            byte tp = equip[0];

            switch (tp)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    obj = new MonHunArmor(equip);
                    break;
                case 6:
                    obj = new MonHunTalisman(equip);
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                    obj = new MonHunWeapon(equip);
                    break;
                default:
                    obj = new MonHunEquip(equip);
                    break;
            }

            return obj;
        }

        public virtual byte[] Serialize()
        {
            byte[] ret = new byte[28];
            Array.Copy(_raw, 0, ret, 0, _raw.Length);

            ret[0] = _type;
            Array.Copy(BitConverter.GetBytes(_id), 0, ret, 2, 2);
            Array.Copy(BitConverter.GetBytes(_slot1), 0, ret, 6, 2);
            Array.Copy(BitConverter.GetBytes(_slot2), 0, ret, 8, 2);
            Array.Copy(BitConverter.GetBytes(_slot3), 0, ret, 0xA, 2);

            return ret;
        }

        public byte[] SerializeOnlyTypeAndId()
        {
            byte[] ret = new byte[28];

            ret[0] = _type;
            Array.Copy(BitConverter.GetBytes(_id), 0, ret, 2, 2);

            return ret;
        }


        public static string[] types = new string[]{
            "<null>",
            "Chest",
            "Arms",
            "Waist",
            "Legs",
            "Head",
            "Talisman",
            "Great Sword",
            "Sword and Shield",
            "Hammer",
            "Lance",
            "Light Bowgun",
            "Heavy Bowgun",
            "Longsword",
            "Switch Axe",
            "Gunlance",
            "Bow",
            "Dual Blades",
            "Hunting Horn",
            "Insect Glaive",
            "Charge Blade"
        };




        public static Dictionary<ushort, string> dictIdsTalisman = new Dictionary<ushort, string> {
            { 0, "0"},
            { 1, "Pawn Talisman"},
            { 2, "Bishop Talisman"},
            { 3, "Knight Talisman"},
            { 4, "Rook Talisman"},
            { 5, "Queen Talisman"},
            { 6, "King Talisman"},
            { 7, "Dragon Talisman"}
        };





        
        
    }
}
