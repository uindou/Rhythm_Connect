using System.Collections.Generic;

namespace RC.Model
{
    class SongModel
    {
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Artist { get; private set; }
        public string DispBpm { get; private set; }
        public List<string> Arranger { get; private set; }
        public List<int> PlayLevel { get; private set; }
        public List<int> NotesNum { get; private set; }
        public List<SheetModel> Sheets { get; private set; }

        public SongModel(Dictionary<string, object> songData)
        {
            Title = (songData["title"] != null) ? songData["title"].ToString() : "";
            Genre = (songData["genre"] != null) ? songData["genre"].ToString() : "";
            Artist = (songData["artist"] != null) ? songData["artist"].ToString() : "";
            DispBpm = (songData["dispbpm"] != null) ? songData["dispbpm"].ToString() : "";
            Arranger = (songData["arranger"] != null) ? songData["arranger"] as List<string> : new List<string>() { "", "", "" };
            PlayLevel = (songData["playlevel"] != null) ? songData["playlevel"] as List<int> : new List<int>() { 0, 0, 0 };
            NotesNum = (songData["notesnum"] != null) ? songData["notesnum"] as List<int> : new List<int>() { 0, 0, 0 };
            Sheets = (songData["sheets"] != null) ? songData["sheets"] as List<SheetModel> : new List<SheetModel>();
        }
    }
}