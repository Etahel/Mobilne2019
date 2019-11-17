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
    private Animator anim;

    private GameManager manager;



    // Start is called before the first frame update
    void Start()
    {       
        maxTime = enemy.AttackTime;
        timeLeft = maxTime;
        anim = GetComponent<Animator>();

       // manager.ChangeGesture();

 
    }

    public void setManager(GameManager manager)
    {
        this.manager = manager;
    }

    // Update is called once per frame
    void Update()
    {
    
        if(timeLeft > 0) // Zmniejszanie paska
        {
            if (timeLeft == maxTime)
            {
                manager.ChangeGesture();
            }
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else // rob cos gdy uplynal czas
        {
            resetTimer();

          
            anim.SetTrigger("Attack");
            player.TakeDamage(Random.Range(enemy.MinDmg, enemy.MaxDmg));
        }
        
    }

    public void resetTimer ()
    {
        timerBar.fillAmount = 1;
        timeLeft = maxTime;
    }
}
