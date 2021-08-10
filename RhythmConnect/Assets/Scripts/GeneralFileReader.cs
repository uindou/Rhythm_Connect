using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GeneralFileReader : MonoBehaviour
{
    public string[] Line { get; private set; } = new string[8192];
    public int LineNum { get; private set; }
    
    public bool LoadFile(string filename)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                for (int i = 0; i < 8192; i++)
                {
                    if (sr.EndOfStream == false)
                    {
                        Line[i] = sr.ReadLine();
                        LineNum++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.Log(e.Message);
            throw;
        }

        return true;
    }
}
