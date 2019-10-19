using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Text hpText;
    public AudioSource hitSound;
    public Image hpBar;
    private float currentHP;
    private float maxHP;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = 100f;
        currentHP = maxHP;
        HpTextUpdate();
    }

    void HpTextUpdate()
    {
        hpText.text = currentHP.ToString("0") + " / " + maxHP.ToString("0");
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpBar.fillAmount = currentHP / maxHP;
        hitSound.Play();
        HpTextUpdate();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().path); //do wczytania w you died
            SceneManager.LoadScene("Assets/Scenes/YouDied.unity");
        }
    }
}
