namespace mh4edit;

public class GuildQuestStatic
{
    static GuildQuestStatic()
    {
        MonsterAIdValues = new(allMonsters.Length);
        MonsterBIdValues = new(allMonsters.Length);

        for (byte i = 0; i < allMonsters.Length; i++)
        {
            MonsterAIdValues.Add(new MonsterAIdValueType(i, allMonsters[i]));
            MonsterBIdValues.Add(new MonsterBIdValueType(i, allMonsters[i]));
        }
    }

    public class MonsterAIdValueType
    {
        public byte MonsterAId {get;set;}
        public string Display {get;set;} = string.Empty;
        public MonsterAIdValueType(byte v, string n)
        {
            MonsterAId = v;
            Display = n;
        }
    }

    public class MonsterBIdValueType
    {
        public byte MonsterBId {get;set;}
        public string Display {get;set;} = string.Empty;
        public MonsterBIdValueType(byte v, string n)
        {
            MonsterBId = v;
            Display = n;
        }
    }

    public static List<MonsterAIdValueType> MonsterAIdValues {get;set;} = new();
    public static List<MonsterBIdValueType> MonsterBIdValues {get;set;} = new();

    public class WeaponBiasValueType
    {
        public byte WeaponBias {get;set;}
        public string Display {get;set;} = string.Empty;
        public WeaponBiasValueType(byte v, string n)
        {
            WeaponBias = v;
            Display = n;
        }
    }

    public static List<WeaponBiasValueType> WeaponBiasValues {get; set;} = new()
    {
        new (0x00, "InsectGlaive/ChargeBlade"),
        new (0x01, "GreatSword"),
        new (0x02, "SnS/DB"),
        new (0x03, "Lance"),
        new (0x04, "Hammer"),
        new (0x05, "Bowgun"),
    };

    public class ArmorSeriesBiasValueType
    {
        public byte ArmorSeriesBias {get;set;}
        public string Display {get;set;} = string.Empty;
        public ArmorSeriesBiasValueType(byte v, string n)
        {
            ArmorSeriesBias = v;
            Display = n;
        }
    }

    public static List<ArmorSeriesBiasValueType> ArmorSeriesBiasValues {get; set;} = new()
    {
        new (0x00, "Original (0x00)"),
        new (0x01, "Original (0x01)"),
        new (0x02, "Original (0x02)"),
        new (0x03, "Original (0x03)"),
        new (0x04, "Original (0x04)"),
        new (0x05, "Original (0x05)"),
        new (0x06, "Tri (0x06)"),
        new (0x07, "Tri (0x07)"),
        new (0x08, "Tri (0x08)"),
        new (0x09, "Tri (0x09)"),
        new (0x0A, "Tri (0x0A)"),
        new (0x0B, "Tri (0x0B)"),
        new (0x0C, "Freedom (0x0C)"),
        new (0x0D, "Freedom (0x0D)"),
        new (0x0E, "Freedom (0x0E)"),
        new (0x0F, "Freedom (0x0F)"),
        new (0x10, "Freedom (0x10)"),
        new (0x11, "Freedom (0x11)"),
    };

    public class ArmorTypeBiasValueType
    {
        public byte ArmorTypeBias {get;set;}
        public string Display {get;set;} = string.Empty;
        public ArmorTypeBiasValueType(byte v, string n)
        {
            ArmorTypeBias = v;
            Display = n;
        }
    }

    public static List<ArmorTypeBiasValueType> ArmorTypeBiasValues {get; set;} = new()
    {
        new (0x00, "Chest"),
        new (0x01, "Arms"),
        new (0x02, "Waist"),
        new (0x03, "Legs"),
        new (0x04, "Head"),
    };

    public static string[] allMonsters = {
        "None",
        "Rathian",
        "Rathalos",
        "Pink Rathian",
        "Azure Rathalos",
        "Gold Rathian",
        "Silver Rathalos",
        "Yian Kut-Ku",
        "Blue Yian Kut-Ku",
        "Gypceros",
        "Purple Gypceros",
        "Tigrex",
        "Brute Tigrex",
        "Gendrome",
        "Iodrome",
        "Great Jaggi",
        "Velocidrome",
        "Congalala",
        "Emerald Congalala",
        "Rajang",
        "Kecha Wacha",
        "Tetsucabra",
        "Zamtrios",
        "Najarala",
        "Dalamadur",
        "Seltas",
        "Seltas Queen",
        "Nerscylla",
        "Gore Magala",
        "Shagaru Magala",
        "Yian Garuga",
        "Kushala Daora",
        "Teostra",
        "Akantor",
        "Kirin",
        "Oroshi Kirin",
        "Khezu",
        "Red Khezu",
        "Basarios",
        "Ruby Basarios",
        "Gravios",
        "Black Gravios",
        "Deviljho",
        "Savage Deviljho",
        "Brachydios",
        "Rajang",
        "Dah'ren Mohran",
        "Lagombi",
        "Zinogre",
        "Stygian Zinogre",
        "Gargwa",
        "Rhenoplos",
        "Aptonoth",
        "Popo",
        "Slagtoth",
        "Red Slagtoth",
        "Jaggi",
        "Jaggia",
        "Velociprey",
        "Genprey",
        "Ioprey",
        "Remobra",
        "Delex",
        "Conga",
        "Kelbi",
        "Felyne",
        "Melynx",
        "Altaroth",
        "Bnahabra (Blue)",
        "Bnahabra (Yellow)",
        "Bnahabra (Green)",
        "Bnahabra (Red)",
        "Zamite",
        "Konchu (Yellow)",
        "Konchu (Green)",
        "Konchu (Blue)",
        "Konchu (Red)",
        "Fatalis",
        "Crimson Fatalis",
        "White Fatalis",
        "Molten Tigrex",
        "Rock (small rock)",
        "Rusted Kushala Daora",
        "Dalamadur",
        "Rock (slightly bigger rock)",
        "Rock (large molten rock)",
        "Rock (large invisible rock)",
        "Rock (anothet invisible rock)",
        "Seregios",
        "Gogmazios",
        "Ash Kecha Wacha",
        "Berserk Tetsucabra",
        "Tigerstripe Zamtrios",
        "Tidal Najarala",
        "Desert Seltas",
        "Desert Seltas Queen",
        "Shrouded Nerscylla",
        "Gore Magala",
        "Raging Brachydios",
        "Diablos",
        "Black Diablos",
        "Monoblos",
        "White Monoblos",
        "Chameleos",
        "Big Boulder",
        "Cephadrome",
        "Cephalos",
        "Daimyo Hermitaur",
        "Plum D.Hermitaur",
        "Hermitaur",
        "Shah Dalamadur",
        "Shah Dalamadur",
        "Apex Rajang",
        "Apex Deviljho",
        "Apex Zinogre",
        "Apex Gravios",
        "Ukanlos",
        "Flame Fatalis",
        "Apceros",
        "Apex Diablos",
        "Apex Tidal Najarala",
        "Apex Tigrex",
        "Apex Seregios",
        "(Reinforcement)"
    };
}