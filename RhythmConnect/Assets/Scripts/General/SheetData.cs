using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetData
{
    public string SongDataFolderPath { get; private set; }
    public int NotesNum { get; private set; } = 0;   //譜面に含まれるノーツ数
    public string Genre { get; private set; }   //譜面に含まれる楽曲のジャンル
    public string Artist { get; private set; }
    public string Arranger { get; private set; }
    public double Bpm { get; private set; }
    public int PlayLevel { get; private set; }
    public double Total { get; private set; }

    public SheetData()
    {
        SongDataFolderPath = "songdata";
    }

    public SheetData(string songdatafolderpath)
    {
        SongDataFolderPath = songdatafolderpath;
        NotesNum = 0;
    }

    public bool LoadSheetData(string songname)
    {
        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(SongDataFolderPath + @"\" + songname + @"\" + songname + ".rcsht");

        foreach (string line in Lines)
        {
            string[] temp = line.Split(' ');

            switch (temp[0])
            {
                case "#GENRE":
                    Genre = temp[1];
                    break;
                
                case "#ARTIST":
                    break;

                default:
                    if(temp[0][0]=='#')
                    {
                        break;
                    }
                    break;
            }
        }

        return true;
    }
}