using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Klasa abstrakcyjna implementująca podstawowe funkcjonalności skryptu kamery w naszej grze. Dziedzicza po niej skrypty ekranu gry i skrypt ekranu dodawania symbolu
/// </summary>
public abstract class CameraScript : MonoBehaviour
{
    public Transform gestureOnScreenPrefab;

    public string gestureFolder;

    //Lista z zaladowanymi symbolami
    protected List<Gesture> trainingSet = new List<Gesture>();

    // LineRenderer odpowiadajacy za widoczna na ekranie linie
    protected LineRenderer currentGestureLineRenderer;

    //W oryginalnym skrypcie liczylo z ilu linni sklada sie gesture. U nas stale na 0, bo tworzymy symbole tylko z 1 linii. 
    protected int strokeId = 0;

    // Lista punktow, ktore sa zaznaczone na ekranie. 
    protected List<Point> points = new List<Point>();

    // Pozycja kursora/palca
    protected Vector3 virtualKeyPosition = Vector2.zero;

    // Obszar na ktorym mozna rysowac - ustawiony na sztywno, zeby nie bylo takiej zmiany przy wysokosci ekranu. 
    protected Rect drawArea;

    //Okreslaja jaka platforma
    protected RuntimePlatform platform;
    protected bool android = false;

    protected TextAsset[] gesturesXml;




    //Przechowuje wiadomosc ktora jest wyswietlana w rogu ekranu przez funkcje onGui (nie ogarniam jeszcze na jakiej zasadzie jest to wywolywane)
    protected string message;

    // Funkcja inicjalizuje linie na ekranie
    protected void lineInit()
    {
        Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation);
        currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
        currentGestureLineRenderer.positionCount = 0;
    }

    // Dodaje punkt zarowno do linii na ekranie jak i listy points. 
    protected void addPoint()
    {
        points.Add(new Point(Mathf.Round(virtualKeyPosition.x), Mathf.Round(virtualKeyPosition.y), strokeId));
        currentGestureLineRenderer.positionCount++;
        currentGestureLineRenderer.SetPosition(currentGestureLineRenderer.positionCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
    }

    // Dokonuje porownania naszych punktow z zaladowanymi symbolami. Wynik porownania zapisuje w message. 
    protected Result checkPattern()
    {
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
        message = gestureResult.GestureClass + " " + gestureResult.Score;
        return gestureResult;
    }

    // Czysci tablice punktow i linie na ekranie. 
    protected void lineClear()
    {
        points.Clear();
        if (currentGestureLineRenderer != null)
            if (currentGestureLineRenderer.gameObject.scene.IsValid())
                Destroy(currentGestureLineRenderer.gameObject);
    }




    protected virtual void Start()
    {
        // Inicjalizacja wartosci
        platform = Application.platform;
        drawArea = new Rect(0, 0, Screen.width, Screen.height);
        message = "TEST";
        if(MenuMusic.Music != null) MenuMusic.Music.gameObject.GetComponent<AudioSource>().Pause();
        // Zaladowanie gesturow 
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/" + gestureFolder+ "/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Okreslenie platformy
        if (platform == RuntimePlatform.Android)
            android = true;
        else
            android = false;


    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Umiejscawia wirtualny kursor zaleznie od platformy 

        if (android)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

    }


   protected virtual void OnGUI()
    {
        // Wyswietla wiadomosc w rogu ekranu


        GUI.contentColor = Color.yellow;
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.yellow;
       // GUI.Label(new Rect(50, 50, 400, 100), message, guiStyle);
    }

}