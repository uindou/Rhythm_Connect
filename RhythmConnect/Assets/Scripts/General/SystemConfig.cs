using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SystemConfig
{
    public char[] KeyConfig { get; private set; } = new char[4];

    public bool LoadSystemConfig(string filename)
    {
        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(filename);

        foreach (string line in Lines)
        {
            string[] temp = myConstants.SplitParam(line, ' ');

            switch (temp[0])
            {
                case "#KEYCONFIG":
                    string[] kc = temp[1].Split(',');
                    for (int j = 0; j < 4; j++)
                    {
                        KeyConfig[j] = kc[j][0];
                    }
                    break;

                default:
                    break;
            }
        }

        return true;
    }
}
