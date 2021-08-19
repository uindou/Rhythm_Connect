using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private int cycle;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        i += 1;
        if (i % cycle == 0)
        {
            var r = Random.Range(0, 255);
            var g = Random.Range(0, 255);
            var b = Random.Range(0, 255);
            cardNameText.faceColor = new Color32((byte)r, (byte)g, (byte)b, 255);
        }

    }
}
