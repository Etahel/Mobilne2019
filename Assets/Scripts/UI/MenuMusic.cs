using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    //Pause: 
    private static MenuMusic music = null;
    public static MenuMusic Music { get { return music; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if(music != null && music != this)
        {
            Destroy(this.gameObject);
            return;
        } else
        {
            music = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
