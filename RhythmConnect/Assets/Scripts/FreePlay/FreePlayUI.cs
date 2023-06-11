using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using static myConstants;
using TMPro;

/// <summary>
/// 現在フォルダと曲のどちらを表示しているのかを表すための列挙体
/// </summary>
enum Phase
{
    folder
    , song
}

/// <summary>
/// FreePlayでフォルダ・曲をスクロールビューに表示するクラス
/// </summary>
public class FreePlayUI : MonoBehaviour
{

#region "フィールド・プロパティ宣言"
    private List<FolderInfo> Folders = new List<FolderInfo>();
    private List<SongInfo> Songs = new List<SongInfo>();
    private Phase NowPhase = Phase.folder;
    private List<GameObject> buttons = new List<GameObject>();
    public int Difficult { get; set; }
    private int _Cursol;

    public int Cursol
    {
        get { return _Cursol; }
        set
        {
            //カーソルが向いているボタンの色を変えるための処理。
            if(buttons.Count > _Cursol)
            {
                buttons[_Cursol].GetComponent<Button>().image.color = ButtonDefault;
            }
            
            if(buttons.Count > value)
            {
                _Cursol = value;
            }
            else
            {
                _Cursol = 0;
            }
            
            buttons[_Cursol].GetComponent<Button>().image.color = ButtonFocus;
        }
    }

    private GameObject ButtonCancel;    //非活性にするとFindで見つからなくなるのでオブジェクトをつかんでおく。
    private GameObject Content;     //スクロールビューの表示要素
    private GameObject ButtonPrefab;    //ボタン生成用のテンプレート
    private GameObject FolderDesc;
    private GameObject SongDesc;

    //フォルダ詳細オブジェクト
    private TextMeshProUGUI FolderTitle;
    private TextMeshProUGUI SongsNum;
    private TextMeshProUGUI RankSSSNum;
    private TextMeshProUGUI RankSSNum;
    private TextMeshProUGUI RankSNum;
    private TextMeshProUGUI RankPNum;
    private TextMeshProUGUI FCNum;
    private TextMeshProUGUI FolderText;
    private Image FolderJacket;

    //曲詳細オブジェクト
    private TextMeshProUGUI LevelLowDiff;
    private TextMeshProUGUI LevelMidDiff;
    private TextMeshProUGUI LevelHighDiff;
    private TextMeshProUGUI SongTitle;
    private TextMeshProUGUI Artist;
    private TextMeshProUGUI Genre;
    private TextMeshProUGUI Arranger;
    private TextMeshProUGUI HighScore;
    private TextMeshProUGUI MaxCombo;
    private TextMeshProUGUI PlayCount;
    private TextMeshProUGUI ClearRank;
    private TextMeshProUGUI DispBpm;
    private TextMeshProUGUI NotesNum;
    private Image SongJacket;

#endregion

#region "Unity標準メソッド"
    private void Awake()
    {
        //変数の初期化
        Difficult = myConstants.LowDiff;

        //フォルダ一覧のロード
        string fp = SongDataFolderPath + '\\' + ModeString[Rc];
        if(LoadFolderList(fp) == false)
        {
            Debug.Log("Failure Folder Load");
        }

        //各種Unityオブジェクトの読込
        //UIパーツ
        Content = GameObject.Find("Content");
        ButtonPrefab = (GameObject)Resources.Load("Prefabs/Button");
        ButtonCancel = GameObject.Find("ButtonCancel");
        FolderDesc = GameObject.Find("Folder Description");
        SongDesc = GameObject.Find("Song Description");

        //フォルダ情報オブジェクト
        FolderTitle = GameObject.Find("FolderTitle").GetComponent<TextMeshProUGUI>();
        SongsNum = GameObject.Find("SongsNum").GetComponent<TextMeshProUGUI>();
        RankSSSNum = GameObject.Find("RankSSSNum").GetComponent<TextMeshProUGUI>();
        RankSSNum = GameObject.Find("RankSSNum").GetComponent<TextMeshProUGUI>();
        RankSNum = GameObject.Find("RankSNum").GetComponent<TextMeshProUGUI>();
        RankPNum = GameObject.Find("RankPNum").GetComponent<TextMeshProUGUI>();
        FCNum = GameObject.Find("FCNum").GetComponent<TextMeshProUGUI>();
        FolderText = GameObject.Find("FolderText").GetComponent<TextMeshProUGUI>();
        FolderJacket = GameObject.Find("FolderJacket").GetComponent<Image>();

        //曲情報オブジェクト
        SongTitle = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        Artist = GameObject.Find("Artist").GetComponent<TextMeshProUGUI>();
        Genre = GameObject.Find("Genre").GetComponent<TextMeshProUGUI>();
        Arranger = GameObject.Find("Arranger").GetComponent<TextMeshProUGUI>();
        HighScore = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();
        MaxCombo = GameObject.Find("MaxCombo").GetComponent<TextMeshProUGUI>();
        PlayCount = GameObject.Find("PlayCount").GetComponent<TextMeshProUGUI>();
        ClearRank = GameObject.Find("ClearRank").GetComponent<TextMeshProUGUI>();
        DispBpm = GameObject.Find("BPM").GetComponent<TextMeshProUGUI>();
        NotesNum = GameObject.Find("Notes").GetComponent<TextMeshProUGUI>();
        LevelLowDiff = GameObject.Find("textLowDiff").GetComponent<TextMeshProUGUI>();
        LevelMidDiff = GameObject.Find("textMidDiff").GetComponent<TextMeshProUGUI>();
        LevelHighDiff = GameObject.Find("textHighDiff").GetComponent<TextMeshProUGUI>();
        SongJacket = GameObject.Find("SongJacket").GetComponent<Image>();
    }

    private void Start()
    {
        //最初はフォルダ選択からスタート
        this.SetViewFromFolders();
        //最初はボタンに色ついてないのでCursolのSetプロパティを呼び出しつつ初期化
        Cursol = 0;
    }

    private void Update()
    {
        switch(NowPhase)
        {
            case Phase.folder:
                FolderInfo nowFolder = Folders[Cursol];
                SongTitle.text = nowFolder.name;
                break;

            case Phase.song:
                SongInfo nowSong = this.Songs[Cursol];
                LevelLowDiff.text = nowSong.PlayLevel[LowDiff].ToString();
                LevelMidDiff.text = nowSong.PlayLevel[MidDiff].ToString();
                LevelHighDiff.text = nowSong.PlayLevel[HighDiff].ToString();
                SongTitle.text = "Title:" + nowSong.SongName;
                Artist.text = "Artist:" + nowSong.Artist;
                Genre.text = "Genre:" + nowSong.Genre;
                Arranger.text = "Arranger:" + nowSong.Arranger[Difficult];
                HighScore.text = "High Score:" + nowSong.HiScore[Difficult].ToString();
                MaxCombo.text = "Max Combo:" + nowSong.MaxCombo[Difficult].ToString();
                PlayCount.text = "Play Count:" + nowSong.PlayCount[Difficult].ToString();
                ClearRank.text = "Clear Rank:" + nowSong.PlayRank[Difficult];
                DispBpm.text = "BPM:" + nowSong.DispBpm;
                NotesNum.text = "Notes:" + nowSong.NotesNum[Difficult].ToString();

                Texture2D tex = readByBinary(readPngFile(nowSong.Path + "\\" + "Jacket.jpg"));     
                SongJacket.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                break;
        }
    }

#endregion

#region "外部メソッド"
    /// <summary>
    /// 決定ボタンが押されたときに呼ばれるメソッド
    /// </summary>
    /// <returns>成否</returns>
    public bool ClickDecide()
    {
        switch(NowPhase)
        {  
            //フォルダと曲のどちらを選択したかで分岐
            case Phase.folder:
                if(!SetViewFromSongs())
                {
                    return false;
                }
                break;
            case Phase.song:
                //曲決定処理
                break;
        }

        return true;
    }

    /// <summary>
    /// キャンセルボタンが押されたときに呼ばれるメソッド
    /// </summary>
    /// <returns>成否</returns>
    public bool ClickCancel()
    {
        switch(NowPhase)
        {
            case Phase.folder:
                //基本的にフォルダ選択のフェイズではCancelボタンは無効化されているので通らないはず
                ButtonCancel.SetActive(false);
                break;
            
            case Phase.song:
                this.SetViewFromFolders();
                break;
        }

        return true;
    }

    /// <summary>
    /// 生成したボタンにアタッチするOnClick処理
    /// </summary>
    /// <param name="index"></param>
    public void ButtonClickEvent(int index)
    {
        GameObject.Find("Canvas").GetComponentInChildren<FreePlayUI>().Cursol = index;
    }

#endregion

#region "内部メソッド"
    /// <summary>
    /// スクロールビューに追加するボタンの生成処理
    /// </summary>
    /// <param name="buttontext"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private GameObject CreateButton(string buttontext, int index)
    {
        //プレハブを元にボタンを生成
        GameObject _button = Instantiate(ButtonPrefab, Content.transform);
        _button.GetComponent<Button>().onClick.AddListener(() => ButtonClickEvent(index));
        Text _buttonText = _button.GetComponentInChildren<Text>();
        _buttonText.text = buttontext;
        return _button;
    }

    /// <summary>
    /// フォルダ一覧の読込
    /// </summary>
    /// <param name="parentfolderpath"></param>
    /// <returns></returns>
    private bool LoadFolderList(string parentfolderpath)
    {
        List<string> folderpaths;
        folderpaths = LoadSubFolderToList(parentfolderpath);
        if(folderpaths == null)
        {
            return false;
        }

        foreach(string folderpath in folderpaths)
        {
            FolderInfo _fi = new FolderInfo(folderpath);
            Folders.Add(_fi);
        }

        return true;
    }

    /// <summary>
    /// 曲一覧をスクロールビューにセットする
    /// </summary>
    /// <returns>成否</returns>
    private bool SetViewFromSongs()
    {
        if(Folders[Cursol] == null)
        {
            return false;
        }

        if(Folders[Cursol].LoadSongList() == false)
        {
            return false;
        }

        this.Songs = Folders[Cursol].Songs;

        this.ClearButtons();

        // ボタンにインデックス情報を付加したいのでforeachは使わない
        for(int i = 0; i < Folders[Cursol].Songs.Count; i++)
        {
            SongInfo _songinfo = Folders[Cursol].Songs[i];
            buttons.Add(CreateButton(_songinfo.SongName, i));
        }

        //曲一覧表示時はCancelボタンを活性化
        ButtonCancel.SetActive(true);
        Cursol = 0;
        NowPhase = Phase.song;
        FolderDesc.SetActive(false);
        SongDesc.SetActive(true);

        return true;
    }

    /// <summary>
    /// フォルダ一覧をスクロールビューにセットする
    /// </summary>
    /// <returns>成否</returns>
    private bool SetViewFromFolders()
    {
        this.ClearButtons();

        // ボタンにインデックス情報を付加したいのでforeachは使わない
        for(int i = 0; i < Folders.Count; i++)
        {
            FolderInfo _folder = Folders[i];
            buttons.Add(CreateButton(_folder.name, i));
        }
        
        //フォルダ一覧表示時はCancelボタンは使わないので消す
        ButtonCancel.SetActive(false);
        FolderDesc.SetActive(true);
        SongDesc.SetActive(false);
        NowPhase = Phase.folder;
        Cursol = 0;

        return true;
    }

    /// <summary>
    /// スクロールビュー上のボタンの全削除
    /// ボタン自体の破棄に加えて、ボタン管理用の配列もクリアする。
    /// </summary>
    private void ClearButtons()
    {
        foreach(GameObject _button in buttons)
        {
            if(_button != null)
            {
                Destroy(_button);
            }
        }

        buttons.Clear();
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

#endregion
}