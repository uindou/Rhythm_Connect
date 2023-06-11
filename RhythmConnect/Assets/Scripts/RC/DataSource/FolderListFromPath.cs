using System;
using System.Collections.Generic;
using System.IO;
using RC.Model;
using RC.Util;
using RC.Interface;

namespace RC.DataSource
{
    class FolderListFromPath : IFolderListRepository
    {
        private string folderListPath;

        public FolderListFromPath(string folderPath)
        {
            this.folderListPath = folderPath;
        }

        public List<FolderModel> LoadData()
        {
            List<string> folderPaths = RcFile.LoadSubFolderToList(folderListPath);
            List<FolderModel> folders = new List<FolderModel>();
            foreach (string folderPath in folderPaths)
            {
                folders.Add(LoadFolder(folderPath));
            }

            return folders;
        }

        private FolderModel LoadFolder(string folderPath)
        {
            List<string> songPaths = RcFile.LoadSubFolderToList(folderPath + "/Songs");
            List<SongModel> songs = new List<SongModel>();
            foreach (string songPath in songPaths)
            {
                songs.Add(LoadSong(songPath));
            }

            return new FolderModel(RcParam.TrimFolderPath(folderPath)
                                    , ""
                                    , songs);
        }

        private SongModel LoadSong(string songPath)
        {
            Dictionary<string, object> songMeta = ReadSongMetaParam(RcFile.LoadFileToList(songPath + "/MetaData.rcdat"));

            return new SongModel(   RcParam.TrimFolderPath(songPath)
                                ,   songMeta["genre"].ToString()
                                ,   songMeta["artist"].ToString()
                                ,   songMeta["dispbpm"].ToString()
                                ,   songMeta["arranger"] as string[]
                                ,   songMeta["playlevel"] as int[]
                                ,   songMeta["notesnum"] as int[]);
        }

        /// <summary>
        /// SongInfoファイルの中身をListとして受け取り、パラメータを読み込む。
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>成否（成功でTrue）</returns>
        private Dictionary<string, object> ReadSongMetaParam(List<string> lines)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

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
                        List<string> playlevel = new List<string>();
                        foreach (string level in lvl)
                        {
                            playlevel.Add(level);
                        }
                        result["playlevel"] = playlevel;
                        break;

                    case "#NOTESNUM":
                        string[] nnum = temp[1].Split(',');
                        List<string> notesnum = new List<string>();
                        foreach (string num in nnum)
                        {
                            notesnum.Add(num);
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

    }
}