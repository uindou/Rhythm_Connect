﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetData
{
    public List<NoteData>[] Notes { get; private set; } = new List<NoteData>[myConstants.LaneNum];
    public BarData[] Bars { get; private set; } = new BarData[myConstants.MaxBarNum];
    public List<BpmData> BpmList { get; private set; } = new List<BpmData>();
    public int EndBar { get; private set; } = 0;
    public int EndCount {get; private set; } = 0;
    public int NotesNum { get; private set; } = 0;   //譜面に含まれるノーツ数
    public string Genre { get; private set; }   //譜面に含まれる楽曲のジャンル
    public string Artist { get; private set; }
    public string Arranger { get; private set; }
    public int PlayLevel { get; private set; }
    public double Total { get; private set; }

    public SheetData()
    {
        for(int i = 0; i < myConstants.LaneNum; i++)
        {
            Notes[i] = new List<NoteData>();
        }

        for(int i = 0; i < myConstants.MaxBarNum; i++)
        {
            Bars[i] = new BarData();
        }
    }

    public bool LoadSheetData(string sheetfilepath)
    {
        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(sheetfilepath);

        LoadHeaderData(Lines);
        LoadNotesData(Lines);

        return true;
    }

    private bool LoadHeaderData(List<string> Lines)
    {
        foreach (string line in Lines)
        {
            string[] temp = myConstants.SplitParam(line, ' ');

            if(temp[0][0]!='#')
            {
                continue;
            }

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

                case "#STARTBPM":
                    BpmData startbpm = new BpmData(double.Parse(temp[1]), 0);
                    BpmList.Add(startbpm);
                    break;

                case "#PLAYLEVEL":
                    PlayLevel = int.Parse(temp[1]);
                    break;

                case "#TOTAL":
                    Total = double.Parse(temp[1]);
                    break;

                default:
                    int barnum = int.Parse(temp[0].Substring(1, 3));
                    int channel = int.Parse(temp[0].Substring(5, 2));

                    if(channel == myConstants.Channel_ChangeBarRate)
                    {
                        Bars[barnum].Rate = double.Parse(temp[1]);
                        break;
                    }

                    if(EndBar < barnum)
                    {
                        EndBar = barnum;
                    }
                    
                    break;
            }
        }

        EndBar++;

        int cnt = 0;

        for(int i = 0; i <= EndBar; i++)
        {
            Bars[i].Count = cnt;
            Bars[i].Length = Convert.ToInt32(myConstants.BarResolution * Bars[i].Rate);
            cnt += Bars[i].Length;
        }

        EndCount = cnt;

        return true;
    }

    private bool LoadNotesData(List<string> Lines)
    {
        foreach(string line in Lines)
        {
            string[] temp = myConstants.SplitParam(line, ' ');

            if(temp[0][0] != '#')
            {
                continue;
            }

            int barnum = int.Parse(temp[0].Substring(1, 3));
            int channel = int.Parse(temp[0].Substring(5, 2));

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
                if(temp[1][i] == 0)
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
