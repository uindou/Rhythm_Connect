using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongInfo
{
    public string SongName { get; private set; }    //楽曲名
    public string Mode { get; private set; }        //公式フォルダorユーザー定義フォルダ
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
        Mode = myConstants.Rc;
        Genre = "TestSong";
        Artist = "TestMan";
        Arranger = "TestFumenTukuriMan";
        DispBpm = "180";
        PlayLevel[myConstants.LowDiff] = 0;
        PlayLevel[myConstants.MidDiff] = 0;
        PlayLevel[myConstants.HighDiff]= 0;
        NotesNum[myConstants.LowDiff] = 0;
        NotesNum[myConstants.MidDiff] = 0;
        NotesNum[myConstants.HighDiff] = 0;
        HiScore[myConstants.LowDiff] = 0;
        HiScore[myConstants.MidDiff] = 0;
        HiScore[myConstants.HighDiff] = 0;
        MaxCombo[myConstants.LowDiff] = 0;
        MaxCombo[myConstants.MidDiff] = 0;
        MaxCombo[myConstants.HighDiff] = 0;
        PlayCount[myConstants.LowDiff] = 0;
        PlayCount[myConstants.MidDiff] = 0;
        PlayCount[myConstants.HighDiff] = 0;
    }

    //曲情報ファイルの読み込み 成否をBool値で返す（成功でTrue）
    public bool LoadSongInfo(string songname, int mode)
    {
        SongName = songname;
        Mode = mode;

        try 
        {
            List<string> Lines = new List<string>();
            Lines = myConstants.LoadFileToList(myConstants.SongInfoFolderPath + '\\' + myConstants.ModeString[Mode] + '\\' + SongName + ".rcdat");
            if(Lines == null)
            {
                return false;
            }

            AnalyseInfoParam(Lines);
        }
        catch(FileNotFoundException e)
        {
            CreateSongInfoFile();
        }
        
        return true;
    }

    private bool AnalyseInfoParam(List<string> lines)
    {
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
    }

    //曲情報ファイルを新規に作成する
    //関数内でSheetDataを生成して譜面データを読み込ませ、そのデータを元に曲情報を得る
    //成否をBool値で返す（成功でTrue）
    public bool CreateSongInfoFile()
    {
        SheetData sd = new SheetData();

        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(myConstants.SongDataFolderPath + '\\' + myConstants.ModeString[Mode] + '\\' + SongName + '\\' + "Info.rcdat");
        //songdata\mode\songname\Info.rcdat

        if(Lines == null)
        {
            return false;
        }

        AnalyseInfoParam(Lines);

        for(int i = 0; i < myConstants.DiffKindNum; i++)
        {
            sd.LoadSheetData(SongName, Mode, i);
            NotesNum[i] = sd.NotesNum;
        }

        return true;
    }
}