using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace mh4edit
{
   public class MonHunCharacter : INotifyPropertyChanged
   {
      // Basic
      public string name;
      public byte gender;
      public byte face;
      public byte hairStyle;
      public byte clothingType;
      public byte voice;
      public byte eyeColor;
      public byte featuresType;
      public byte[] featuresColor = new byte[3];
      public byte[] hairColor = new byte[3];
      public byte[] clothingColor = new byte[3];
      public byte[] skinColor = new byte[3];
      public byte unknown1;
      public uint hunterRank;
      public uint hunterRankPoints;
      public uint funds;
      public uint playtimeMH4G;
      public uint playtimeMH4;

      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      public MonHunItem[] itemBox;
      public MonHunEquip[] equipBox;
      public GuildQuest[] guildQuests;
      public Palico[] palicos;

      public MonHunItem[] ItemBox
      {
         get
         {
            return itemBox;
         }

         set
         {
            itemBox = value;
            OnPropertyChanged(nameof(ItemBox));
         }
      }

      public MonHunEquip[] EquipBox
      {
         get
         {
            return equipBox;
         }

         set
         {
            equipBox = value;
            OnPropertyChanged(nameof(EquipBox));
         }
      }

      public GuildQuest[] GuildQuests
      {
         get
         {
            return guildQuests;
         }

         set
         {
            guildQuests = value;
            OnPropertyChanged(nameof(GuildQuests));
         }
      }

      public Palico[] Palicos
      {
         get
         {
            return palicos;
         }

         set
         {
            palicos = value;
            OnPropertyChanged(nameof(GuildQuests));
         }
      }

      public uint Funds
      {
         get
         {
            return funds;
         }

         set
         {
            funds = value;
         }
      }

      public uint HunterRank
      {
         get
         {
            return hunterRank;
         }

         set
         {
            hunterRank = value;
         }
      }

      public uint HunterRankPoints
      {
         get
         {
            return hunterRankPoints;
         }

         set
         {
            hunterRankPoints = value;
         }
      }

      public string Name
      {
         get
         {
            return name;
         }

         set
         {
            name = value;
         }
      }

      ushort idxEqpWeapon = 0;
      ushort idxEqpChest = 0;
      ushort idxEqpArms = 0;
      ushort idxEqpWaist = 0;
      ushort idxEqpLegs = 0;
      ushort idxEqpHead = 0;
      ushort idxEqpTalisman = 0;

      public string[] ItemNames => MonHunItem.names;

      public MonHunEquip ReloadEquip(int idx)
      {
         if (idx < 0 || idx > equipBox.Length - 1) return equipBox[idx];
         equipBox[idx] = MonHunEquip.Create(equipBox[idx].Serialize());
         return equipBox[idx];
      }

      public byte[] SerializeBase()
      {
         byte[] ret = new byte[0x40];
         BinaryWriter writer = new BinaryWriter(new MemoryStream(ret));

         byte[] bname = new byte[24];
         for (int i = 0; i < 24; i++) bname[i] = 0;
         System.Text.Encoding.Unicode.GetBytes(name, 0, Math.Min(12, name.Length), bname, 0);
         writer.Write(bname);
         writer.Write(gender);
         writer.Write(face);
         writer.Write(hairStyle);
         writer.Write(clothingType);
         writer.Write(voice);
         writer.Write(eyeColor);
         writer.Write(featuresType);
         writer.Write(featuresColor);
         writer.Write(hairColor);
         writer.Write(clothingColor);
         writer.Write(skinColor);
         writer.Write(unknown1);
         writer.Write(hunterRank);
         writer.Write(hunterRankPoints);
         writer.Write(funds);
         writer.Write(playtimeMH4G);
         writer.Write(playtimeMH4);

         return ret;
      }

      public byte[] SerializeCharacterEquip()
      {
         byte[] ret = new byte[196];
         byte[] empty = new byte[28];
         BinaryWriter writer = new BinaryWriter(new MemoryStream(ret));

         empty[0] = 7;
         writer.Write(idxEqpWeapon == 0xFFFF ? empty : equipBox[idxEqpWeapon].Serialize());
         empty[0] = 1;
         writer.Write(idxEqpChest == 0xFFFF ? empty : equipBox[idxEqpChest].Serialize());
         empty[0] = 2;
         writer.Write(idxEqpArms == 0xFFFF ? empty : equipBox[idxEqpArms].Serialize());
         empty[0] = 3;
         writer.Write(idxEqpWaist == 0xFFFF ? empty : equipBox[idxEqpWaist].Serialize());
         empty[0] = 4;
         writer.Write(idxEqpLegs == 0xFFFF ? empty : equipBox[idxEqpLegs].Serialize());
         empty[0] = 5;
         writer.Write(idxEqpHead == 0xFFFF ? empty : equipBox[idxEqpHead].Serialize());
         empty[0] = 6;
         writer.Write(idxEqpTalisman == 0xFFFF ? empty : equipBox[idxEqpTalisman].Serialize());

         return ret;
      }

      public byte[] SerializeItemBox()
      {
         byte[] ret = new byte[5600];
         BinaryWriter writer = new BinaryWriter(new MemoryStream(ret));

         for (int i = 0; i < 1400; i++)
         {
            writer.Write(itemBox[i].ID);
            writer.Write(itemBox[i].Count);
         }

         return ret;
      }

      public byte[] SerializeEquipmentBox()
      {
         byte[] ret = new byte[28 * 1400];
         BinaryWriter writer = new BinaryWriter(new MemoryStream(ret));

         for (int i = 0; i < 1400; i++)
         {
            Array.Copy(equipBox[i].Serialize(), 0, ret, i * 28, 28);
         }

         return ret;
      }

      public byte[] SerializeActiveGq()
      {
         byte[] ret = new byte[304 * 10];
         BinaryWriter writer = new BinaryWriter(new MemoryStream(ret));

         for (int i = 0; i < 10; i++)
         {
            Array.Copy(guildQuests[i].Serialize(), 0, ret, i * 304, 304);
         }

         return ret;
      }

      public byte[] SerializePalicoMain()
      {
         return palicos[0].Serialize();
      }

      public byte[] SerializePalicoStringers()
      {
         byte[] ret = new byte[232 * 5];
         for(int i=0; i<5; i++)
         {
            Array.Copy(palicos[i+1].Serialize(), 0, ret, i*232, 232);
         }
         return ret;
      }

      public byte[] SerializePalicoReserves()
      {
         byte[] ret = new byte[232 * 50];
         for(int i=0; i<50; i++)
         {
            Array.Copy(palicos[i+6].Serialize(), 0, ret, i*232, 232);
         }
         return ret;
      }

      public static List<MonHunItem> allItems = new();
      public List<MonHunItem> AllItems => allItems;
      public List<MonHunEquip> AllEquipTypes => MonHunEquip.equipTypes;
      public List<MonHunEquip> AllEquipGreatswords => MonHunEquip.equipGreatsword;


      public MonHunCharacter(MemoryStream ms)
      {
         BinaryReader reader = new BinaryReader(ms);
         itemBox = new MonHunItem[1400];
         equipBox = new MonHunEquip[1400];
         guildQuests = new GuildQuest[10];

         reader.BaseStream.Seek(0x0, SeekOrigin.Begin);
         name = System.Text.Encoding.Unicode.GetString(reader.ReadBytes(24), 0, 24);
         name = name.Split('\0')[0];
         gender = reader.ReadByte();
         face = reader.ReadByte();
         hairStyle = reader.ReadByte();
         clothingType = reader.ReadByte();
         voice = reader.ReadByte();
         eyeColor = reader.ReadByte();
         featuresType = reader.ReadByte();
         reader.Read(featuresColor, 0, 3);
         reader.Read(hairColor, 0, 3);
         reader.Read(clothingColor, 0, 3);
         reader.Read(skinColor, 0, 3);
         unknown1 = reader.ReadByte();
         hunterRank = reader.ReadUInt32();
         hunterRankPoints = reader.ReadUInt32();
         funds = reader.ReadUInt32();
         playtimeMH4G = reader.ReadUInt32();
         playtimeMH4 = reader.ReadUInt32();
         //equippedWeapon = MonHunEquip.Create(reader.ReadBytes(28));
         ms.Seek(0x104, SeekOrigin.Begin);
         idxEqpWeapon = reader.ReadUInt16();
         idxEqpChest = reader.ReadUInt16();
         idxEqpArms = reader.ReadUInt16();
         idxEqpWaist = reader.ReadUInt16();
         idxEqpLegs = reader.ReadUInt16();
         idxEqpHead = reader.ReadUInt16();
         idxEqpTalisman = reader.ReadUInt16();

         // Item Box
         reader.BaseStream.Seek(0x15E, SeekOrigin.Begin);
         for (int i = 0; i < 1400; i++)
         {
            itemBox[i] = new MonHunItem(reader.ReadUInt16(), reader.ReadUInt16());
         }

         // Equipment Box
         reader.BaseStream.Seek(0x173E, SeekOrigin.Begin);
         for (int i = 0; i < 1400; i++)
         {
            equipBox[i] = MonHunEquip.Create(reader.ReadBytes(28));
         }

         // Guild Quests
         reader.BaseStream.Seek(0xDCA8, SeekOrigin.Begin);
         for (int i = 0; i < 10; i++)
         {
            guildQuests[i] = new GuildQuest(reader.ReadBytes(304));
         }

         // Palicos
         palicos = new Palico[56];
         reader.BaseStream.Seek(0xC4B0, SeekOrigin.Begin);
         palicos[0] = new Palico(reader.ReadBytes(232));
         reader.BaseStream.Seek(0xF338, SeekOrigin.Begin);
         for (int i = 1; i < 6; i++)
         {
            palicos[i] = new Palico(reader.ReadBytes(232));
         }
         for (int i = 6; i < 56; i++)
         {
            palicos[i] = new Palico(reader.ReadBytes(232));
         }
      }
   }
}
