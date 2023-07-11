using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RC.Scene.FreePlay;

public class ButtonDecide : MonoBehaviour
{
    public void OnClick()
    {
        GameObject _canvas;
        _canvas = GameObject.Find("Canvas");
        if (_canvas == null)
        {
            return;
        }
        _canvas.GetComponent<FreePlay>().ClickDecide();
    }
}