using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image timerBar;
    public Enemy enemy;
    public Player player;
    private float maxTime;
    private float timeLeft;


    // Start is called before the first frame update
    void Start()
    {       
        maxTime = enemy.AttackTime;
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0) // Zmniejszanie paska
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else // rob cos gdy uplynal czas
        {
            timerBar.fillAmount = 1;
            timeLeft = maxTime;

            player.TakeDamage(Random.Range(enemy.MinDmg, enemy.MaxDmg));
        }
        
    }
}
