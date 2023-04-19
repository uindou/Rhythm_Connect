using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 曲を含むフォルダ情報を保持するクラス
/// </summary>
public class FolderInfo
{
    // TODO : 曲リストのソートはこのクラスに任せたいね
    public List<SongInfo> Songs { get; private set; } = new List<SongInfo>();
    //曲情報のリスト

    /// <summary>
    /// フォルダの曲情報を全て読み込む
    /// </summary>
    /// <returns>成否（成功でTrue）</returns>
    public bool LoadSongList(string folderpath)
    {
        List<string> songnames = new List<string>();
        
        songnames = myConstants.LoadSubFolderToList(folderpath);
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
            if(temp.LoadSongInfo(Path) == false)
            {
                continue;
            }
            Songs.Add(temp);
        }

        return true;
    }
}
