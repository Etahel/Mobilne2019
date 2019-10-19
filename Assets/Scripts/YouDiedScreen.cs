using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouDiedScreen : MonoBehaviour
{
    public Button TryAgainButton;
    public Button LevelSelectButton;
    public Button MainMenuButton;

    private void LoadLevelSelect()
    {
        SceneManager.LoadScene("Assets/Scenes/LevelSelect.unity");
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }

    private void TryAgain()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    // Start is called before the first frame update
    void Start()
    {
        TryAgainButton.onClick.AddListener(TryAgain);
        LevelSelectButton.onClick.AddListener(LoadLevelSelect);
        MainMenuButton.onClick.AddListener(LoadMainMenu);

    }
}
