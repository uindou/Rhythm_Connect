using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivePanel : MonoBehaviour
{
    [SerializeField] GameObject active_panel;
    [SerializeField] List<GameObject> hide_panels; 

    public void OnClick()
    {
        active_panel.SetActive(true);
        foreach (GameObject g in hide_panels)
        {
            g.SetActive(false);
        }
    }
}
