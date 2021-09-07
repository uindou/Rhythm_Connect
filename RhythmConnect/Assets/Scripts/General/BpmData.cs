using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//譜面中のBPM変化を記録する為のクラス（実質構造体）
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