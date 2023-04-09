using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 楽曲情報
/// </summary>
public class SongInfo
{
    public string SongName { get; private set; }    //楽曲名
    public int Mode { get; private set; }        //公式フォルダorユーザー定義フォルダ
    public string Genre { get; private set; }       //ジャンル名
    public string Artist { get; private set; }      //作曲者名
    public string DispBpm { get; private set; }     //プレビューで表示するBPM文字列（ソフラン含む）
    public string[] Arranger { get; private set; } = new string[myConstants.DiffKindNum];
    //譜面作成者(難易度ごとに)
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

    /// <summary>
    /// 曲情報ファイルの読み込み 成否をBool値で返す（成功でTrue）
    /// </summary>
    /// <param name="songname"></param>
    /// <param name="mode"></param>
    /// <returns>成否（成功でTrue）</returns>
    public bool LoadSongInfo(string songname, int mode)
    {
        SongName = songname;
        Mode = mode;

        Debug.Log("Loading " + SongName);

        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(myConstants.SongInfoFolderPath + '\\' + myConstants.ModeString[Mode] + '\\' + SongName + ".rcdat");
        //ex. songinfo\rc\songname.rcdat
        if(Lines == null)
        {
            //対象のファイルがなかった場合（初回読込の場合）、新たに生成する。
            CreateSongInfoFile();
        }
        else
        {
            ReadInfoParam(Lines);
        }
        
        return true;
    }

    /// <summary>
    /// SongInfoファイルの中身をListとして受け取り、パラメータを読み込む。
    /// </summary>
    /// <param name="lines"></param>
    /// <returns>成否（成功でTrue）</returns>
    private bool ReadInfoParam(List<string> lines)
    {
        foreach (string line in lines)
        {
            string[] temp;

            if(line != "")
            {
                temp = myConstants.SplitParam(line, ' ');
            }
            else
            {
                continue;
            }

            if(temp[0][0] != '#')
            {
                continue;
            }
            
            string[] s;

            //FIXME : Parseを使ってる部分は後でTryParseにしないと例外吐いたら実行止まる
            switch (temp[0])
            {
                case "#GENRE":
                    Genre = temp[1];
                    break;
                    
                case "#ARTIST":
                    Artist = temp[1];
                    break;
                    
                case "#ARRANGER":
                    s = temp[1].Split(',');
                    for(int i=0; i < myConstants.DiffKindNum; i++)
                    {
                        Arranger[i] = s[i];
                    }
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

                //どのパラメータにも一致しない場合は読み飛ばす
                default:
                    break;
            }
        }
        Debug.Log(SongName + " " + Artist);
        return true;
    }

    /// <summary>
    /// 曲情報ファイルを新規に作成し、読み込む
    /// </summary>
    /// <returns>成否（成功でTrue）</returns>
    public bool CreateSongInfoFile()
    {
        Debug.Log("Start CreateSongInfoFile " + SongName);

        SheetData sd = new SheetData();
        
        List<string> Lines = new List<string>();
        
        //楽曲パッケージは譜面データ以外に作曲者情報などを記載したInfoファイルを持つ
        Lines = myConstants.LoadFileToList(myConstants.SongDataFolderPath + '\\' + myConstants.ModeString[Mode] + '\\' + SongName + '\\' + "Info.rcdat");
        //ex. songdata\mode\songname\Info.rcdat

        if(Lines == null)
        {
            return false;
        }

        ReadInfoParam(Lines);

        //ノーツ数を得るために譜面データも読み込む
        for(int i = 0; i < myConstants.DiffKindNum; i++)
        {
            sd.LoadSheetData(SongName, Mode, i);
            NotesNum[i] = sd.NotesNum;
        }

        return true;
    }
}