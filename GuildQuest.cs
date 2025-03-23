using System.Collections.Specialized;
using System.ComponentModel;

namespace mh4edit;

public class GuildQuest : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public byte[] _raw = new byte[304];

    public string _owner = string.Empty;
    public ulong _id = 0;
    public byte _initialLevel = 0;
    public byte _currentLevel = 0;

    public byte _weaponBias = 0;
    public byte _armorSeriesBias = 0;
    public byte _armorTypeBias = 0;
    public byte _monsterAId = 0;
    public byte _monsterBId = 0;


    public string Owner
    {
        get
        {
            return _owner;
        }

        set
        {
            _owner = value;
            OnPropertyChanged(nameof(Owner));
        }
    }

    public ulong Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public byte InitialLevel
    {
        get
        {
            return _initialLevel;
        }

        set
        {
            _initialLevel = value;
            OnPropertyChanged(nameof(InitialLevel));
        }
    }

    public byte CurrentLevel
    {
        get
        {
            return _currentLevel;
        }

        set
        {
            _currentLevel = value;
            OnPropertyChanged(nameof(CurrentLevel));
        }
    }

    public byte WeaponBias
    {
        get
        {
            return _weaponBias;
        }

        set
        {
            _weaponBias = value;
            OnPropertyChanged(nameof(WeaponBias));
        }
    }

    public byte ArmorSeriesBias
    {
        get
        {
            return _armorSeriesBias;
        }

        set
        {
            _armorSeriesBias = value;
            OnPropertyChanged(nameof(ArmorSeriesBias));
        }
    }

    public byte ArmorTypeBias
    {
        get
        {
            return _armorTypeBias;
        }

        set
        {
            _armorTypeBias = value;
            OnPropertyChanged(nameof(ArmorTypeBias));
        }
    }

    public byte MonsterAId
    {
        get
        {
            return _monsterAId;
        }

        set
        {
            _monsterAId = value;
            OnPropertyChanged(nameof(MonsterAId));
        }
    }

    public byte MonsterBId
    {
        get
        {
            return _monsterBId;
        }

        set
        {
            _monsterBId = value;
            OnPropertyChanged(nameof(MonsterBId));
        }
    }

    public GuildQuest Self => this;


    public GuildQuest(byte[] equip)
    {
        _raw = new byte[304];
        Array.Copy(equip, 0, _raw, 0, _raw.Length);

        _owner = System.Text.Encoding.Unicode.GetString(equip, 0, 24);
        _id = BitConverter.ToUInt64(equip, 0x18);
        _weaponBias = equip[0x25];
        _armorSeriesBias = equip[0x26];
        _armorTypeBias = equip[0x27];
        _monsterAId = equip[0x28];
        _monsterBId = equip[0x50];
        _initialLevel = equip[0x128];
        _currentLevel = equip[0x129];
    }

    public virtual byte[] Serialize()
    {
        byte[] ret = new byte[304];
        Array.Copy(_raw, 0, ret, 0, _raw.Length);

        for (int i = 0; i < 24; i++) ret[i] = 0;
        System.Text.Encoding.Unicode.GetBytes(_owner, 0, 12, ret, 0);
        Array.Copy(BitConverter.GetBytes(_id), 0, ret, 0x18, 8);
        ret[0x25] = _weaponBias;
        ret[0x26] = _armorSeriesBias;
        ret[0x27] = _armorTypeBias;
        ret[0x28] = _monsterAId;
        ret[0x50] = _monsterBId;
        ret[0x128] = _initialLevel;
        ret[0x129] = _currentLevel;

        return ret;
    }
}