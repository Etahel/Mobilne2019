using PDollarGestureRecognizer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
// Skrypt sluzacy do wygodnego dodawania gesturow - aktualnie dziala tylko na pc, bo w zasadzie nie powinno byc roznicy gdzie gesture jest zdefiniowany. 
//</summary>

public class AddGestureScript : CameraScript
{
    private string newGestureName = "";

    //Nie ma Start() bo inicjalizacja taka sama jak w base klasie. 

    private void Start()
    {
        base.Start();
        drawArea = new Rect(0, Screen.height - 1600, Screen.width, 1600);
    }

    override protected void Update()
    {
        //Ustawienie kursora
        base.Update();

        //Sprawdzamy czy kursor znajduje sie nad strefą rysowania
        if (drawArea.Contains(virtualKeyPosition))
            if (Input.GetMouseButton(0))
            {
                // Sprawdzamy czy to pierwszy punkt. Jesli tak - inicjalizujemy linie i dodajemy go. 
                if (points.Count == 0)
                {
                    lineInit();
                    addPoint();
                }

                // Wrzucam tu troche debbugowego info do message: ilosc punktow, pozycja ostatniego punktu, ilosc zaladowanych gesturow
                message = points.Count.ToString() + " " + points[points.Count - 1].X.ToString() + " " + points[points.Count - 1].Y.ToString() + " " + trainingSet.Count;
                Point last = points[points.Count - 1];

                // Duzym problemem bylo dodawanie masowo punktow kiedy kursor stal w miejscu, wiec dodalem sprawdzanie czy nowy punkt znajduje sie 
                // w jakiejs sensownej odleglosci od poprzedniego
                if (Mathf.Abs((last.X - virtualKeyPosition.x)) + Mathf.Abs((last.Y - virtualKeyPosition.y)) > 5)
                {
                    addPoint();
                }
            }

        // Drugi przycisk myszki czysci ekran. 
        if (Input.GetMouseButtonDown(1))
        {
            lineClear();
        }




    }

    override protected void OnGUI()
    {
        //Tutaj poza wyswietlaniem wiadomosci dodajemy przycisk do rozpoznawania dedykowane i przycisk do dodawania symbolu.


        GUI.contentColor = Color.yellow;
        GUIStyle guiButton = new GUIStyle(GUI.skin.button);
        guiButton.fontSize = 50;
        guiButton.normal.textColor = Color.yellow;

        GUI.contentColor = Color.yellow;
        GUIStyle guiLabel = new GUIStyle();
        guiLabel.fontSize = 50;
        guiLabel.normal.textColor = Color.yellow;

        GUI.contentColor = Color.yellow;
        GUIStyle guiText = new GUIStyle(GUI.skin.textField);
        guiText.fontSize = 60;
        guiText.normal.textColor = Color.yellow;
        //in awake or start use these lines of code to set the size of the font


        base.OnGUI();
        if (GUI.Button(new Rect(50, Screen.height - 300,400,100), "Recognize", guiButton))
        {
            Gesture candidate = new Gesture(points.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            message = gestureResult.GestureClass + " " + gestureResult.Score;
        }

        GUI.Label(new Rect(100, Screen.height - 150,200, 100), "Save as: ", guiLabel);

        newGestureName = GUI.TextField(new Rect(350, Screen.height - 150, 600, 75), newGestureName, guiText);

        if (GUI.Button(new Rect(600, Screen.height - 300, 400, 100), "Save", guiButton) && points.Count > 0 && newGestureName != "")
        {

            string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

#if !UNITY_WEBPLAYER
            GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
#endif

            trainingSet.Add(new Gesture(points.ToArray(), newGestureName));

            newGestureName = "";
        }
    }
}
