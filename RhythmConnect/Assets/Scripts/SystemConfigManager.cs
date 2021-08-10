using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SystemConfigManager : GeneralFileReader
{
    public char[] KeyConfig { get; private set; } = new char[4];
    public string SongDataFolderPath { get; private set; }

    public bool AnalyseConfigString(string filename)
    {
        LoadFile(filename);

        for (int i = 0; i < LineNum; i++)
        {
            string[] temp = Line[i].Split(' ');

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

    // Start is called before the first frame update
    void Start()
    {
        AnalyseConfigString("systemconfig.rccfg");
        Debug.Log(SongDataFolderPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
