using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class GameConfigManager : GeneralFileReader
{
    public double CalculatedHS { get; private set; }
    public double HSMultiplier { get; private set; }
    public bool RandomFlag { get; private set; }
    public bool MirrorFlag { get; private set; }
    public string GaugeMode { get; private set; }

    public bool AnalyseConfigString(string filename)
    {
        LoadFile(filename);

        foreach (string line in Line)
        {
            string[] temp = line.Split(' ');

            switch (temp[0])
            {
                case "#CALCULATEDHS":
                    CalculatedHS = double.Parse(temp[1]);
                    break;

                case "#HSMULTIPLIER":
                    HSMultiplier = double.Parse(temp[1]);
                    break;

                case "#RANDOMFLAG":
                    RandomFlag = bool.Parse(temp[1]);
                    break;

                case "#MIRRORFLAG":
                    MirrorFlag = bool.Parse(temp[1]);
                    break;

                case "#GAUGEMODE":
                    GaugeMode = temp[1];
                    break;

                default:
                    break;
            }
        }

        return true;
    }

    public void Start()
    {
        AnalyseConfigString("gameconfig.rccfg");
        Debug.Log(CalculatedHS);
        Debug.Log(HSMultiplier);
        Debug.Log(RandomFlag);
        Debug.Log(MirrorFlag);
        Debug.Log(GaugeMode);
    }
}
