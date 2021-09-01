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
    public static readonly string GameConfigFilePath = "gameconfig.rccfg";
    public static readonly string SystemConfigFilePath = "systemconfig.rccfg";
    public static readonly string SongInfoFolderPath = "songinfo";
    public static readonly string DefaultSongDataFolderPath = "songdata";

    //以下コンビニ関数
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

    public static List<string> LoadSubFolderToList(string folderpath)
    {
        List<string> Folders = new List<string>();

        try
        {
            string[] temp = Directory.GetDirectories(foldername);
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

}