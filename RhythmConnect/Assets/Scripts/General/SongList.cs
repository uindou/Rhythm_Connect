using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全曲情報を保持するクラス
/// </summary>
public class SongList
{
    public List<SongInfo> Songs { get; private set; } = new List<SongInfo>();
    //曲情報のリスト

    /// <summary>
    /// フォルダの曲情報を全て読み込む
    /// </summary>
    /// <returns>成否（成功でTrue）</returns>
    public bool LoadSongList()
    {
        List<string> songnames = new List<string>();
        
        for(int mode = 0; mode < 2; mode++)
        {
            songnames = myConstants.LoadSubFolderToList(myConstants.SongDataFolderPath + '\\' + myConstants.ModeString[mode]);
            if(songnames == null)
            {
                continue;
            }

            foreach(string songname in songnames)
            {
                SongInfo temp = new SongInfo();
                string str;
                str = songname.Split('\\')[songname.Split('\\').Length - 1];
                Debug.Log("LoadSongInfo from " + str);
                if(temp.LoadSongInfo(str, mode) == false)
                {
                    continue;
                }
                Songs.Add(temp);
            }
        }

        return true;
    }
}
