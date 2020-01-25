using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect2 : MonoBehaviour
{

    public LevelButton Lv1;
    public LevelButton Lv2;
    public LevelButton Lv3;
    public LevelButton back;
    public LevelButton reset;
    public GameObject badgePanel;

    void Start()
    {
        if (!MenuMusic.Music.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            MenuMusic.Music.gameObject.GetComponent<AudioSource>().Play();
        }
        int levelPassed = PlayerPrefs.GetInt("LevelPassed");
        Lv1.SetInteractable(true);
        Lv2.SetInteractable(false);
        Lv3.SetInteractable(false);
        back.SetInteractable(true);
        reset.SetInteractable(true);
        badgePanel.SetActive(false);
        switch (levelPassed)
        {
            case 0:
                break;
            case 1:
                Lv2.SetInteractable(true);
                break;
            case 2:
                Lv2.SetInteractable(true);
                Lv3.SetInteractable(true);
                break;
            default:// >=3
                Lv2.SetInteractable(true);
                Lv3.SetInteractable(true);
                badgePanel.SetActive(true);
                break;
        }
        AddListeners();
    }

    private void AddListeners()
    {
        Lv1.AddListener(LoadLv1);
        Lv2.AddListener(LoadLv2);
        Lv3.AddListener(LoadLv3);
        back.AddListener(GoBack);
        reset.AddListener(resetprefs);
    }

    private void resetprefs()
    {
        PlayerPrefs.SetInt("LevelPassed", 0);
        SceneManager.LoadScene("Assets/Scenes/LevelSelect2.unity");
    }

    private void LoadLv1()
    {
        SceneManager.LoadScene("Assets/Scenes/Level1.unity");
    }

    private void LoadLv2()
    {
        SceneManager.LoadScene("Assets/Scenes/Level2.unity");
    }

    private void LoadLv3()
    {
        SceneManager.LoadScene("Assets/Scenes/Level3.unity");
    }


    private void GoBack()
    {
        SceneManager.LoadScene("Assets/Scenes/LevelSelect.unity");
    }
}

 
