using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh4edit
{
    public class MonHunWeapon : MonHunEquip
    {
        byte _kinsectLevel;
        byte _elementValue;
        byte _elementType;
        byte _sharpness;
        byte _modifier;
        byte _upgrade;
        byte _special;

        bool _rusted;
        bool _glow;
        int _numSlots;
        byte _honing;
        byte _rarity;
        byte _polishReq;

        public enum SubKinds
        {
            None = -1,
            Low = 0,
            Medium = 1,
            High = 2,
        }

        public SubKinds SubKind
        {
            get
            {
                return _type switch
                {
                    7 => SubKinds.High,
                    8 => SubKinds.Low,
                    9 => SubKinds.Medium,
                    10 => SubKinds.Medium,
                    11 => SubKinds.None,
                    12 => SubKinds.None,
                    13 => SubKinds.Medium,
                    14 => SubKinds.Medium,
                    15 => SubKinds.Medium,
                    16 => SubKinds.Low,
                    17 => SubKinds.Low,
                    18 => SubKinds.High,
                    19 => SubKinds.Low,
                    20 => SubKinds.Medium,
                    _ => SubKinds.None,
                };
            }
        }

        public bool IsBowgun
        {
            get
            {
                return _type switch
                {
                    11 => true,
                    12 => true,
                    _ => false,
                };
            }
        }

        public bool IsBow => _type == 16;
        public bool IsLbg => _type == 11;
        public bool IsHbg => _type == 12;
        public bool IsGlaive => _type == 19;

        public bool IsRanged
        {
            get
            {
                return _type switch
                {
                    11 => true,
                    12 => true,
                    16 => true,
                    _ => false,
                };
            }
        }

        public List<MonHunEquipStatic.ModifierValueType> ModifierValueList
        {
            get
            {
                if(IsRanged)
                {
                    return MonHunEquipStatic.ModifierValuesRanged;
                }
                else
                {
                    return MonHunEquipStatic.ModifierValuesMelee;
                }
            }
        }

        public List<MonHunEquipStatic.SharpnessValueType> SharpnessValueList
        {
            get
            {
                switch(_type)
                {
                    case 11:
                        return MonHunEquipStatic.SharpnessValuesLbg;
                    case 12:
                        return MonHunEquipStatic.SharpnessValuesHbg;
                    case 16:
                        return MonHunEquipStatic.SharpnessValuesBow;
                    default:
                        return MonHunEquipStatic.SharpnessValuesMelee;
                }
            }
        }

        public List<MonHunEquipStatic.UpgradeValueType> UpgradeValueList
        {
            get
            {
                return MonHunEquipStatic.UpgradeValues;
            }
        }

        public List<MonHunEquipStatic.SpecialValueType> SpecialValueList
        {
            get
            {
                switch(_type)
                {
                    case 7:
                        return MonHunEquipStatic.SpecialValuesGreatswordHammer;
                    case 8:
                        return MonHunEquipStatic.SpecialValuesSnsLance;
                    case 9:
                        return MonHunEquipStatic.SpecialValuesGreatswordHammer;
                    case 10:
                        return MonHunEquipStatic.SpecialValuesSnsLance;
                    case 11:
                        return MonHunEquipStatic.SpecialValuesLbg;
                    case 12:
                        return MonHunEquipStatic.SpecialValuesHbg;
                    case 13:
                        return MonHunEquipStatic.SpecialValuesLongswordDualblades;
                    case 14:
                        return MonHunEquipStatic.SpecialValuesSwitchaxe;
                    case 15:
                        return MonHunEquipStatic.SpecialValuesGunlance;
                    case 16:
                        return MonHunEquipStatic.SpecialValuesBow;
                    case 17:
                        return MonHunEquipStatic.SpecialValuesLongswordDualblades;
                    case 18:
                        return MonHunEquipStatic.SpecialValuesHuntinghorn;
                    case 19:
                        return MonHunEquipStatic.SpecialValuesInsectglaive;
                    case 20:
                        return MonHunEquipStatic.SpecialValuesChargeblade;
                    default:
                        return [];
                }
            }
        }

        public string SpecialValueName
        {
            get
            {
                return _type switch
                {
                    7 => "ATK Boost",
                    8 => "DEF Boost",
                    9 => "ATK Boost",
                    10 => "DEF Boost",
                    11 => "Shots available",
                    12 => "Shots available",
                    13 => "Affinity Boost",
                    14 => "Phials",
                    15 => "Shots",
                    16 => "(None)",
                    17 => "Affinity Boost",
                    18 => "Notes",
                    19 => "Kinsect Type",
                    20 => "Phials",
                    _ => "Unknown",
                };  
            }
        }


        public int ElementKind
        {
            get
            {
                return _elementType switch
                {
                    6 => 1,
                    7 => 1,
                    8 => 1,
                    9 => 1,
                    _ => 0,
                };
            }
        }

        public List<MonHunEquipStatic.ElementValuesValueType> ElementValueList
        {
            get
            {
                if(SubKind==SubKinds.None)
                {
                    return [];
                }
                int idx = (int)SubKind + 3 * ElementKind;
                return MonHunEquipStatic.ElementValues[idx];
            }
        }

        public byte KinsectLevel { get { return _kinsectLevel; } set { _kinsectLevel = value; OnPropertyChanged(nameof(KinsectLevel)); } }
        public byte ElementValue { get { return _elementValue; } set { _elementValue = value; OnPropertyChanged(nameof(ElementValue)); } }
        public string ElementValueText
        {
            get
            {
                return _elementValue.ToString();
            }
            set 
            {
                long test;
                if(long.TryParse(value, out test))
                {
                    if(test > 255)
                    {
                        _elementValue = 255;
                    }
                    else if(test < 0)
                    {
                        _elementValue = 0;
                    }
                    else
                    {
                        _elementValue = (byte)test;
                    }
                }
            }
        }
        public byte ElementType 
        { 
            get { return _elementType; } 
            set 
            { 
                _elementType = value;
                OnPropertyChanged(nameof(ElementType));
                OnPropertyChanged(nameof(ElementValueList));

                if(_elementType == 0)
                {
                    _elementValue = 0;
                    OnPropertyChanged(nameof(ElementValue));
                }

                OnPropertyChanged(nameof(HasElement));
            } 
        }

        public bool HasElement => _elementType != 0;


        public bool IsRelic
        {
            get
            {
                return IDString.Contains("(red)")
                    || IDString.Contains("(yellow)")
                    || IDString.Contains("(green)")
                    || IDString.Contains("(blue)")
                    || IDString.Contains("(purple)");
            }
        }

        public byte Sharpness { get { return _sharpness; } set { _sharpness = value; OnPropertyChanged(nameof(Sharpness)); } }
        public byte Modifier { get { return _modifier; } set { _modifier = value; OnPropertyChanged(nameof(Modifier)); } }
        public byte Upgrade { get { return _upgrade; } set { _upgrade = value; OnPropertyChanged(nameof(Upgrade)); } }
        public byte Special { get { return _special; } set { _special = value; OnPropertyChanged(nameof(Special)); } }
        public bool Polished { get { return !_rusted; } set { _rusted = !value; OnPropertyChanged(nameof(Polished)); } }
        public bool Glow { get { return _glow; } set { _glow = value; OnPropertyChanged(nameof(Glow)); } }
        public int NumSlots { get { return _numSlots; } set { _numSlots = value; OnPropertyChanged(nameof(NumSlots)); } }
        public byte Honing { get { return _honing; } set { _honing = value; OnPropertyChanged(nameof(Honing)); } }
        public byte Rarity { get { return _rarity; } set { _rarity = value; OnPropertyChanged(nameof(Rarity)); } }
        public byte PolishReq { get { return _polishReq; } set { _polishReq = value; OnPropertyChanged(nameof(PolishReq)); } }

        public MonHunWeapon(byte[] equip) : base(equip)
        {
            _kinsectLevel = equip[1];
            _elementValue = equip[4];
            _elementType = equip[5];
            _sharpness = equip[0xC];
            _modifier = equip[0xD];
            _upgrade = equip[0xE];
            _special = equip[0xF];
            byte relicOpts = equip[0x10];
            _rusted = (relicOpts & 0x01) != 0;
            _glow = (relicOpts & 0x02) != 0;
            _numSlots = (relicOpts & 0x0C) >> 2;
            _rarity = equip[0x11];
            _polishReq = equip[0x12];
            _honing = equip[0x13];
        }

        public override void Reset()
        {
            base.Reset();

            _kinsectLevel = 0;
            _elementValue = 0;
            _elementType = 0;
            _sharpness = 0;
            _modifier = 0;
            _upgrade = 0;
            _special = 0;
            _rusted = false;
            _glow = false;
            _numSlots = 0;
            _rarity = 0;
            _polishReq = 0;
            _honing = 0;
        }

        public override byte[] Serialize()
        {
            byte[] ret = base.Serialize();

            ret[1] = _kinsectLevel;
            ret[4] = _elementValue;
            ret[5] = _elementType;
            ret[0xC] = _sharpness;
            ret[0xD] = _modifier;
            ret[0xE] = _upgrade;
            ret[0xF] = _special;
            byte relicOpts = 0;
            if (_rusted) relicOpts |= 0x01;
            if (_glow) relicOpts |= 0x02;
            relicOpts |= (byte)(_numSlots << 2);
            ret[0x10] = relicOpts;
            ret[0x11] = _rarity;
            ret[0x12] = _polishReq;
            ret[0x13] = _honing;

            return ret;
        }

        public static Dictionary<byte, string> dictSkills = new Dictionary<byte, string> {
            { 0, "<null>"},
            { 1, "Poison"},
            { 2, "Paralysis"},
            { 3, "Sleep"},
            { 4, "Stun"},
            { 5, "Hearing"},
            { 6, "Wind Res"},
            { 7, "Tremor Res"},
            { 8, "Tumbling Res"},
            { 9, "Heat Res"},
            { 10, "Cold Res"},
            { 11, "Cold Acc"},
            { 12, "Heat Acc"},
            { 13, "Anti-Theft"},
            { 14, "Def lock"},
            { 15, "Frenzy Res"},
            { 16, "Biology"},
            { 17, "Bleeding"},
            { 18, "Attack"},
            { 19, "Defense"},
            { 20, "Health"},
            { 21, "Fire Res"},
            { 22, "Water Res"},
            { 23, "Thunder Res"},
            { 24, "Ice Res"},
            { 25, "Dragon Res"},
            { 26, "Blight Res"},
            { 27, "Fire Atk"},
            { 28, "Water Atk"},
            { 29, "ThunderAtk"},
            { 30, "Ice Atk"},
            { 31, "Dragon Atk"},
            { 32, "Elemental"},
            { 33, "Special Attack"},
            { 34, "Sharpener"},
            { 35, "Handicraft"},
            { 36, "Sharpness"},
            { 37, "Fencing"},
            { 38, "Polishing"},
            { 39, "Blunt"},
            { 40, "Crit Draw"},
            { 41, "PunishDraw"},
            { 42, "Sheathing"},
            { 43, "Reload Spd"},
            { 44, "Recoil"},
            { 45, "Precision"},
            { 46, "Normal Up"},
            { 47, "Pierce Up"},
            { 48, "Pellet Up"},
            { 49, "Strike Up"},
            { 50, "Normal S+"},
            { 51, "Pierce S+"},
            { 52, "Pellet S+"},
            { 53, "Crag S+"},
            { 54, "Clust S+"},
            { 55, "Poison C+"},
            { 56, "Para C+"},
            { 57, "Sleep C+"},
            { 58, "Power C+"},
            { 59, "Element C+"},
            { 60, "C.Range C+"},
            { 61, "Exhaust C+"},
            { 62, "Blast C+"},
            { 63, "Rapid Fire"},
            { 64, "Dead Eye"},
            { 65, "Ammo"},
            { 66, "Irregular S."},
            { 67, "Ammo Depot"},
            { 68, "Expert"},
            { 69, "Tenderizer"},
            { 70, "Unceasing"},
            { 71, "CritStatus"},
            { 72, "CritElement"},
            { 73, "Crit. Boost"},
            { 74, "FastCharge"},
            { 75, "Stamina"},
            { 76, "Constitutn"},
            { 77, "Stam Recov"},
            { 78, "Evasion"},
            { 79, "Evade Dist"},
            { 80, "Bubbles"},
            { 81, "Guard"},
            { 82, "Guard Boost"},
            { 83, "KO"},
            { 84, "Stam Drain"},
            { 85, "Maestro"},
            { 86, "Artillery"},
            { 87, "Destroyer"},
            { 88, "Bomb Boost"},
            { 89, "Gloves Off"},
            { 90, "Spirit"},
            { 91, "Unscathed"},
            { 92, "Chance"},
            { 93, "Potential"},
            { 94, "Survivor"},
            { 95, "Rage"},
            { 96, "Predicament"},
            { 97, "Guts"},
            { 98, "Sense"},
            { 99, "TeamPlayer"},
            {100, "TeamLeader"},
            {101, "Mounting"},
            {102, "Leaping"},
            {103, "Clear Mind"},
            {104, "Psychic"},
            {105, "Perception"},
            {106, "Scout"},
            {107, "Transporting"},
            {108, "Protection"},
            {109, "Buckler"},
            {110, "Rec Level"},
            {111, "Rec Speed"},
            {112, "LastingPwr"},
            {113, "Wide-Range"},
            {114, "Hunger"},
            {115, "Gluttony"},
            {116, "Eating"},
            {117, "LightEater"},
            {118, "Carnivore"},
            {119, "Mycology"},
            {120, "Herbalism"},
            {121, "Combo Rate"},
            {122, "Combo Plus"},
            {123, "SpeedSetup"},
            {124, "Gathering"},
            {125, "Honey"},
            {126, "Charmer"},
            {127, "Whim"},
            {128, "Fate"},
            {129, "Carving"},
            {130, "Capts"},
            {131, "Beruna"},
            {132, "Kokoto"},
            {133, "Pokke"},
            {134, "Yukumo"},
            {135, "Crimson Helmet"},
            {136, "Heavy Snow"},
            {137, "Arms Breaker"},
            {138, "Boulder Piercer"},
            {139, "Purple Poison"},
            {140, "Treasure Carrier"},
            {141, "White Gale"},
            {142, "One Eyed"},
            {143, "Black Blaze"},
            {144, "Gold Thunder"},
            {145, "Raging Talon"},
            {146, "Ember Blade"},
            {147, "D. Fencing"},
            {148, "Torso Up"}
        };
    }
}
