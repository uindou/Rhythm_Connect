using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections.Generic;
using RC.Model;
using RC.DataSource;
using TMPro;

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

    enum Difficulty
    {
        Simple
        , Decent
        , Complex
    }

    public class FreePlay : MonoBehaviour
    {
        //持ってるデータ
        private FolderListModel folderList;

        //表示制御用
        private Phase nowPhase = Phase.folder;
        private Difficulty nowDifficulty = Difficulty.Simple;
        private int _Cursol;
        public int Cursol
        {
            get { return _Cursol; }
            set
            {
                //カーソルが向いているボタンの色を変えるための処理。
                if (buttons.Count > _Cursol)
                {
                    buttons[_Cursol].GetComponent<Button>().image.color = ButtonDefault;
                }

                if (buttons.Count > value)
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

        //GameObject
        //既にあるオブジェクト
        private GameObject ButtonCancel;
        private GameObject Content;
        private GameObject ButtonPrefab;
        private GameObject FolderDesc;
        private GameObject SongDesc;
        //動的に生成するオブジェクト
        private List<GameObject> buttons = new List<GameObject>();

        //Buttonの色
        //TODO: ここは後で変更する
        private Color ButtonFocus = Color.cyan;
        private Color ButtonDefault = Color.white;

        private void Awake()
        {
            //データのロード
            folderList = new FolderListModel(new FolderListFromPath("FolderList"));
            //オブジェクトの取得
            ButtonCancel = GameObject.Find("ButtonCancel");
            Content = GameObject.Find("Content");
            FolderDesc = GameObject.Find("FolderDesc");
            SongDesc = GameObject.Find("SongDesc");

            //ボタンのテンプレートをロード
            ButtonPrefab = (GameObject)Resources.Load("Prefabs/Button");
        }

        private void Start()
        {
            Cursol = 0;
        }
    }
}
