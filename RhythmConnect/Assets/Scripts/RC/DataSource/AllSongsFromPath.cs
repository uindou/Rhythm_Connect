using System;
using System.Collections.Generic;
using System.IO;
using RC.Model;
using RC.Util;
using RC.Interface;
using RC.Const;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Rendering;

namespace RC.DataSource
{
    class AllSongsFromPath : IAllSongsRepository
    {
        #region フィールド・コンストラクタ
        private string folderListPath;

        public AllSongsFromPath(string folderPath)
        {
            this.folderListPath = folderPath;
        }
        #endregion

        #region インターフェース実装
        public List<FolderModel> GetRCSongData()
        {
            List<string> folderPaths = RcFile.LoadSubFolderToList(folderListPath);
            List<FolderModel> folders = new List<FolderModel>();
            foreach (string folderPath in folderPaths)
            {
                folders.Add(LoadFolder(folderPath));
            }

            return folders;
        }

        public List<SongModel> GetUserSongData()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 内部メソッド
        /// <summary>
        /// 引数に受け取ったフォルダパスからフォルダを読み込む
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private FolderModel LoadFolder(string folderPath)
        {
            List<string> songPaths = RcFile.LoadSubFolderToList(folderPath + "\\Songs");
            List<SongModel> songs = new List<SongModel>();
            foreach (string songPath in songPaths)
            {
                songs.Add(LoadSong(songPath));
            }

            Dictionary<string, object> folderData = new Dictionary<string, object>();
            folderData["name"] = RcParam.TrimFolderPath(folderPath);
            folderData["description"] = "";
            folderData["songs"] = songs;

            return new FolderModel(folderData);
        }

        /// <summary>
        /// 引数に受け取ったフォルダパスから曲を読み込む
        /// </summary>
        /// <param name="songPath"></param>
        /// <returns></returns>
        private SongModel LoadSong(string songPath)
        {
            Dictionary<string, object> songData = ReadSongMetaParam(RcFile.LoadFileToList(songPath + "\\MetaData.rcdat"));
            songData["title"] = RcParam.TrimFolderPath(songPath);
            songData["sheets"] = LoadSheets(songPath + "\\sheet");

            return new SongModel(songData);
        }

        /// <summary>
        /// Metaファイルの中身をListとして受け取り、パラメータを読み込む。
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>成否（成功でTrue）</returns>
        private Dictionary<string, object> ReadSongMetaParam(List<string> lines)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            //resultの初期化
            result["genre"] = "";
            result["artist"] = "";
            result["dispbpm"] = "";
            result["arranger"] = new List<string>
            {
                "", "", ""
            };
            result["playlevel"] = new List<int>
            {
                0, 0, 0
            };
            result["notesnum"] = new List<int>
            {
                0, 0, 0
            };

            foreach (string line in lines)
            {
                string[] temp;

                if (line != "")
                {
                    temp = RcParam.SplitParam(line, ' ');
                }
                else
                {
                    //空行は無視
                    continue;
                }

                if (temp[0][0] != '#')
                {
                    //コマンドでない行は無視
                    continue;
                }

                //FIXME : Parseを使ってる部分は後でTryParseにしないと例外吐いたら実行止まる
                switch (temp[0])
                {
                    case "#GENRE":
                        result["genre"] = temp[1];
                        break;

                    case "#ARTIST":
                        result["artist"] = temp[1];
                        break;

                    case "#ARRANGER":
                        string[] arr = temp[1].Split(',');
                        List<string> arranger = new List<string>();
                        foreach (string name in arr)
                        {
                            arranger.Add(name);
                        }
                        result["arranger"] = arranger;
                        break;

                    case "#DISPBPM":
                        result["dispbpm"] = temp[1];
                        break;

                    case "#PLAYLEVEL":
                        string[] lvl = temp[1].Split(',');
                        List<int> playlevel = new List<int>();
                        foreach (string level in lvl)
                        {
                            playlevel.Add(Int32.Parse(level));
                        }
                        result["playlevel"] = playlevel;
                        break;

                    case "#NOTESNUM":
                        string[] nnum = temp[1].Split(',');
                        List<int> notesnum = new List<int>();
                        foreach (string num in nnum)
                        {
                            notesnum.Add(Int32.Parse(num));
                        }
                        result["notesnum"] = notesnum;
                        break;

                    //どのパラメータにも一致しない場合は読み飛ばす
                    default:
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 各難度の譜面ファイルを読み込む
        /// </summary>
        /// <param name="sheetPath"></param>
        /// <returns></returns>
        private List<SheetModel> LoadSheets(string sheetPath)
        {
            var result = new List<SheetModel>
            {
                  new SheetModel(ReadSheetData(RcFile.LoadFileToList(sheetPath + "\\Simple.rcsht")))
                , new SheetModel(ReadSheetData(RcFile.LoadFileToList(sheetPath + "\\Decent.rcsht")))
                , new SheetModel(ReadSheetData(RcFile.LoadFileToList(sheetPath + "\\Complex.rcsht")))
            };

            return result;
        }

        /// <summary>
        /// 譜面データを読み込む
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<List<NoteModel>> ReadSheetData(List<string> lines)
        {
            List<List<NoteModel>> result = new List<List<NoteModel>>();

            for (int i = 0; i < Enum.GetValues(typeof(Channels)).Length; i++)
            {
                result.Add(new List<NoteModel>());
            }

            List<BarModel> barData = ReadBarParam(lines);

            foreach(BarModel bar in barData)
            {
                result[(int)Channels.Bar].Add(bar);
            }

            foreach (string line in lines)
            {
                Dictionary<string, object> param = RcParam.ReadDataParam(line);
                if (param == null) continue;

                if ((Channels)param["channel"] == Channels.Bar) continue;

                if ((Channels)param["channel"] == Channels.ChangeBpm)
                {
                    continue;
                }

                int dataNum = param["data"].ToString().Length;

                BarModel nowBar = (BarModel)result[(int)Channels.Bar][(int)param["barNum"]];

                int tick = nowBar.Length / dataNum;

                for (int i = 0; i < dataNum; i++)
                {
                    var noteType = (NoteType)Int32.Parse(param["data"].ToString()[i].ToString());
                    if (noteType == NoteType.None) continue;
                    int noteCount = nowBar.Count + tick * i;
                    var note = new PlayNoteModel(noteCount, noteType);
                    result[(int)param["channel"]].Add(note);
                }
            }

            return result;
        }

        /// <summary>
        /// 譜面データから小節データだけを読み込む
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<BarModel> ReadBarParam(List<string> lines)
        {
            List<BarModel> result = new List<BarModel>();

            int endBar = 0;

            foreach(string line in lines)
            {
                Dictionary<string, object> param = RcParam.ReadDataParam(line);
                if (param == null) continue;

                if ((int)param["barNum"] > endBar)
                {
                    for(int i = endBar; i <= (int)param["barNum"]; i++)
                    {
                        result.Add(new BarModel() { Rate = 1.0 });
                    }
                    endBar = (int)param["barNum"];
                }

                if ((Channels)param["channel"] == Channels.Bar)
                {
                    double? rate = RcExtension.DoubleTryParse((string)param["data"]);
                    if(rate != null)
                    {
                        result[(int)param["barNum"]].Rate = (double)rate;
                    }
                }
            }

            int count = 0;
            for(int i = 0; i < result.Count; i++)
            {
                result[i].Count = count;
                result[i].Length = (int)(result[i].Rate * GameConst.BarResolution);
                count += result[i].Length;
            }

            return result;
        }
        #endregion
    }
}