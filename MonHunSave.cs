using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mh4edit
{
   public class MonHunSave
   {
      public MonHunCharacter slot;
      public string FileName = string.Empty;
      public MemoryStream ms = null;
      public byte[] Buffer = [];
      public ushort Key = 0;

      
      public MonHunSave(string fileName)
      {
         FileName = fileName;
         
         using (FileStream fs = new FileStream(fileName, FileMode.Open))
         {
            BinaryReader reader = new BinaryReader(fs);

            Buffer = reader.ReadBytes(81408);

            for(int i=0; i < Buffer.Length; i+=4)
            {
               var tmp = Buffer[i];
               Buffer[i] = Buffer[i+3];
               Buffer[i+3] = tmp;
               tmp = Buffer[i+1];
               Buffer[i+1] = Buffer[i+2];
               Buffer[i+2] = tmp;
            }

            Blowfish bf = new Blowfish(Encoding.ASCII.GetBytes("blowfish key iorajegqmrna4itjeangmb agmwgtobjteowhv9mope"));
            bf.Decipher(Buffer, Buffer.Length);

            for(int i=0; i < Buffer.Length; i+=4)
            {
               var tmp = Buffer[i];
               Buffer[i] = Buffer[i+3];
               Buffer[i+3] = tmp;
               tmp = Buffer[i+1];
               Buffer[i+1] = Buffer[i+2];
               Buffer[i+2] = tmp;
            }

            var sixteen = BitConverter.ToUInt16(Buffer, 0);
            Key = BitConverter.ToUInt16(Buffer, 2);
            var key = Key;

            for(int i=4; i<Buffer.Length; i+=2)
            {
               var val = BitConverter.ToUInt16(Buffer, i);
               if(key==0)
                  key = 1;
               key = (ushort)(key * 0xb0 % 0xff53);
               val ^= key;
               Buffer[i] = (byte)(val & 0xFF);
               Buffer[i + 1] = (byte)((val >> 8) & 0xFF);
            }

            ms = new MemoryStream(Buffer, 8, 81400);
            slot = new MonHunCharacter(ms);
         }
      }

      public void Save()
      {
         // actual data
         ms.Seek(0, SeekOrigin.Begin);
         ms.Write(slot.SerializeBase());
         ms.Seek(0x40, SeekOrigin.Begin);
         ms.Write(slot.SerializeCharacterEquip());
         ms.Seek(0x15E, SeekOrigin.Begin);
         ms.Write(slot.SerializeItemBox());
         ms.Seek(0x173E, SeekOrigin.Begin);
         ms.Write(slot.SerializeEquipmentBox());
         ms.Seek(0xDCA8, SeekOrigin.Begin);
         ms.Write(slot.SerializeActiveGq());

         // from here on work on a duplicate of the decrypted buffer
         var outBuf = new byte[81408];
         Array.Copy(Buffer, outBuf, Buffer.Length);

         // encryption
         uint csum = 0;
         for(int i=8; i<outBuf.Length; i++)
         {
            csum += outBuf[i];
         }
         BitConverter.GetBytes((ushort)16).CopyTo(outBuf, 0);
         BitConverter.GetBytes((ushort)Key).CopyTo(outBuf, 2);
         BitConverter.GetBytes(csum).CopyTo(outBuf, 4);

         var key = Key;
         for(int i=4; i<outBuf.Length; i+=2)
         {
            var val = BitConverter.ToUInt16(outBuf, i);
            if(key==0)
               key = 1;
            key = (ushort)(key * 0xb0 % 0xff53);
            val ^= key;
            outBuf[i] = (byte)(val & 0xFF);
            outBuf[i + 1] = (byte)((val >> 8) & 0xFF);
         }

         for(int i=0; i < outBuf.Length; i+=4)
         {
            var tmp = outBuf[i];
            outBuf[i] = outBuf[i+3];
            outBuf[i+3] = tmp;
            tmp = outBuf[i+1];
            outBuf[i+1] = outBuf[i+2];
            outBuf[i+2] = tmp;
         }

         Blowfish bf = new Blowfish(Encoding.ASCII.GetBytes("blowfish key iorajegqmrna4itjeangmb agmwgtobjteowhv9mope"));
         bf.Encipher(outBuf, outBuf.Length);

         for(int i=0; i < outBuf.Length; i+=4)
         {
            var tmp = outBuf[i];
            outBuf[i] = outBuf[i+3];
            outBuf[i+3] = tmp;
            tmp = outBuf[i+1];
            outBuf[i+1] = outBuf[i+2];
            outBuf[i+2] = tmp;
         }

         // write file
         using (var file = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
         {
            file.Write(outBuf);
            file.Close();
         }
      }
   }
}
