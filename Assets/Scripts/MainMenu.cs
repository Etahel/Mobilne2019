using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private void LoadLevelSelect()
    {
        SceneManager.LoadScene("Assets/Scenes/LevelSelect.unity");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        //PlayerPrefs.SetInt("LevelPassed", 0);
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("LevelPassed", 0);
            PlayerPrefs.SetString("FirstTime", "false");
        }
        playButton.onClick.AddListener(LoadLevelSelect);
        quitButton.onClick.AddListener(QuitGame);
    }

}
