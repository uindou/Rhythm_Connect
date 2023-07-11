using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using RC.Const;
using System;

namespace RC.Model
{
    public class SheetModel
    {
        private List<List<NoteModel>> _notes;

        public int NotesNum
        {
            get
            {
                int notesNum = 0;
                for (int i = 0; i <= (int)Channels.LaneABCD; i++)
                {
                    notesNum += _notes[i].Count;
                }
                return notesNum;
            }
        }

        public List<List<NoteModel>> PlayNotesList
        {
            get
            {
                List<List<NoteModel>> playNotesList = new List<List<NoteModel>>();
                for (int i = 0; i <= (int)Channels.LaneABCD; i++)
                {
                    playNotesList.Add(_notes[i]);
                }
                return playNotesList;
            }
        }

        public List<NoteModel> ChangeBpmList
        {
            get
            {
                return _notes[(int)Channels.ChangeBpm];
            }
        }

        public List<NoteModel> BarList
        {
            get
            {
                return _notes[(int)Channels.Bar];
            }
        }

        public SheetModel(List<List<NoteModel>> notesData)
        {
            _notes = notesData;
        }
    }
}