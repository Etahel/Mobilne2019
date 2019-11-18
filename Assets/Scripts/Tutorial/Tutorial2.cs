using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial2 : MonoBehaviour
{

    private bool android;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            android = true;
        else
            android = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (android)
        {
            if (Input.touchCount > 0)
            {
                SceneManager.LoadScene("Assets/Scenes/Tutorial 3.unity");
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("Assets/Scenes/Tutorial 3.unity");
            }
        }
    }
}
