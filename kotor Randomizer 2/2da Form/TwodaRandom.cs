using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using KotOR_IO;

namespace kotor_Randomizer_2
{
    public static class TwodaRandom
    {
        public static void Twoda_rando(Globals.KPaths paths)
        {
            BIF b = KReader.ReadBIF(File.OpenRead(paths.data + "\\2da.bif"));
            KEY k = KReader.ReadKEY(File.OpenRead(paths.get_backup(paths.chitin)));
            b.attachKey(k, "data\\2da.bif");

            foreach (BIF.Var_Res_Entry VRE in b.Variable_Resource_Table.Where(x => Globals.Selected2DAs.Keys.Contains(x.ResRef)))
            {
                TwoDA_REFRACTOR t = new TwoDA_REFRACTOR(new MemoryStream(VRE.Entry_Data), VRE.ResRef);

                foreach (string col in Globals.Selected2DAs[VRE.ResRef])
                {
                    Randomize.FisherYatesShuffle(t.Data[col]);
                }

                t.write(File.OpenWrite(paths.Override + t.name + ".2da"));
            }
            

        }
    }

    //I created this class to deal with how awful the Kotor_IO version of 2DA I coded was, fixing all that garbage will be a future project.
    class TwoDA_REFRACTOR
    {
        public string name;

        public string FileType;

        public string Version;

        private int _row_count;

        public List<string> Columns = new List<string>();

        public Dictionary<string, List<string>> Data = new Dictionary<string, List<string>>();

        public int row_count
        {
            get
            {
                return _row_count;
            }
        }

        public TwoDA_REFRACTOR(Stream s, string name)
        {
            this.name = name;
            BinaryReader br = new BinaryReader(s);

            //Get header info
            FileType = new string(br.ReadChars(4));
            Version = new string(br.ReadChars(4));

            br.ReadByte();

            //Get Column Labels
            List<char> TempString = new List<char>();

            while (br.PeekChar() != 0)
            {
                TempString.Clear();
                while (br.PeekChar() != 9) //May have to make this go one past the current limit
                {
                    TempString.Add(br.ReadChar());
                }
                Columns.Add(new string(TempString.ToArray()));
                br.ReadByte();
            }

            br.ReadByte();

            //Get row count
            _row_count = br.ReadInt32();

            //Skip row indexes (maybe a bad idea, but who cares)
            for (int i = 0; i < _row_count; i++)
            {
                while (br.PeekChar() != 9)
                {
                    br.ReadByte();
                }
                br.ReadByte();
            }

            //generate index column
            List<string> index_list = new List<string>();
            for (int i = 0; i < _row_count; i++) { index_list.Add(Convert.ToString(i)); }
            Data.Add("row_index", index_list);

            //populate collumn keys
            foreach (string c in Columns) { List<string> tempColumn = new List<string>(); Data.Add(c, tempColumn); }

            List<short> Offsets = new List<short>();

            //get offsets
            for (int i = 0; i < (1 + (_row_count * Columns.Count())); i++) //iterates through the number of cells
            {
                Offsets.Add(br.ReadInt16());
            }
            int DataOffset = (int)br.BaseStream.Position;

            //Populate data
            int OffsetIndex = 0;
            for (int i = 0; i < _row_count; i++)
            {
                for (int k = 0; k < Columns.Count(); k++)
                {
                    br.BaseStream.Seek(DataOffset + Offsets[OffsetIndex], SeekOrigin.Begin);
                    TempString.Clear();
                    while (br.PeekChar() != 0)
                    {
                        TempString.Add(br.ReadChar());
                    }
                    Data[Columns[k]].Add(new string(TempString.ToArray()));
                    br.ReadByte();
                    OffsetIndex++;
                }
            }
            br.Close();
        }

        public void write(Stream s)
        {
            BinaryWriter bw = new BinaryWriter(s);

            //Header
            bw.Write(FileType.ToArray());
            bw.Write(Version.ToArray());

            bw.Write((byte)10);

            //Column Labels
            foreach (string c in Columns)
            {
                bw.Write(c.ToArray());
                bw.Write((byte)9);
            }

            bw.Write((byte)0);

            //Row Count
            bw.Write(_row_count);

            //Row Indexs
            for (int i = 0; i < _row_count; i++)
            {
                bw.Write(Convert.ToString(i).ToArray());
                bw.Write((byte)9);
            }

            //Offset Generation
            List<string> All_Values = new List<string>();
            foreach (string c in Columns)
            {
                All_Values.AddRange(Data[c].Distinct());
            }
            All_Values = All_Values.Distinct().ToList();

            Dictionary<string, short> offset_table = new Dictionary<string, short>();

            short curr_offset = 0;
            foreach (string v in All_Values)
            {
                offset_table.Add(v, curr_offset);
                curr_offset += (short)v.Length;
                curr_offset++;
            }

            for (int i = 0; i < _row_count; i++)
            {
                foreach (string c in Columns)
                {
                    bw.Write(offset_table[Data[c][i]]);
                }
            }
            bw.Write(curr_offset);

            foreach (string k in offset_table.Keys)
            {
                bw.Write(k.ToArray());
                bw.Write('\0');
            }


        }
    }
}
