using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OmankoManager : MonoBehaviour
{
    public string scene;
    void Start() { }
    // Start is called before the first frame update

    public void OnClick()
    {
        SceneManager.LoadScene(scene);
    }
}
