using System.Collections.Specialized;
using System.ComponentModel;

namespace mh4edit;

public class Palico : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public byte[] _raw = new byte[232];

    public string _name = string.Empty;
    public ulong _exp = 0;


    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public ulong Exp
    {
        get
        {
            return _exp;
        }

        set
        {
            _exp = value;
            OnPropertyChanged(nameof(Exp));
        }
    }

    override public string ToString()
    {
        return _name==string.Empty ? "(Unnamed or Empty)" : _name;
    }

    public Palico Self => this;

    public Palico(byte[] palBytes)
    {
        _raw = new byte[232];
        Array.Copy(palBytes, 0, _raw, 0, _raw.Length);

        _name = System.Text.Encoding.Unicode.GetString(palBytes, 0, 24);
        _name = _name.Split('\0')[0];
        _exp = BitConverter.ToUInt64(palBytes, 0x70);
    }

    public virtual byte[] Serialize()
    {
        byte[] ret = new byte[232];
        Array.Copy(_raw, 0, ret, 0, _raw.Length);

        for (int i = 0; i < 24; i++) ret[i] = 0;
        System.Text.Encoding.Unicode.GetBytes(_name, 0, Math.Min(12, _name.Length), ret, 0);
        Array.Copy(BitConverter.GetBytes(_exp), 0, ret, 0x70, 8);

        return ret;
    }
}