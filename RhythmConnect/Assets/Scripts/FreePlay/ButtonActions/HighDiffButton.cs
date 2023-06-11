using UnityEngine;

public class HighDiffButton : MonoBehaviour
{
    private FreePlayUI Fp;
    private void Awake()
    {
        Fp = GameObject.Find("Canvas").GetComponent<FreePlayUI>();
    }
    public void OnClick()
    {
        Fp.Difficult = myConstants.HighDiff;
    }
}