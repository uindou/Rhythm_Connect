using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クラスを跨いで使う定数、関数
/// 追加の際はstatic指定を忘れずに
/// </summary>
static class myConstants
{
    //1小節の解像度
    public static readonly int BarResolution = 9600;

    //読み込める最大の小節数
    //一般的なゲーム尺の曲は80前後で落ち着くと思われる
    public static readonly int MaxBarNum = 300;

    //各チャンネル分類用定数
    public static readonly int Channel_LaneA = 0;
    public static readonly int Channel_LaneB = 1;
    public static readonly int Channel_LaneC = 2;
    public static readonly int Channel_LaneD = 3;
    public static readonly int Channel_LaneAB = 4;
    public static readonly int Channel_LaneBC = 5;
    public static readonly int Channel_LaneCD = 6;
    public static readonly int Channel_LaneABC = 7;
    public static readonly int Channel_LaneBCD = 8;
    public static readonly int Channel_LaneABCD = 9;
    /// <summary>
    /// BPM変更データ用チャンネル
    /// </summary>
    public static readonly int Channel_ChangeBpm = 10;
    /// <summary>
    /// 小節倍率変更データ用チャンネル
    /// </summary>
    public static readonly int Channel_ChangeBarRate = 11;
    public static readonly int LaneNum = 10;

    //ノートの種類分類用定数
    public static readonly int SingleNote = 1;
    public static readonly int LongNoteStart = 2;
    public static readonly int LongNoteEnd = 3;

    //ゲージモード定数
    public static readonly int Easy = 0;
    public static readonly int Normal = 1;
    public static readonly int Hard = 2;
    public static readonly int ExHard = 3;

    //難易度に関する各種定数（名前、種類数、インデックス用定数等）
    public static readonly string[] DiffName = new string[] { "Simple", "Decent", "Complex" };
    public static readonly int LowDiff = 0;
    public static readonly int MidDiff = 1;
    public static readonly int HighDiff = 2;
    public static readonly int DiffKindNum = 3;
    public static readonly Color[] DiffColor =new Color[] {Color.green, Color.yellow, Color.red};

    //スコアランク
    public static readonly int BorderFullScore = 1000000;
    public static readonly int BorderSSS = 980000;
    public static readonly int BorderSS = 950000;
    public static readonly int BorderS = 900000;
    public static readonly int BorderA = 850000;
    public static readonly int BorderB = 800000;
    public static readonly int BorderC = 700000;

    //設定ファイルなどのファイル名/フォルダパス
    public static readonly string GameConfigFilePath = @"config\gameconfig.rccfg";
    public static readonly string SystemConfigFilePath = @"config\systemconfig.rccfg";
    public static readonly string SongDataFolderPath = "songdata";
    public static readonly string SongInfoFolderPath = "songinfo";
    public static readonly int Rc = 0;
    public static readonly int User = 1;
    public static readonly string[] ModeString = new string[] { "rc", "user" };
    

    //以下コンビニ関数

    /// <summary>
    /// 引数で受け取ったテキストファイルの内容を一行ずつListに読み込む。
    /// </summary>
    /// <param name="filename"></param>
    /// <returns>テキストファイルの中身（ファイルがなければnull）</returns>
    public static List<string> LoadFileToList(string filename)
    {
        List<string> Lines = new List<string>();

        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                while(sr.EndOfStream == false)
                {
                    Lines.Add(sr.ReadLine());
                }
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("file not found.");
            Debug.Log(e.Message);

            return null;
        }

        return Lines;
    }

    /// <summary>
    /// 指定したフォルダのサブフォルダ一覧をListで返す。
    /// </summary>
    /// <param name="folderpath"></param>
    /// <returns>サブフォルダ一覧（フォルダがなければnull）</returns>
    public static List<string> LoadSubFolderToList(string folderpath)
    {
        List<string> Folders = new List<string>();

        try
        {
            string[] temp = Directory.GetDirectories(folderpath);
            foreach (string fn in temp)
            {
                Folders.Add(fn);
            }
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("Directory not found.");
            Debug.Log(e.Message);

            return null;
        }

        return Folders;
    }

    // TODO : これもうちょい拡張してその行がパラメータorデータ行になってるかを判断させたい
    /// <summary>
    /// パラメータ分解用のSplit
    /// source内で初めて登場したsplitterの位置でsourceを二分する
    /// </summary>
    /// <param name="source">対象文字列</param>
    /// <param name="splitter">区切り文字</param>
    /// <returns>Splitメソッドと同様</returns>
    public static string[] SplitParam(string source,char splitter)
    {
        string[] rtn = new string[2];

        rtn[0] = source.Substring(0, source.IndexOf(splitter));
        rtn[1] = source.Substring(source.IndexOf(splitter) + 1);

        return rtn;
    }
    
    public static string CalcRank(int score)
    {
        string rank;

        if      (score >= BorderFullScore)  rank = "PERFECT";
        else if (score >= BorderSSS)        rank = "SSS";
        else if (score >= BorderSS)         rank = "SS";
        else if (score >= BorderS)          rank = "S";
        else if (score >= BorderA)          rank = "A";
        else if (score >= BorderB)          rank = "B";
        else if (score >= BorderC)          rank = "C";
        else                                rank = "D";

        return rank;
    }
}