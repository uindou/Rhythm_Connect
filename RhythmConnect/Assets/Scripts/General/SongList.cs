using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全曲情報を保持するクラス
public class SongList
{
    public List<SongInfo> Songs { get; private set; } = new List<SongInfo>();
    //曲情報のリスト

    //LoadSongList
    //引数で受け取ったモード（ユーザ定義譜面フォルダor公式譜面フォルダ）に応じて、該当するフォルダの曲情報を全て読み込む
    //myConstantsにまあまあ依存している。
    //成功でTrueを返す
    public bool LoadSongList(string mode)
    {
        List<string> songnames = new List<string>();

        songnames = myConstants.LoadSubFolderToList(myConstants.SongDataFolderPath + '\\' + mode);

        foreach(string songname in songnames)
        {
            SongInfo temp = new SongInfo();
            if(temp.LoadSongInfo(myConstants.SongInfoFolderPath + '\\' + mode + '\\' + songname) == false)
            {
                continue;
            }
            Songs.Add(temp);
        }

        return true;
    }
}
