using System;
using System.IO;
using System.Text;

namespace byUtils {
    class Chunk {
        public string Name;
        public UInt32 Length;
        public long Base;
        public Chunk(BinaryReader brMain) {
            this.Name = Encoding.ASCII.GetString(brMain.ReadBytes(4));
            this.Length = brMain.ReadUInt32();
            this.Base = brMain.BaseStream.Position;
        }

        public override string ToString() {
            return "Name: " + this.Name + ", Length: " + this.Length.ToString();
        }
    }

    class Program {
        public static string byFile = @"/Users/torinfreimiller/Library/Application Support/com.yoyogames.macyoyorunner/game/assets/game.ios";
        static void Main(string[] args) {

            if (File.Exists(byFile) == true) {
                // Read the file 
                using (BinaryReader brMain = new BinaryReader(File.OpenRead(byFile))) {
                    Chunk byForm = new Chunk(brMain);
                    if (byForm.Name == "FORM") {
                        while (brMain.BaseStream.Position < byForm.Length) {
                            Chunk byChunk = new Chunk(brMain);
                            switch (byChunk.Name) {
                            case "STRG": {
                                    STRG.Read(brMain);
                                    break;
                                }

                            default: {
                                    Console.WriteLine("Chunk '" + byChunk.Name + "' is not supported.");
                                    break;
                                }
                            }
                            brMain.BaseStream.Seek(byChunk.Base + byChunk.Length, SeekOrigin.Begin);
                        }
                    }
                }

            }

        }
    }
}