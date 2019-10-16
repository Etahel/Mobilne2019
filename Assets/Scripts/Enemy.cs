using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour // klasa wroga w scenie
{

    public string Name;
    public float Hp;
    public float AttackTime = 5f;
    public int MinDmg, MaxDmg; //ile obrazen zadaje
    public string SceneToLoadOnDeath;
    public Text textRenderer;
    public Image hpBar;

    private float currentHP;

    public bool IsKilled()
    {
        if (currentHP <= 0) return true;
        return false;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        hpBar.fillAmount = currentHP / Hp;
    }

    void Start()
    {
        textRenderer.text = Name;
        currentHP = Hp;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (IsKilled())
        {
            SceneManager.LoadScene(SceneToLoadOnDeath);
        }
    }
}
