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
    public FolderInfo sl { get; private set; }  //現在のフォルダの全曲リスト
    public int cursol { get; set; }  //現在選択されている曲のインデックス
    public int difficult { get; set; }  //現在選択されている難易度

    //外の各コンポーネントをアタッチする用の変数。
    //Unity側でアタッチする必要があるので注意
    [SerializeField] private TextMeshProUGUI uiLowDiff;
    [SerializeField] private TextMeshProUGUI uiMidDiff;
    [SerializeField] private TextMeshProUGUI uiHighDiff;
    [SerializeField] private TextMeshProUGUI uiSongName;
    [SerializeField] private TextMeshProUGUI uiArtist;
    [SerializeField] private TextMeshProUGUI uiGenre;
    [SerializeField] private TextMeshProUGUI uiArranger;
    [SerializeField] private TextMeshProUGUI uiHighScore;
    [SerializeField] private TextMeshProUGUI uiMaxCombo;
    [SerializeField] private TextMeshProUGUI uiPlayCount;
    [SerializeField] private TextMeshProUGUI uiClearRank;
    [SerializeField] private TextMeshProUGUI uiDispBpm;
    [SerializeField] private TextMeshProUGUI uiNotesNum;
    [SerializeField] private Image IMG_jacket;

    private void Awake() 
    {
        cursol = 0;
        difficult = myConstants.LowDiff;
        uiLowDiff.color = myConstants.DiffColor[myConstants.LowDiff];
        uiMidDiff.color = myConstants.DiffColor[myConstants.MidDiff];
        uiHighDiff.color = myConstants.DiffColor[myConstants.HighDiff];
    }

    private void Update() 
    {
        SongInfo nowSong = sl.Songs[cursol];

        uiLowDiff.text = nowSong.PlayLevel[myConstants.LowDiff].ToString();
        uiMidDiff.text = nowSong.PlayLevel[myConstants.MidDiff].ToString();
        uiHighDiff.text = nowSong.PlayLevel[myConstants.HighDiff].ToString();

        uiSongName.text = "Title:" + nowSong.SongName;
        uiArtist.text = "Artist:" + nowSong.Artist;
        uiGenre.text = "Genre:" + nowSong.Genre;
        uiArranger.text = "Arranger:" + nowSong.Arranger[difficult];

        uiHighScore.text = "High Score:" + nowSong.HiScore[difficult].ToString();
        uiMaxCombo.text = "Max Combo:" + nowSong.MaxCombo[difficult].ToString();
        uiPlayCount.text = "Play Count:" + nowSong.PlayCount[difficult].ToString();
        uiClearRank.text = "Clear Rank:" + nowSong.PlayRank[difficult];

        uiDispBpm.text = "BPM:" + nowSong.DispBpm;
        uiNotesNum.text = "Notes:" + nowSong.NotesNum[difficult].ToString();

        Texture2D tex = readByBinary(readPngFile(
            nowSong.Path + "\\" + "Jacket.jpg"));
        IMG_jacket.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero); 
    }

    /// <summary>
    /// 画像を読んでくるための奴
    /// Png用っぽい。
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] readPngFile(string path)
    {
        using (FileStream fileStream = new FileStream (path, FileMode.Open, FileAccess.Read)) {
            BinaryReader bin = new BinaryReader (fileStream);
            byte[] values = bin.ReadBytes ((int)bin.BaseStream.Length);
            bin.Close ();
            return values;
        }
    }

    /// <summary>
    /// バイナリで取ってきた画像データを読むメソッドぽい
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Texture2D readByBinary(byte[] bytes)
    {
        Texture2D texture = new Texture2D (1, 1);
        texture.LoadImage (bytes);
        return texture;
    }
}