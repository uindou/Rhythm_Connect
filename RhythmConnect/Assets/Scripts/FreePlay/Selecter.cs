using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum phase
{
    FolderSelect,
    SongSelect
}

public class Selecter : MonoBehaviour
{
    private phase nowPhase = phase.FolderSelect;

    private void Awake()
    {
        
    }
}