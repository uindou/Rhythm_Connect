using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongInfo : GeneralFileReader
{
    public string SongName { get; private set; }
    public string Genre { get; private set; }
    public int HiScore { get; private set; }
    public int NotesNum { get; private set; }
    public int MaxCombo { get; private set; }

    public bool LoadSongInfo(string songname)
    {
        SongName = songname;

        if(LoadFile(@"songinfo\" + songname + @"\" + songname + ".rcdat") == false)
        {
            CreateSongInfoFile(songname);
        }

        foreach (string line in Line)
        {
            string[] temp = line.Split(' ');

            switch (temp[0])
            {
                case "#GENRE":
                    Genre = temp[1];
                    break;

                case "#HISCORE":
                    HiScore = int.Parse(temp[1]);
                    break;

                case "#NOTESNUM":
                    NotesNum = int.Parse(temp[1]);
                    break;

                case "#MAXCOMBO":
                    MaxCombo = int.Parse(temp[1]);
                    break;

                default:
                    break;
            }
        }
        
        return true;
    }

    private bool CreateSongInfoFile(string songname)
    {
        return true;
    }
}