using UnityEditor;
using UnityEngine;

namespace RC.Model
{
    public class ChangeBpmEventModel : NoteModel
    {
        public double Bpm { get; private set; }

        public ChangeBpmEventModel(int count, double bpm)
        {
            Count = count;
            Bpm = bpm;
        }
    }
}