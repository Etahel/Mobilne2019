using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.UI;

public class GameScript : CameraScript
{
    public Text SpellAccuracyText;
    public GameObject enemyGObject;

    protected Enemy enemy;
    protected GameManager manager;

    override protected void Start()
    {
        // Tutaj bedziemy mieli rozszerzona funkcjonalnosc o inicjalizowanie przeciwnika itp. 
        base.Start();
        SpellAccuracyText.text = "";
        enemy = enemyGObject.GetComponent<Enemy>();

        foreach (Gesture gesture in trainingSet)
            manager.AddGestureClass(gesture.Name);
            

    }

    public void setManager (GameManager manager)
    {
        this.manager = manager;
    }

    protected void OnRecognition()
    {
        manager.EvaluateGesture(checkPattern());
    }

    override protected void Update()
    {
        base.Update();

        // Sprawdzamy na jakiej platformie sie znajdujemy
        if (android)
        {
            // Sprawdzamy czy kursor znajduje sie wewnatrz strefy rysowania
            if (drawArea.Contains(virtualKeyPosition))
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    //Sprawdzamy czy to pierwszy punkt. Jesli tak - inicjalizujemy
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
                    if (Mathf.Abs((last.X - virtualKeyPosition.x)) + Mathf.Abs((last.Y + virtualKeyPosition.y)) > 5)
                    {
                        addPoint();
                    }
                }

            // Jesli palec zostanie oderwany od ekranu lub ruch sie skonczy z innego powodu - porownujemy i sprzatamy. 
            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                // Sprawdzamy czy w tablicy jest wiecej niz 1 punkt - ten lib rzuca exception jak mu sie jednoelementową lub pusta tablice daje
                if (points.Count > 1)
                    OnRecognition();
                lineClear();
            }

        }
        else
        {
            // Wszystko tutaj adekwatnie do androida, tylko z funkcjami myszki. 
            if (drawArea.Contains(virtualKeyPosition))
                if (Input.GetMouseButton(0))
                {
                    if (points.Count == 0)
                    {
                        lineInit();
                        addPoint();
                    }


                    message = points.Count.ToString() + " " + points[points.Count - 1].X.ToString() + " " + points[points.Count - 1].Y.ToString() + " " + trainingSet.Count;
                    Point last = points[points.Count - 1];

                    if (Mathf.Abs((last.X - virtualKeyPosition.x)) + Mathf.Abs((last.Y - virtualKeyPosition.y)) > 5)
                    {
                        addPoint();
                    }
                }

            if (Input.GetMouseButtonUp(0))
            {
                if (points.Count > 1)
                    OnRecognition();
                lineClear();

            }
        }

    }
}