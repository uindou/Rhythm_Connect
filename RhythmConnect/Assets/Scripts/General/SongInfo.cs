using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongInfo
{
    public string SongName { get; private set; }    //楽曲名
    public string Genre { get; private set; }       //ジャンル名
    public string Artist { get; private set; }      //作曲者名
    public string Arranger { get; private set; }    //譜面作成者
    public string DispBpm { get; private set; }     //プレビューで表示するBPM文字列（ソフラン含む）
    public int[] PlayLevel { get; private set; } = new int[myConstants.DiffKindNum];
    //プレイ難易度（難易度ごとに）
    public int[] NotesNum { get; private set; } = new int[myConstants.DiffKindNum];
    //譜面に含まれるノートの合計数（難易度ごとに）
    public int[] HiScore { get; private set; } = new int[myConstants.DiffKindNum];
    //譜面のハイスコア（難易度ごとに）
    public int[] MaxCombo { get; private set; } = new int[myConstants.DiffKindNum];
    //譜面で過去に出したことのある最大コンボ数（難易度ごとに）
    public int[] PlayCount { get; private set; } = new int[myConstants.DiffKindNum];
    //譜面をプレイした回数（難易度ごとに）

    public SongInfo()
    {
        //初期化
        SongName = "TestSong";
        Genre = "TestSong";
        Artist = "TestMan";
        Arranger = "TestFumenTukuriMan";
        DispBpm = "180";
        PlayLevel[myConstants.LowDiff] = 3;
        PlayLevel[myConstants.MidDiff] = 6;
        PlayLevel[myConstants.HighDiff]= 9;
        NotesNum[myConstants.LowDiff] = 300;
        NotesNum[myConstants.MidDiff] = 600;
        NotesNum[myConstants.HighDiff] = 900;
        HiScore[myConstants.LowDiff] = 1000000;
        HiScore[myConstants.MidDiff] = 1000000;
        HiScore[myConstants.HighDiff] = 1000000;
        MaxCombo[myConstants.LowDiff] = 300;
        MaxCombo[myConstants.MidDiff] = 600;
        MaxCombo[myConstants.HighDiff] = 900;
        PlayCount[myConstants.LowDiff] = 3;
        PlayCount[myConstants.MidDiff] = 6;
        PlayCount[myConstants.HighDiff] = 9;
    }

    //曲情報ファイルの読み込み 成否をBool値で返す（成功でTrue）
    public bool LoadSongInfo(string infodatapath)
    {
        string[] t = infodatapath.Split('\\');
        string[] u = t[t.Length-1].Split('.');
        SongName = u[1];

        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(infodatapath);
        if(Lines == null)
        {
            return false;
        }

        foreach (string line in Lines)
        {
            string[] temp = myConstants.SplitParam(line, ' ');
            string[] s;

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
                    s = temp[1].Split(',');
                    for(int i = 0;i < myConstants.DiffKindNum;i++)
                    {
                        PlayLevel[i] = int.Parse(s[i]);
                    }
                    break;

                case "#HISCORE":
                    s = temp[1].Split(',');
                    for(int i = 0;i < myConstants.DiffKindNum;i++)
                    {
                        HiScore[i] = int.Parse(s[i]);
                    }
                    break;

                case "#NOTESNUM":
                    s = temp[1].Split(',');
                    for(int i = 0;i < myConstants.DiffKindNum;i++)
                    {
                        NotesNum[i] = int.Parse(s[i]);
                    }
                    break;

                case "#MAXCOMBO":
                    s = temp[1].Split(',');
                    for(int i = 0;i < myConstants.DiffKindNum;i++)
                    {
                        MaxCombo[i] = int.Parse(s[i]);
                    }
                    break;
                
                case "#PLAYCOUNT":
                    s = temp[1].Split(',');
                    for(int i = 0;i < myConstants.DiffKindNum;i++)
                    {
                        PlayCount[i] = int.Parse(s[i]);
                    }
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
    public bool CreateSongInfoFile()
    {
        SheetData sd = new SheetData();



        return true;
    }
}