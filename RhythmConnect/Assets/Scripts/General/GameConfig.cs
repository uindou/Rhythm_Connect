using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲームプレイに関わるレベルの設定を保持するクラス

class GameConfig
{
    public double CalculatedHS { get; private set; }    //BPM*ハイスピードを計算した後の数値 固定HS用
    public double HSMultiplier { get; private set; }    //HS倍率 固定HSを使わない奇特な方向け
    public bool RandomFlag { get; private set; }        //ランダムオプションの有効状態
    public bool MirrorFlag { get; private set; }        //ミラーオプションの有効状態
    public int GaugeMode { get; private set; }       //ゲージの難易度状態 数値に対応するゲージ状態はmyConstantsクラスを参照の事

    public bool LoadGameConfig(string filename)
    {
        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(filename);

        foreach (string line in Lines)
        {
            string[] temp = myConstants.SplitParam(line, " ");

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
                    GaugeMode = int.Parse(temp[1]);
                    break;

                default:
                    break;
            }
        }

        return true;
    }
}
