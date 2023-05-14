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
    ,song
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
    private int _Cursol;

    public int Cursol
    {
        get { return _Cursol; }
        set
        {
            //カーソルが向いているボタンの色を変えるための処理。
            buttons[_Cursol].GetComponent<Button>().image.color = ButtonDefault;
            _Cursol = value;
            buttons[_Cursol].GetComponent<Button>().image.color = ButtonFocus;

        }
    }

    private GameObject ButtonCancel;    //非活性にするとFindで見つからなくなるのでオブジェクトをつかんでおく。
    private GameObject Content;     //スクロールビューの表示要素
    private GameObject ButtonPrefab;    //ボタン生成用のテンプレート
    private GameObject LowDiff;
    private GameObject MidDiff;
    private GameObject HighDiff;
    private GameObject SongTitle;
    private GameObject Artist;
    private GameObject Genre;
    private GameObject Arranger;
    private GameObject HighScore;
    private GameObject MaxCombo;
    private GameObject PlayCount;
    private GameObject ClearRank;
    private GameObject DispBpm;
    private GameObject NotesNum;
    private Image IMG_jacket;

#endregion

#region "Unity標準メソッド"
    private void Awake()
    {
        //フォルダ一覧のロード
        string fp = SongDataFolderPath + '\\' + ModeString[Rc];
        if(LoadFolderList(fp) == false)
        {
            Debug.Log("Failure Folder Load");
        }

        //各種Unityオブジェクトの読込
        Content = GameObject.Find("Content");
        ButtonPrefab = (GameObject)Resources.Load("Prefabs/Button");
        ButtonCancel = GameObject.Find("ButtonCancel");
        SongTitle = GameObject.Find("Title");
        Artist = GameObject.Find("Artist");
        Genre = GameObject.Find("Genre");
        Arranger = GameObject.Find("Arranger");
        HighScore = GameObject.Find("HighScore");
        MaxCombo = GameObject.Find("MaxCombo");
        PlayCount = GameObject.Find("PlayCount");
        ClearRank = GameObject.Find("ClearRank");
        DispBpm = GameObject.Find("BPM");
        NotesNum = GameObject.Find("Notes");
        LowDiff = GameObject.Find("buttonLowDiff");
        MidDiff = GameObject.Find("buttonMidDiff");
        HighDiff = GameObject.Find("buttonHighDiff");
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
                break;

            case Phase.song:
                SongInfo nowSong = this.Songs[Cursol];
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
                NowPhase = Phase.folder;
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

#endregion
}