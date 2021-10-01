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
    public bool LoadSongList()
    {
        List<string> songnames = new List<string>();
        
        for(int mode = 0;mode < 2;mode++)
        {
            songnames = myConstants.LoadSubFolderToList(myConstants.SongDataFolderPath + '\\' + myConstants.ModeString[mode]);
            if(songnames == null)
            {
                return false;
            }

            foreach(string songname in songnames)
            {
                SongInfo temp = new SongInfo();
                string str;
                str = songname.Split('\\')[songname.Split('\\').Length - 1];
                Debug.Log("LoadSongInfo from " + str);
                temp.LoadSongInfo(str, mode);
                Songs.Add(temp);
            }
        }

        return true;
    }
}
