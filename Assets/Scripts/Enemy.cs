using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public int hp { get; set; }

    void Start()
    {
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
