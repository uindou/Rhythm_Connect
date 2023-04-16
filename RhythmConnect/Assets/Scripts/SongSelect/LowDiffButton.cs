using UnityEngine;

public class LowDiffButton : MonoBehaviour
{
    private SongSelecter SSelecter;
    private void Awake()
    {
        SSelecter = GameObject.Find("Musics").GetComponent<SongSelecter>();
    }
    public void OnClick()
    {
        SSelecter.difficult = myConstants.LowDiff;
    }
}