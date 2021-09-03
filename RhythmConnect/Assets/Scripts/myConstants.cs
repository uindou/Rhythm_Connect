using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static class myConstants
{
    //1小節の解像度
    public static readonly int BarResolution = 9600;
    public static readonly int MaxBarNum = 1000;

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
    public static readonly int Channel_ChangeBpm = 10;
    public static readonly int Channel_ChangeBarRate = 11;
    public static readonly int LaneNum = 10;

    //ノートの種類分類用定数
    public static readonly int SingleNote = 1;
    public static readonly int LongNoteStart = 2;
    public static readonly int LongNoteEnd = 3;

    //ゲージモード対数値 拡張性を考えて念のため採番は間を空けている
    public static readonly int Easy = 10;
    public static readonly int Normal = 20;
    public static readonly int Hard = 30;
    public static readonly int ExHard = 40;

    //難易度に関する各種定数（名前、種類数、インデックス用定数等）
    public static readonly string LowDiffName = "Simple";
    public static readonly string MidDiffName = "Decent";
    public static readonly string HighDiffName = "Complex";
    public static readonly int LowDiff = 0;
    public static readonly int MidDiff = 1;
    public static readonly int HighDiff = 2;
    public static readonly int DiffKindNum = 3;

    //設定ファイルなどのファイル名/フォルダパス
    public static readonly string GameConfigFilePath = @"config\gameconfig.rccfg";
    public static readonly string SystemConfigFilePath = @"config\systemconfig.rccfg";
    public static readonly string SongDataFolderPath = "songdata";
    public static readonly string SongInfoFolderPath = "songinfo";
    

    //以下コンビニ関数

    //LoadFileToList
    //引数で受け取ったテキストファイルの内容を一行ずつListに読み込み、返す。
    //ファイルが見つからなかった場合はnullを返す。
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
            Debug.Log("file load failed.");
            Debug.Log(e.Message);

            return null;
        }

        return Lines;
    }

    //LoadSubFolderToList
    //指定したフォルダのサブフォルダ一覧をListで返す。
    //フォルダが見つからなかった場合はnullを返す。
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

    public static string[] SplitParam(string source,char splitter)
    {
        string[] rtn = new string[2];

        rtn[0] = source.Substring(0, source.IndexOf(splitter));
        rtn[1] = source.Substring(source.IndexOf(splitter) + 1);

        return rtn;
    }
}