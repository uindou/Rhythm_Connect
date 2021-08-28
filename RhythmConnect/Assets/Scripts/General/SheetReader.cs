using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetReader : GeneralFileReader
{
    public int NotesNum { get; private set; }
    public string Genre { get; private set; }

    public SheetReader()
    {
        NotesNum = 0;
    }

    public bool LoadSheetData(string songname)
    {
        LoadFile(@"songdata\" + songname + @"\" + songname + ".rcsht");

        foreach (string line in Line)
        {
            string[] temp = line.Split(' ');

            switch (temp[0])
            {
                case "#GENRE":
                    Genre = temp[1];
                    break;

                default:
                    if(temp[0][0]=='#')
                    {
                        break;
                    }
                    break;
            }
        }

        return true;
    }
}