using UnityEditor;
using UnityEngine;
using RC.Const;

namespace RC.Model
{
    public class PlayNoteModel : NoteModel
    {
        public NoteType noteKind { get; private set; }

        public PlayNoteModel(int count, NoteType noteKind)
        {
            Count = count;
            this.noteKind = noteKind;
        }
    }
}