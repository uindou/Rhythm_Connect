using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// スクロールバーに表示する全曲と、現在選択されている曲を保持する。
/// また、現在選択されている曲に応じて各表示項目を操作する。
/// </summary>
public class SongSelecter : MonoBehaviour {
    private GameManager gm;
    private SongList sl;
    private int cursol;
    [SerializeField] private TextMeshProUGUI UI_low_level;
    [SerializeField] private TextMeshProUGUI UI_mid_level;
    [SerializeField] private TextMeshProUGUI UI_high_level;
    [SerializeField] private TextMeshProUGUI UI_MusicInfo;
    [SerializeField] private TextMeshProUGUI UI_Artist;
    [SerializeField] private TextMeshProUGUI UI_Genre;
    [SerializeField] private TextMeshProUGUI UI_Arranger;
    [SerializeField] private TextMeshProUGUI UI_HighScore;
    [SerializeField] private TextMeshProUGUI UI_MaxCombo;
    [SerializeField] private TextMeshProUGUI UI_PlayCount;
    [SerializeField] private TextMeshProUGUI UI_ClearRank;
    [SerializeField] private TextMeshProUGUI UI_BPM;
    [SerializeField] private TextMeshProUGUI UI_Notes;
    [SerializeField] private Image IMG_jacket;

    private void Awake() 
    {
        gm = GameManager.Find("GameManager").GetComponent<GameManager>();
        this.sl = gm.sl;
        cursol = 0;
    }

    private void Update() 
    {
        SongInfo nowSong = sl.Songs[cursol];

        UI_low_level.text = nowSong.PlayLevel[myConstants.LowDiff.ToString()];
        UI_mid_level.text = nowSong.PlayLevel[myConstants.MidDiff.ToString()];
        UI_high_level.text = nowSong.PlayLevel[myConstants.HighDiff.ToString()];

        UI_MusicInfo.text = "Title:" + nowSong.SongName;
        UI_Artist.text = "Artist:" + nowSong.Artist;
        UI_Genre.text = "Genre:" + nowSong.Genre;
        UI_Arranger.text = "Arranger:" + nowSong.Arranger[myConstants.LowDiff];

        UI_HighScore.text = "High Score:" + nowSong.HiScore[myConstants.LowDiff].ToString();
        UI_MaxCombo.text = "Max Combo:" + nowSong.MaxCombo[myConstants.LowDiff].ToString();
        UI_PlayCount.text = "Play Count:" + nowSong.PlayCount[myConstants.LowDiff].ToString();
        UI_ClearRank.text = "Clear Rank:" + "A";

        UI_BPM.text = "BPM:" + nowSong.DispBpm;
        UI_Notes.text = "Notes:" + nowSong.NotesNum;

        Texture2D tex = readByBinary(readPngFile(
            myConstants.SongDataFolderPath + "\\" + myConstants.ModeString[nowSong.Mode] + "\\" + nowSong.SongName + "\\" + "Jacket.jpg"));
    }
}