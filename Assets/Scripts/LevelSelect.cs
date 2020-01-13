using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public Button FTIMS;
    public LevelButton Back;

    private void LoadFTIMS()
    {
        SceneManager.LoadScene("Assets/Scenes/LevelSelect2.unity");
    }
    private void LoadMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }
    void Start()
    {
        Back.AddListener(LoadMenu);
        FTIMS.onClick.AddListener(LoadFTIMS);
    }


}
