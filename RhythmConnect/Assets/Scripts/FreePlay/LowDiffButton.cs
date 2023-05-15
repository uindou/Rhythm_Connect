using UnityEngine;

public class LowDiffButton : MonoBehaviour
{
    private FreePlayUI Fp;
    private void Awake()
    {
        Fp = GameObject.Find("Canvas").GetComponent<FreePlayUI>();
    }
    public void OnClick()
    {
        Fp.Difficult = myConstants.LowDiff;
    }
}