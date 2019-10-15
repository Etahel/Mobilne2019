using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour // klasa wroga w scenie
{

    public string SceneToLoadOnDeath;
    public float Hp = 100f; 
    public string Name; 
    public int MinDmg, MaxDmg; //ile obrazen zadaje

    public bool IsKilled()
    {
        if (Hp <= 0) return true;
        return false;
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
    }

    /*void Start()
    {
       
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (IsKilled())
        {
            SceneManager.LoadScene(SceneToLoadOnDeath);
        }
    }
}
