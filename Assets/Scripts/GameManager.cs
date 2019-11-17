using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject enemyGObject;
    public GameObject cameraGObject;

    private Enemy enemy;
    private Timer timer; 
    private GameScript game;

    private List<string> gestureClasses;
    private string currentGesture;

    private GameObject displayedGesture;



    // Start is called before the first frame update
    void Awake()
    {
        enemy = enemyGObject.GetComponent<Enemy>();
        timer = enemyGObject.GetComponent<Timer>();
        game = cameraGObject.GetComponent<GameScript>();
        gestureClasses = new List<string>();

        game.setManager(this);
        timer.setManager(this);
        game.setManager(this);
    }

    private void Start()
    {
   
    }

    public void AddGestureClass(string gestureClass)
    {
        gestureClasses.Add(gestureClass);
    }

    private string GetRandomGesture()
    {
        return gestureClasses[Random.Range(0,gestureClasses.Count-1)];
    }

    //ToDo: Rozne sposoby generowania
    public void ChangeGesture()
    {

        if (displayedGesture != null)
            Destroy(displayedGesture);

        currentGesture = GetRandomGesture();
        displayedGesture = Instantiate(Resources.Load(currentGesture)) as GameObject;
    }

    public void EvaluateGesture(Result result)
    {
        float gestureScore = result.Score;
        string gestureClass = result.GestureClass;
        game.SpellAccuracyText.canvasRenderer.SetAlpha(1.0f);

        if (gestureClass == currentGesture)
        {
            if (gestureScore >= 0.9)
            {
                game.SpellAccuracyText.color = Color.green;
                game.SpellAccuracyText.text = "GREAT !";
                enemy.TakeDamage(gestureScore * 30);
                timer.resetTimer();
            }

            if (gestureScore >= 0.5 && gestureScore < 0.9)
            {
                game.SpellAccuracyText.color = Color.green;
                game.SpellAccuracyText.text = "GOOD";
                enemy.TakeDamage(gestureScore * 30);
                timer.resetTimer();
            }
            if  (gestureScore < 0.5)
            {
                game.SpellAccuracyText.color = new Color32(255, 149, 0, 255);
                game.SpellAccuracyText.text = "TRY HARDER";
            }
        }
        else
        {
            game.SpellAccuracyText.color = new Color32(255, 149, 0, 255);
            game.SpellAccuracyText.text = "NOT THIS ONE";
        }



        game.SpellAccuracyText.CrossFadeAlpha(0.0f, 1.0f, false);
    }

}
