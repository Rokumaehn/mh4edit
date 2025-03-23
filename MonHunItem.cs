using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace mh4edit
{
    public class MonHunItem : INotifyPropertyChanged
    {
        ushort _id;
        ushort _count;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ushort ID 
        { 
            get { return _id; } 
            set { 
                _id = (ushort)(value & 0x7FF);
                OnPropertyChanged(nameof(ID)); 
            } 
        }
        public ushort Count 
        { 
            get { return _count; } 
            set { _count = value > 99 ? (ushort)99 : value; OnPropertyChanged(nameof(Count)); } 
        }
        public string Name
        {
            get
            {
                if (_id < 1949)
                    return names[_id];
                else
                    return "<UNDEF>";
            }
        }

        public MonHunItem(ushort id, ushort count)
        {
            _id = id;
            _count = count;
        }

        static MonHunItem()
        {
            List<string> items = new();
            using(StreamReader sr = new StreamReader("lists\\itemlist.txt"))
            {
                while(!sr.EndOfStream)
                {
                    string[] parts = sr.ReadLine().Split('\t');
                    if(parts.Length == 2)
                    {
                        items.Add(parts[1].Substring(1, parts[1].Length - 2));
                    }
                }
            }
            names = items.ToArray();

            for(int i=0; i < MonHunItem.names.Length; i++)
            {
               MonHunCharacter.allItems.Add(new MonHunItem((ushort)i, 0));
            }
        }

        public static string[] Names { get { return names; } }
        public static string[] names = new string[] { };
    }
}
