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
        playButton.onClick.AddListener(LoadLevelSelect);
        quitButton.onClick.AddListener(QuitGame);
    }

}
