using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial4 : MonoBehaviour
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
                SceneManager.LoadScene("Assets/Scenes/Game.unity");
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("Assets/Scenes/Game.unity");
            }
        }
    }
}
