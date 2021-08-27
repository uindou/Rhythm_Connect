using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SystemConfigManager : GeneralFileReader
{
    public char[] KeyConfig { get; private set; } = new char[4];
    public string SongDataFolderPath { get; private set; }

    public bool LoadSystemConfig(string filename)
    {
        LoadFile(filename);

        foreach (string line in Line)
        {
            string[] temp = line.Split(' ');

            switch (temp[0])
            {
                case "#KEYCONFIG":
                    string[] kc = temp[1].Split(',');
                    for (int j = 0; j < 4; j++)
                    {
                        KeyConfig[j] = kc[j][0];
                    }
                    break;

                case "#SONGDATAFOLDER":
                    SongDataFolderPath = temp[1];
                    break;

                default:
                    break;
            }
        }

        return true;
    }
}
