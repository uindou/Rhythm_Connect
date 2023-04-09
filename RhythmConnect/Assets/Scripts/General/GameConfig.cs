using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームプレイに関わるレベルの設定を保持するクラス
/// </summary>
public class GameConfig
{
    public double CalculatedHS { get; private set; }    //BPM*ハイスピードの数値 数字に意味はないが固定HS用に適切な倍率を求めるのに必要
    public double HSMultiplier { get; private set; }    //ハイスピード倍率
    public bool RandomFlag { get; private set; }        //ランダムオプションの有効状態
    public bool MirrorFlag { get; private set; }        //ミラーオプションの有効状態
    public int GaugeMode { get; private set; }       //ゲージの難易度状態 数値に対応するゲージ状態はmyConstantsクラスを参照の事

    /// <summary>
    /// 引数に与えられたファイルからゲームコンフィグを読み込むメソッド
    /// </summary>
    /// <param name="filename"></param>
    /// <returns>成功でTrue 1行もなければFalse</returns>
    public bool LoadGameConfig(string filename)
    {
        List<string> Lines = new List<string>();
        Lines = myConstants.LoadFileToList(filename);
        if(Lines == null)
        {
            return false;
        }

        foreach (string line in Lines)
        {
            string[] temp = myConstants.SplitParam(line, ' ');

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
