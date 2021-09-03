using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongList
{
    public List<SongInfo> Songs { get; private set; } = new List<SongInfo>();

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
