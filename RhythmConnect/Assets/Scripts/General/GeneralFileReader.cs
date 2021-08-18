using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GeneralFileReader : MonoBehaviour
{
    public List<string> Line { get; private set; } = new List<string>();
    public bool LoadFile(string filename)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                while(sr.EndOfStream == false)
                {
                    Line.Add(sr.ReadLine());
                }
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("file load failed.");
            Debug.Log(e.Message);

            return false;
        }

        return true;
    }
}
