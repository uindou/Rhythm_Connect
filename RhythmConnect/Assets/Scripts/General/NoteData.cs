using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteData
{
    public int Count { get; private set; }
    public int NoteKind { get; private set; }
    public int Exist;

    public NoteData(int count, int kind)
    {
        Count = count;
        NoteKind = kind;
        Exist = 1;
    }
}