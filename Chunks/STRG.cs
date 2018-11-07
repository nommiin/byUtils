using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace byUtils {
    public class Stringy {
        public UInt32 Offset;
        public string Value;
        public long Base;
        public Stringy(BinaryReader brMain) {
            this.Offset = brMain.ReadUInt32();
            this.Base = brMain.BaseStream.Position;
            brMain.BaseStream.Seek(this.Offset, SeekOrigin.Begin);
            this.Value = Encoding.ASCII.GetString(brMain.ReadBytes((int)brMain.ReadUInt32()));
            brMain.BaseStream.Seek(this.Base, SeekOrigin.Begin);
        }
    }

    public static class STRG {
        public static void Read(BinaryReader brMain) {
            List<Stringy> brStrings = new List<Stringy>();
            for (int i = 0, _i = (int)brMain.ReadUInt32(); i < _i; i++) {
                brStrings.Add(new Stringy(brMain));
                Console.WriteLine(brStrings[brStrings.Count - 1]);

            }
        }
    }
}