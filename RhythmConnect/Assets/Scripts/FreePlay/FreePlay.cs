using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using RC.Model;
using RC.DataSource;
using RC.Util;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.Analytics;
using UnityEngine.WSA;
using RC.Const;

namespace RC.Scene.FreePlay
{
    /// <summary>
    /// 現在フォルダと曲のどちらを表示しているのかを表すための列挙体
    /// </summary>
    enum Phase
    {
        folder
        , song
    }

    public class FreePlay : MonoBehaviour
    {
        #region フィールド・プロパティ
        //持ってるデータ
        private AllSongsModel allSongs;
        private FolderModel nowFolder;      //現在表示しているフォルダ

        //表示制御用
        private Difficulty _nowDifficulty;
        public Difficulty NowDifficulty
        {
            get { return _nowDifficulty; }
            set
            {
                _nowDifficulty = value;
                UpdateView();
            }
        }

        private Phase _nowPhase;
        private Phase NowPhase
        {
            get { return _nowPhase; }
            set
            {
                _nowPhase = value;
                switch (_nowPhase)
                {
                    case Phase.folder:
                        SetViewFolder();
                        break;
                    case Phase.song:
                        nowFolder = allSongs.RcFolders[Cursor];
                        SetViewSong();
                        break;
                }
            }
        }

        private int _cursor;
        public int Cursor
        {
            get { return _cursor; }
            set
            {
                //カーソルが向いているボタンの色を変えるための処理。
                if (buttons.Count > _cursor)
                {
                    buttons[_cursor].GetComponent<Button>().image.color = ButtonDefault;
                }

                if (buttons.Count > value)
                {
                    _cursor = value;
                }
                else
                {
                    _cursor = 0;
                }

                buttons[_cursor].GetComponent<Button>().image.color = ButtonFocus;
                UpdateView();
            }
        }

        //GameObject
        //既にあるオブジェクト
        //private GameObject ButtonCancel;
        private GameObject Content;
        private GameObject ButtonPrefab;
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

        //動的に生成するオブジェクト
        private List<GameObject> buttons = new List<GameObject>();

        //Buttonの色
        //TODO: ここは後で変更する
        private Color ButtonFocus = Color.cyan;
        private Color ButtonDefault = Color.white;

        #endregion

        private void Awake()
        {
            //データのロード
            allSongs = new AllSongsModel(new AllSongsFromPath("FolderList"));
            allSongs.LoadRCSongData();

            //オブジェクトの取得
            //ButtonCancel = GameObject.Find("ButtonCancel"); //非活性にするとFindで見つからなくなるのでオブジェクトをつかんでおく。
            Content = GameObject.Find("Content");   //ボタンの生成先
            FolderDesc = GameObject.Find("Folder Description");
            SongDesc = GameObject.Find("Song Description");

            //ボタンのテンプレートをロード
            ButtonPrefab = (GameObject)Resources.Load("Prefabs/Button");

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
            NowPhase = Phase.folder;
        }

        private void SetViewFolder()
        {
            FolderDesc.SetActive(true);
            SongDesc.SetActive(false);
            ClearButtons();

            List<FolderModel> Folders = allSongs.RcFolders;
            for (int i = 0; i < Folders.Count; i++)
            {
                // ボタンにインデックス情報を付加したいのでforeachは使わない
                buttons.Add(CreateButton(Folders[i].Name, i));
            }

            Cursor = 0;
        }

        private void SetViewSong()
        {
            FolderDesc.SetActive(false);
            SongDesc.SetActive(true);
            ClearButtons();

            List<SongModel> Songs = allSongs.RcFolders[Cursor].Songs;
            for (int i = 0; i < Songs.Count; i++)
            {
                // ボタンにインデックス情報を付加したいのでforeachは使わない
                buttons.Add(CreateButton(Songs[i].Title, i));
            }

            Cursor = 0;
        }

        private void UpdateView()
        {
            switch (NowPhase)
            {
                case Phase.folder:
                    FolderModel nowFolder = allSongs.RcFolders[Cursor];
                    FolderTitle.text = nowFolder.Name;
                    break;

                case Phase.song:
                    SongModel nowSong = this.nowFolder.Songs[Cursor];

                    SongTitle.text = "Title:" + nowSong.Title;
                    Artist.text = "Artist:" + nowSong.Artist;
                    Genre.text = "Genre:" + nowSong.Genre;
                    Arranger.text = "Arranger:" + nowSong.Arranger[(int)NowDifficulty];
                    HighScore.text = "High Score:";
                    MaxCombo.text = "Max Combo:";
                    PlayCount.text = "Play Count:";
                    ClearRank.text = "Clear Rank:";
                    DispBpm.text = "BPM:" + nowSong.DispBpm;
                    NotesNum.text = "Notes:" + nowSong.Sheets[(int)NowDifficulty].NotesNum.ToString();
                    LevelLowDiff.text = nowSong.PlayLevel[(int)Difficulty.Simple].ToString();
                    LevelMidDiff.text = nowSong.PlayLevel[(int)Difficulty.Decent].ToString();
                    LevelHighDiff.text = nowSong.PlayLevel[(int)Difficulty.Complex].ToString();
                    //TODO: ここは後で変更する
                    Texture2D tex = RcImage.ReadByBinary(RcImage.ReadPngFile($"FolderList\\{this.nowFolder.Name}\\Songs\\{nowSong.Title}\\Jacket.jpg"));
                    SongJacket.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                    break;
            }
        }

        /// <summary>
        /// スクロールビュー上のボタンの全削除
        /// ボタン自体の破棄に加えて、ボタン管理用の配列もクリアする。
        /// </summary>
        private void ClearButtons()
        {
            foreach (GameObject _button in buttons)
            {
                if (_button != null)
                {
                    Destroy(_button);
                }
            }

            buttons.Clear();
        }

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
        /// 生成したボタンにアタッチするOnClick処理
        /// </summary>
        /// <param name="index"></param>
        public void ButtonClickEvent(int index)
        {
            GameObject.Find("Canvas").GetComponentInChildren<FreePlay>().Cursor = index;
        }

        public void ClickDecide()
        {
            switch (NowPhase)
            {
                //フォルダと曲のどちらを選択したかで分岐
                case Phase.folder:
                    NowPhase = Phase.song;
                    break;
                case Phase.song:
                    //曲決定処理
                    break;
            }
        }
        
        public void ClickCancel()
        {
            switch (NowPhase)
            {
                case Phase.folder:
                    //フォルダ選択をキャンセル
                    break;
                case Phase.song:
                    NowPhase = Phase.folder;
                    break;
            }
        }
    }
}
