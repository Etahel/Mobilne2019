using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public Button FTIMS;

    private void LoadFTIMS()
    {
        SceneManager.LoadScene("Assets/Scenes/LevelSelect2.unity");
    }
    void Start()
    {
        FTIMS.onClick.AddListener(LoadFTIMS);
    }


}
