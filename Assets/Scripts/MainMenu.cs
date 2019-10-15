using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string SceneToLoad;
    public Button playButton;
    public Button quitButton;

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        playButton.onClick.AddListener(LoadNextScene);
        quitButton.onClick.AddListener(QuitGame);
    }

}
