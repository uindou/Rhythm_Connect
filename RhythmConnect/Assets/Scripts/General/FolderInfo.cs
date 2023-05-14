using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 曲を含むフォルダ情報を保持するクラス
/// </summary>
public class FolderInfo
{
    // TODO : 曲リストのソートはこのクラスに任せたいね
    public string name { get; private set; }
    public string path { get; private set; }
    public bool isLoaded { get; private set; }
    public List<SongInfo> Songs { get; private set; } = new List<SongInfo>();   //曲情報のリスト
    public FolderInfo(string folderpath)
    {
        this.name = TrimFolderPath(folderpath);
        this.path = folderpath;
    }

    /// <summary>
    /// フォルダの曲情報を全て読み込む
    /// </summary>
    /// <returns>成否（成功でTrue）</returns>
    public bool LoadSongList()
    {
        // 既に読込済みならスキップ
        if(this.isLoaded)
        {
            return true;
        }

        List<string> songpaths = new List<string>();
        
        songpaths = myConstants.LoadSubFolderToList(this.path);
        if(songpaths == null)
        {
            return false;
        }

        List<SongInfo> list = new List<SongInfo>();
        foreach(string songpath in songpaths)
        {
            SongInfo temp = new SongInfo();
            if(temp.LoadSongInfo(songpath) == false)
            {
                continue;
            }
            list.Add(temp);
        }
        this.Songs = list;
        this.isLoaded = true;

        return true;
    }

    /// <summary>
    /// 引数に受け取ったフォルダパスから末尾のフォルダ名だけを切り出す。
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string TrimFolderPath(string path)
    {
        string[] foldernames = path.Split('\\');
        string endfoldername = foldernames[foldernames.Length - 1];

        return endfoldername;
    }
}
