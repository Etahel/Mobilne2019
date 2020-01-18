using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public Button tutorialButton;

    private void LoadLevelSelect()
    {
        SceneManager.LoadScene("Assets/Scenes/LevelSelect.unity");
    }

    private void LoadTutorial()
    {
        SceneManager.LoadScene("Assets/Scenes/Tutorial.unity");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        if (!MenuMusic.Music.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            MenuMusic.Music.gameObject.GetComponent<AudioSource>().Play();
        }
        //PlayerPrefs.SetInt("LevelPassed", 0);
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("LevelPassed", 0);
            PlayerPrefs.SetString("FirstTime", "false");
        }
        playButton.onClick.AddListener(LoadLevelSelect);
        quitButton.onClick.AddListener(QuitGame);
        tutorialButton.onClick.AddListener(LoadTutorial);
    }

}
