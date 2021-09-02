using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongInfo
{
    public string SongName { get; private set; }    //楽曲名
    public string Genre { get; private set; }       //ジャンル名
    public string Artist { get; private set; }
    public string Arranger { get; private set; }
    public string DispBpm { get; private set; }
    public int PlayLevel { get; private set; }
    public int NotesNum { get; private set; }       //この譜面に含まれるノートの合計数
    public int HiScore { get; private set; }        //この譜面のハイスコア
    public int MaxCombo { get; private set; }       //この譜面で過去に出したことのある最大コンボ数
    public int PlayCount { get; private set; }
    
    //曲情報ファイルの読み込み 成否をBool値で返す（成功でTrue）
    public bool LoadSongInfo(string infodatapath)
    {
        string[] t = infodatapath.Split(@"\");
        string[] u = t[t.Length-1].Split(".");
        SongName = u[1];

        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(infodatapath);
        if(Lines == null)
        {
            return false;
        }

        foreach (string line in Lines)
        {
            string[] temp = myConstants.SplitParam(line, " ");

            switch (temp[0])
            {
                case "#GENRE":
                    Genre = temp[1];
                    break;
                
                case "#ARTIST":
                    Artist = temp[1];
                    break;
                
                case "#ARRANGER":
                    Arranger = temp[1];
                    break;

                case "#DISPBPM":
                    DispBpm = temp[1];
                    break;

                case "#PLAYLEVEL":
                    PlayLevel = int.Parse(temp[1]);
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
                
                case "#PLAYCOUNT":
                    PlayCount = int.Parse(temp[1]);
                    break;

                default:
                    break;
            }
        }
        
        return true;
    }

    //曲情報ファイルを新規に作成する
    //関数内でSheetDataを生成して譜面データを読み込ませ、そのデータを元に曲情報を得る
    //成否をBool値で返す（成功でTrue）
    public bool CreateSongInfoFile(string sheetdatapath)
    {
        return true;
    }
}