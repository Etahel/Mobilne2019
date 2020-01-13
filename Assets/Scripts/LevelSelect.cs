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
        if (!MenuMusic.Music.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            MenuMusic.Music.gameObject.GetComponent<AudioSource>().Play();
        }
        Back.AddListener(LoadMenu);
        FTIMS.onClick.AddListener(LoadFTIMS);
    }


}
