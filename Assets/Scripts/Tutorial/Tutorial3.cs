using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial3 : MonoBehaviour
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
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("Assets/Scenes/Tutorial 4.unity");
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("Assets/Scenes/Tutorial 4.unity");
            }
        }
    }
}
