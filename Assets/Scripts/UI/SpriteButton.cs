using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpriteButton : MonoBehaviour, IPointerClickHandler,
                                  IPointerDownHandler, IPointerEnterHandler,
                                  IPointerUpHandler, IPointerExitHandler
{
    public string SceneToLoadOnClick;
    private Color color;

    void Start()
    {
        //Attach Physics2DRaycaster to the Camera
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();

        addEventSystem();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // Debug.Log("Mouse Clicked!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        color = new Color(render.color.r, render.color.b, render.color.g);
        render.color = new Color(render.color.r/2, render.color.b/2, render.color.g/2);
        //render.color = Color.black;
        Debug.Log("Mouse Down!");
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.color = color;
        SceneManager.LoadScene(SceneToLoadOnClick);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
    }

    //Add Event System to the Camera
    void addEventSystem()
    {
        GameObject eventSystem = null;
        GameObject tempObj = GameObject.Find("EventSystem");
        if (tempObj == null)
        {
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
        else
        {
            if ((tempObj.GetComponent<EventSystem>()) == null)
            {
                tempObj.AddComponent<EventSystem>();
            }

            if ((tempObj.GetComponent<StandaloneInputModule>()) == null)
            {
                tempObj.AddComponent<StandaloneInputModule>();
            }
        }
    }

}
