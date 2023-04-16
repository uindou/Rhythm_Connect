using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 小節線データ
/// </summary>
public class BarData
{
    public double Rate { get; set; }    //譜面に定義されている小節長倍率
    public int Count { get; set; }       //この小節線が画面で実際に流れるタイミング
    public int Length { get; set; }      //Rateから計算される実際の小節の長さ

    public BarData()
    {
        Rate = 1.0;
    }
}