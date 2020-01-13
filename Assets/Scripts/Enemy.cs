using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour // klasa wroga w scenie
{
    public int Number;
    public string Name;
    public float Hp;
    public float AttackTime = 5f;
    public int MinDmg, MaxDmg; //ile obrazen zadaje
    public string SceneToLoadOnDeath;
    public AudioSource hitSound;
    public Text textRenderer;
    public Image hpBar;
    private Animator anim;
    private float currentHP;

    private GameManager manager;

    public bool IsKilled()
    {
        if (currentHP <= 0) return true;
        return false;
    }

    public void TakeDamage(float damage)
    {
        anim.SetTrigger("Defend");

        if (damage > MaxDmg)
            damage = MaxDmg;
        if (damage < MinDmg)
            damage = MinDmg;

        currentHP -= damage;
        hpBar.fillAmount = currentHP / Hp;
        hitSound.Play();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        textRenderer.text = Name;
        currentHP = Hp;

    }

    public void setManager(GameManager manager)
    {
        this.manager = manager;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (IsKilled())
        {
            PlayerPrefs.SetInt("LevelPassed", Number);
            SceneManager.LoadScene(SceneToLoadOnDeath);
        }
    }

}
