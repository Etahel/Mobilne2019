using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect2 : MonoBehaviour
{
    // Start is called before the first frame update

    public Button Lv1;
    public Button lv2;
    public Button lv3;
    public Button boss;

    void Start()
    {
        Lv1.onClick.AddListener(LoadLv1);
    }

    private void LoadLv1()
    {
        SceneManager.LoadScene("Assets/Scenes/Tutorial 1.unity");
    }
}

 
