using UnityEngine;

public class MidDiffButton : MonoBehaviour
{
    private FreePlayUI Fp;
    private void Awake()
    {
        Fp = GameObject.Find("Canvas").GetComponent<FreePlayUI>();
    }
    public void OnClick()
    {
        Fp.Difficult = myConstants.MidDiff;
    }
}