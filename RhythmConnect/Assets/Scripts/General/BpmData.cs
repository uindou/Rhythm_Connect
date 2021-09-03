using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BpmData
{
    public double Bpm { get; private set; }
    public int Count { get; private set; }

    public BpmData(double bpm, int count)
    {
        Bpm = bpm;
        Count = count;
    }
}