using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Collider))]
public class SpriteButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string SceneToLoadOnClick;
    private Color color;

    void Start()
    {
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        addEventSystem();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        color = new Color(render.color.r, render.color.b, render.color.g);
        render.color = new Color(render.color.r/2, render.color.b/2, render.color.g/2);   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.color = color;
        SceneManager.LoadScene(SceneToLoadOnClick);
    }

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
