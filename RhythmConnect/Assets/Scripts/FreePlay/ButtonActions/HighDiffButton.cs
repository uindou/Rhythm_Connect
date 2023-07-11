using UnityEngine;
using RC.Scene.FreePlay;
using RC.Const;

public class HighDiffButton : MonoBehaviour
{
    private FreePlay Fp;
    private void Awake()
    {
        Fp = GameObject.Find("Canvas").GetComponent<FreePlay>();
    }
    public void OnClick()
    {
        Fp.NowDifficulty = Difficulty.Complex;
    }
}