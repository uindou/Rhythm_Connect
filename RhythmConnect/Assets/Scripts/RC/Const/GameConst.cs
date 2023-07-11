using UnityEditor;
using UnityEngine;

namespace RC.Const
{
    public enum Difficulty
    {
        Simple
      , Decent
      , Complex
    }

    public enum NoteType
    {
        None
      , Tap
      , StartHold
      , EndHold
    }

    public enum Channels
    {
        LaneA
      , LaneB
      , LaneC
      , LaneD
      , LaneAB
      , LaneBC
      , LaneCD
      , LaneABC
      , LaneBCD
      , LaneABCD
      , ChangeBpm
      , Bar
    }

    public class GameConst
    {
        public static readonly int BarResolution = 9600;
    }
}