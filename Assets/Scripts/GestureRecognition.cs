using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;

public class GestureRecognition : MonoBehaviour
{
    public Transform gestureOnScreenPrefab;

    private List<Gesture> trainingSet = new List<Gesture>();

    private List<Point> points = new List<Point>();
    private int strokeId = 0;

    private Vector3 virtualKeyPosition = Vector2.zero;
    private Rect GuiArea;

    private RuntimePlatform platform;
    private int vertexCount = 0;

    private bool android = false;

    private LineRenderer currentGestureLineRenderer;

    private GameObject result;



    //GUI
    private bool overGui = false;
    private GUIStyle guiStyle = new GUIStyle();
    private string message;
    private bool recognized;
    private string newGestureName = "";

    private void lineInit()
    {
        Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation);
        currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
        currentGestureLineRenderer.positionCount = 0;
    }

    private void addPoint()
    {
        points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));
        currentGestureLineRenderer.positionCount++;
        currentGestureLineRenderer.SetPosition(currentGestureLineRenderer.positionCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
    }

    private void checkPattern()
    {
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
        message = gestureResult.GestureClass + " " + gestureResult.Score;
    }

    public void lineClear()
    {
        points.Clear();
        Destroy(currentGestureLineRenderer.gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
        platform = Application.platform;
        GuiArea = new Rect(0, 0, Screen.width, 400);

        message = "TEST";


        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        if (platform == RuntimePlatform.Android)
            android = true;
        else
            android = false;


    }

    // Update is called once per frame
    void Update()
    {

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




        

       if (android)
        {

            if (GuiArea.Contains(virtualKeyPosition))
            {

            }
            else
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (points.Count == 0)
                {
                    lineInit();
                    addPoint();
                }

                message = points.Count.ToString() + " " + virtualKeyPosition.x.ToString() + " -" + virtualKeyPosition.y.ToString() + " " + trainingSet.Count;
                Point last = points[points.Count - 1];

                if (Mathf.Abs((last.X - virtualKeyPosition.x)) + Mathf.Abs((last.Y + virtualKeyPosition.y)) > 5)
                {
                    addPoint();
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {

                checkPattern();
                lineClear();
            }

        }
        else
        {


            if (Input.GetMouseButton(0))
            {
                if (points.Count == 0)
                {
                    lineInit();
                    addPoint();
                }


                message = points.Count.ToString() + " " + virtualKeyPosition.x.ToString() + " -" + virtualKeyPosition.y.ToString() + " " + trainingSet.Count;
                Point last = points[points.Count - 1];

                if (Mathf.Abs((last.X - virtualKeyPosition.x)) + Mathf.Abs((last.Y + virtualKeyPosition.y)) > 5)
                {
                    addPoint();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (points.Count > 1)
                    checkPattern();
                lineClear();

            }
        }



    }

    void OnGUI()
    {
        GUI.contentColor = Color.yellow;
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.yellow;
        GUI.Label(new Rect(0, 0, 400, 400), message, guiStyle);


        if (GUI.Button(new Rect(Screen.width - 200, 50, 100, 30), "Recognize"))
        {

            recognized = true;

            Gesture candidate = new Gesture(points.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            message = gestureResult.GestureClass + " " + gestureResult.Score;
        }
    }
}
