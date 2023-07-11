using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RC.Scene.FreePlay;

public class ButtonCancel : MonoBehaviour
{
    public void OnClick()
    {
        GameObject _canvas;
        _canvas = GameObject.Find("Canvas");
        if(_canvas != null)
        {
            _canvas.GetComponent<FreePlay>().ClickCancel();
        }
    }
}