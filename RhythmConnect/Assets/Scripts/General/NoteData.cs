using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ノート1個の情報を保持するクラス
public class NoteData
{
    public int Count { get; private set; }      //このノートが実際に画面で流れるタイミング
    public int NoteKind { get; private set; }   //このノートの種類（myConstantsを参照の事）
    public int Exist { get; set; }              //既に消えたノートかの判定用フラグ

    public NoteData(int count, int kind)
    {
        Count = count;
        NoteKind = kind;
        Exist = 1;
    }
}