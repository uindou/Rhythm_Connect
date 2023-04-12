using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using TMPro;

public class SongDetailKettei : UIBehaviour
{
    [SerializeField] TextMeshProUGUI music_name_button;
    [SerializeField] TextMeshProUGUI easy_level_button;
    [SerializeField] TextMeshProUGUI normal_level_button;
    [SerializeField] TextMeshProUGUI hard_level_button;

    [SerializeField] TextMeshProUGUI UI_low_level;
    [SerializeField] TextMeshProUGUI UI_mid_level;
    [SerializeField] TextMeshProUGUI UI_high_level;

    [SerializeField] TextMeshProUGUI UI_MusicInfo;
    [SerializeField] TextMeshProUGUI UI_Artist;
    [SerializeField] TextMeshProUGUI UI_Genre;
    [SerializeField] TextMeshProUGUI UI_Arranger;

    [SerializeField] TextMeshProUGUI UI_HighScore;
    [SerializeField] TextMeshProUGUI UI_MaxCombo;
    [SerializeField] TextMeshProUGUI UI_PlayCount;
    [SerializeField] TextMeshProUGUI UI_ClearRank;

    [SerializeField] TextMeshProUGUI UI_BPM;
    [SerializeField] TextMeshProUGUI UI_Notes;

    private GameObject go;
    [SerializeField] private Image jacket;
    private GameManager gm;
    private List<SongInfo> songList;

    private int cursol = 0;
    private string low,mid,high;
    private string music_name,artist,genre,arranger;
    private string high_score, max_combo, play_count, clear_rank;
    private string bpm, notes;
    private static readonly string folder = "All";

    void Awake()
    {
        Debug.Log("Awake");
        go = GameObject.Find("GameManager");
        Debug.Log("GameObject Load");
        gm = go.GetComponent<GameManager>();
        Debug.Log("GameManager Load");
        songList = gm.sl.Songs;

        Debug.Log(songList.Count);
    }

    public void UpdateItem(int count)
    {
        int ringcount = count % songList.Count;

        low = songList[ringcount].PlayLevel[myConstants.LowDiff].ToString();
        mid = songList[ringcount].PlayLevel[myConstants.MidDiff].ToString();
        high = songList[ringcount].PlayLevel[myConstants.HighDiff].ToString();

        music_name = songList[ringcount].SongName;
        artist = songList[ringcount].Artist;
        genre = songList[ringcount].Genre;
        arranger = songList[ringcount].Arranger[myConstants.LowDiff];

        high_score = songList[ringcount].HiScore[myConstants.LowDiff].ToString();
        max_combo = songList[ringcount].MaxCombo[myConstants.LowDiff].ToString();
        play_count = songList[ringcount].PlayCount[myConstants.LowDiff].ToString();
        clear_rank = "A";

        bpm = songList[ringcount].DispBpm;
        notes = songList[ringcount].NotesNum[myConstants.LowDiff].ToString();
        cursol = ringcount;


        music_name_button.text = music_name;
        easy_level_button.text = low;
        normal_level_button.text = mid;
        hard_level_button.text = high;


    }
    public void OnClick()
    {
        UI_low_level.text = low;
        UI_mid_level.text = mid;
        UI_high_level.text = high;

        UI_MusicInfo.text = "Title:"+music_name;
        UI_Artist.text = "Artist:"+artist;
        UI_Genre.text = "Genre:"+genre;
        UI_Arranger.text = "Arranger:"+arranger;

        UI_HighScore.text = "High Score:"+high_score;
        UI_MaxCombo.text = "Max Combo:"+max_combo;
        UI_PlayCount.text = "Play Count:"+play_count;
        UI_ClearRank.text = "Clear Rank:"+clear_rank;

        UI_BPM.text = "BPM:"+bpm;
        UI_Notes.text = "Notes:"+notes;

        Texture2D tex = readByBinary(readPngFile(myConstants.SongDataFolderPath + "\\" + myConstants.ModeString[songList[cursol].Mode] + "\\" + songList[cursol].SongName + "\\" + "Jacket.jpg"));
        jacket.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero); 
    }

    public byte[] readPngFile(string path)
    {
        using (FileStream fileStream = new FileStream (path, FileMode.Open, FileAccess.Read)) {
            BinaryReader bin = new BinaryReader (fileStream);
            byte[] values = bin.ReadBytes ((int)bin.BaseStream.Length);
            bin.Close ();
            return values;
        }
    }

    public Texture2D readByBinary(byte[] bytes)
    {
        Texture2D texture = new Texture2D (1, 1);
        texture.LoadImage (bytes);
        return texture;
    }
}


