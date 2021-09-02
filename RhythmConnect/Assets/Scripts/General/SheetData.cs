using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetData
{
    public int NotesNum { get; private set; } = 0;   //譜面に含まれるノーツ数
    public string Genre { get; private set; }   //譜面に含まれる楽曲のジャンル
    public string Artist { get; private set; }
    public string Arranger { get; private set; }
    public double Bpm { get; private set; }
    public int PlayLevel { get; private set; }
    public double Total { get; private set; }

    public bool LoadSheetData(string sheetfilepath)
    {
        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(sheetfilepath);

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

                case "#STARTBPM":
                    Bpm = double.Parse(temp[1]);
                    break;

                case "#PLAYLEVEL":
                    PlayLevel = int.Parse(temp[1]);
                    break;

                case "#TOTAL":
                    Total = double.Parse(temp[1]);
                    break;

                default:
                    if(temp[0][0]!='#')
                    {
                        break;
                    }



                    break;
            }
        }

        return true;
    }
}