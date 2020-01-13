using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class LevelButton : MonoBehaviour
{
    public Button button;
    public Image buttonImage;
    public Sprite active;
    public Sprite disabled;

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
        if (interactable)
        {
            button.image.sprite = active;
        } else
        {
            button.image.sprite = disabled;
        }
    }

    private void playAudio()
    {
        GetComponent<AudioSource>().Play();
    }

    public void AddListener(UnityEngine.Events.UnityAction call)
    {
        button.onClick.AddListener(playAudio);
        button.onClick.AddListener(call);
    }

    

    
}
