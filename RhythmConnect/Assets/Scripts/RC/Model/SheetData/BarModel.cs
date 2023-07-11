using UnityEditor;
using UnityEngine;

namespace RC.Model
{
    public class BarModel : NoteModel
    {
        public double Rate { get; set; }
        public int Length { get; set; }

        public BarModel(){}
    }
}