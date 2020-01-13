using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    public GameObject enemy;
    public GameObject ui;
    public GameObject prop;
    public GameObject filter;
    public TextMeshProUGUI text;




    private const string enemyLayer = "Enemy";
    private const string uiLayer = "UI";
    private const string foregroundLayer = "Foreground";

    private const string mess1 = "Witaj dzielny Studencie! Politechnikę zaatakowały potowry i musisz je zatrzymać!";
    private const string mess2 = "Rysuj na ekranie wyświetlane symbole. Uważaj - one ciągle sie zmieniają";
    private const string mess3 = "Potwor zaatakuje jesli bedziesz zbyt wolny. Śledź pozostaly czas oraz stany zdrowia na odpowiednich paskach!";
    private const string mess4 = "Gotowy? W takim razie spróbuj swoich sił przeciwko patyczakowi testowemu!";


    private bool android;
    private int tutorialStep;
    // Start is called before the first frame update
    void Start()
    {
        tutorialStep = 1;

        text.SetText(mess1);


        if (Application.platform == RuntimePlatform.Android)
            android = true;
        else
            android = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (android)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //SceneManager.LoadScene("Assets/Scenes/Tutorial 2.unity");
                advanceTutorial();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //SceneManager.LoadScene("Assets/Scenes/Tutorial 2.unity");
                advanceTutorial();
            }
        }


    }

    private void advanceTutorial()
    {
        switch (tutorialStep)
        {
            case 1:
                loadStageTwo();
                break;
            case 2:
                loadStageThree();
                break;
            case 3:
                loadStageFour();
                break;
            case 4:
                endTutorial();
                break;
        }

    }

    private void loadStageTwo()
    {
        tutorialStep = 2;
        enemy.GetComponent<Renderer>().sortingLayerName = enemyLayer;
        prop.GetComponent<Renderer>().sortingLayerName = foregroundLayer;
        text.SetText(mess2);

    }

    private void loadStageThree()
    {
        tutorialStep = 3;
        prop.GetComponent<Renderer>().sortingLayerName = uiLayer;
        ui.GetComponent<Canvas>().sortingLayerName = foregroundLayer;
        text.SetText(mess3);
    }

    private void loadStageFour()
    {
        tutorialStep = 4;
        ui.GetComponent<Canvas>().sortingLayerName = uiLayer;
        enemy.GetComponent<Renderer>().sortingLayerName = foregroundLayer;
        text.SetText(mess4);
    }

    private void endTutorial()
    {
        SceneManager.LoadScene("Assets/Scenes/TutorialGame.unity");
    }
}
