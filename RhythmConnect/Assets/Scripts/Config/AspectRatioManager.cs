using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void FullScreen()
    {
        Screen.SetResolution(1920, 1080,true, 60);
    }
    public void Aspect16_9()
    {
        Screen.SetResolution(1920,1080, false, 60);
    }
    public void Aspect4_3()
    {
        Screen.SetResolution(960,720, false, 60);
    }
}
