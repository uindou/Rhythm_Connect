using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static class myConstants
{
    //ゲージモード対数値 拡張性を考えて念のため採番は間を空けている
    public static readonly int Easy = 10;
    public static readonly int Normal = 20;
    public static readonly int Hard = 30;
    public static readonly int ExHard = 40;

    //設定ファイルなどのファイル名/フォルダパス
    public static readonly string GameConfigFilePath = @"config\gameconfig.rccfg";
    public static readonly string SystemConfigFilePath = @"config\systemconfig.rccfg";
    public static readonly string RcSongDataFolderPath = @"songdata\rc";
    public static readonly string UserSongDataFolderpath = @"songdata\user";
    public static readonly string RcSongInfoFolderPath = @"songinfo\rc";
    public static readonly string UserSongInfoFolderPath = @"songinfo\user";



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

    public static List<string> SplitParam(string source,string splitter)
    {
        List<string> rtn = new List<string>();

        rtn.Add(source.Substring(0, source.IndexOf(splitter)));
        rtn.Add(source.Substring(IndexOf(splitter + 1)));

        return rtn;
    }

}