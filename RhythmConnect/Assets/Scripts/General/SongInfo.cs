using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongInfo
{
    public string SongName { get; private set; }    //楽曲名
    public string Genre { get; private set; }       //ジャンル名
    public int NotesNum { get; private set; }       //この譜面に含まれるノートの合計数
    public int HiScore { get; private set; }        //この譜面のハイスコア
    public int MaxCombo { get; private set; }       //この譜面で過去に出したことのある最大コンボ数
    
    //曲情報ファイルの読み込み 成否をBool値で返す（成功でTrue）
    public bool LoadSongInfo(string songname)
    {
        SongName = songname;
        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(myConstants.SongInfoFolderPath + @"\" + songname + ".rcdat");
        if(Lines == null)
        {
            CreateSongInfoFile(songname);
        }

        foreach (string line in Lines)
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

    //曲情報ファイルを新規に作成する 曲情報ファイルが読み込めなかった際に呼び出される
    //成否をBool値で返す（成功でTrue）
    private bool CreateSongInfoFile(string songname)
    {
        return true;
    }
}