using System.IO;
using System.Collections.Generic;

namespace RC.Model
{
    class SongModel
    {
        public string Name { get; private set; }
        public string Genre { get; private set; }
        public string Artist { get; private set; }
        public string DispBpm { get; private set; }
        public string[] Arranger { get; private set; } = new string[3];
        public int[] PlayLevel { get; private set; } = new int[3];
        public int[] NotesNum { get; private set; } = new int[3];

        public SongModel(   string name, string genre, string artist
                        ,   string dispBpm, string[] arranger, int[] playLevel
                        ,   int[] notesNum)
        {
            this.Name = name;
            this.Genre = genre;
            this.Artist = artist;
            this.DispBpm = dispBpm;
            this.Arranger = arranger;
            this.PlayLevel = playLevel;
            this.NotesNum = notesNum;
        }
    }
}