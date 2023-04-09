using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 譜面データ
/// </summary>
public class SheetData
{
    public List<NoteData>[] Notes { get; private set; } = new List<NoteData>[myConstants.LaneNum];
    //全ノーツを保持するリストの配列（レーン数分）
    public BarData[] Bars { get; private set; } = new BarData[myConstants.MaxBarNum];
    //全小節線情報を保持するリストの配列（許可された最大小節数分）
    public List<BpmData> BpmList { get; private set; } = new List<BpmData>();
    //全BPM変化情報を保持するリスト
    public int EndBar { get; private set; } = 0;        //譜面中最後の小節
    public int EndCount { get; private set; } = 0;       //譜面中最後の小節が流れるタイミング
    public int NotesNum { get; private set; } = 0;      //譜面に含まれるノーツ数
    public double Total { get; private set; }           //この譜面の全ノーツを最高判定で叩いた時に溜まるゲージ量（Normal想定）

    public SheetData()
    {
        //クラスの配列のインスタンス生成
        for(int i = 0; i < myConstants.LaneNum; i++)
        {
            Notes[i] = new List<NoteData>();
        }

        for(int i = 0; i < myConstants.MaxBarNum; i++)
        {
            Bars[i] = new BarData();
        }
    }

    /// <summary>
    /// 引数に受け取ったファイルから譜面データを読み込む
    /// </summary>
    /// <param name="songname"></param>
    /// <param name="mode">公式orユーザー定義譜面</param>
    /// <param name="diff">難易度</param>
    /// <returns>成否（成功でTrue）</returns>
    public bool LoadSheetData(string songname, int mode, int diff)
    {
        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(myConstants.SongDataFolderPath + '\\' + myConstants.ModeString[mode] + '\\' + songname + "\\sheet\\" + myConstants.DiffName[diff] + ".rcsht");
        if(Lines == null)
        {
            return false;
        }

        LoadHeaderData(Lines);
        LoadNotesData(Lines);

        return true;
    }

    /// <summary>
    /// 実際の譜面に関係ないヘッダデータ（作曲者名など）と、小節線情報のみを読み込む
    /// ノーツ情報の読込の際に小節線情報だけ先に分かっていた方が都合がいい
    /// </summary>
    /// <param name="Lines"></param>
    /// <returns>成否（成功でTrue）</returns>
    private bool LoadHeaderData(List<string> Lines)
    {
        foreach (string line in Lines)
        {
            string[] temp;

            if(line != "")
            {
                temp = myConstants.SplitParam(line, ' ');
            }
            else
            {
                //空白行は読み飛ばす
                continue;
            }

            if(temp[0][0] != '#')
            {
                //#以外で始まる行は読み飛ばす
                continue;
            }

            switch (temp[0])
            {
                case "#STARTBPM":
                    BpmData startbpm = new BpmData(double.Parse(temp[1]), 0);
                    BpmList.Add(startbpm);
                    break;

                case "#TOTAL":
                    Total = double.Parse(temp[1]);
                    break;

                case "#OFFSET":
                    break;

                //上の3つ以外であればデータ行と判断
                default:
                    int barnum = int.Parse(temp[0].Substring(1, 3));
                    int channel = int.Parse(temp[0].Substring(4, 2));

                    //小節倍率変更データの場合
                    if(channel == myConstants.Channel_ChangeBarRate)
                    {
                        Bars[barnum].Rate = double.Parse(temp[1]);
                        break;
                    }

                    //小節の数をカウントする
                    if(EndBar < barnum)
                    {
                        EndBar = barnum;
                    }
                    
                    break;
            }
        }

        //余韻として最後に+1小節する
        EndBar++;

        int cnt = 0;

        //各小節線の降ってくるタイミングを計算する
        for(int i = 0; i <= EndBar; i++)
        {
            Bars[i].Count = cnt;
            Bars[i].Length = Convert.ToInt32(myConstants.BarResolution * Bars[i].Rate);
            cnt += Bars[i].Length;
        }

        //全部の小節を計算し終えたらそこが曲の終了
        EndCount = cnt;

        return true;
    }

    /// <summary>
    /// ノーツ情報とBPM変化情報を読み込む
    /// </summary>
    /// <param name="Lines"></param>
    /// <returns>成否（成功でTrue）</returns>
    private bool LoadNotesData(List<string> Lines)
    {
        foreach(string line in Lines)
        {
            string[] temp;
            if(line != "")
            {
                temp = myConstants.SplitParam(line, ' ');
            }
            else
            {
                //空白行は読み飛ばす
                continue;
            }

            if(temp[0][0] != '#')
            {
                //#以外で始まる行は読み飛ばす
                continue;
            }

            int trash;

            if(int.TryParse(temp[0].Substring(1,5),out trash) == false)
            {
                continue;
            }

            int barnum = int.Parse(temp[0].Substring(1, 3));
            int channel = int.Parse(temp[0].Substring(4, 2));

            if(channel == myConstants.Channel_ChangeBarRate)
            {
                continue;
            }

            if(channel == myConstants.Channel_ChangeBpm)
            {
                BpmData b = new BpmData(double.Parse(temp[1]), Bars[barnum].Count);
                continue;
            }

            int tick = Bars[barnum].Length / temp[1].Length;
            
            for(int i = 0; i < temp[1].Length; i++)
            {
                if(temp[1][i] == '0')
                {
                    continue;
                }
                NoteData nt = new NoteData(Bars[barnum].Count + (tick * i), temp[1][i]);
                Notes[channel].Add(nt);
                NotesNum++;
            }
        }
        return true;
    }
}
