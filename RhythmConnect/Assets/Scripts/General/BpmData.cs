using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 譜面中のBPM変化をノーツと同じようなタイミングデータとして保持する
/// </summary>
public class BpmData
{
    public double Bpm { get; private set; }
    public int Count { get; private set; }  //BPMが変化するタイミング

    public BpmData(double bpm, int count)
    {
        Bpm = bpm;
        Count = count;
    }
}