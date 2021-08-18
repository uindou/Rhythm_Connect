using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GeneralFolderReader : MonoBehaviour
{
    public List<string> Folders { get; private set; } = new List<string>();
    
    public bool LoadFolder(string foldername)
    {
        try
        {
            string[] temp = Directory.GetDirectories(foldername);
            foreach(string fn in temp)
            {
                Folders.Add(fn);
            }
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("Directory not found.");
            Debug.Log(e.Message);

            return false;
        }

        return true;
    }

    public void Start()
    {
        LoadFolder("songdata");
        foreach(string s in Folders)
        {
            Debug.Log(s);
        }
    }
}
