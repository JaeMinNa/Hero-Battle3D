using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public event Action OnDie;

    public bool IsDead => health == 0;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;
        health = Mathf.Max(health - damage, 0);
        
        GameManager.Instance.UIManager.SetPlayerHealthBar();
        GameManager.Instance.UIManager.SetEnemyHealthBar();
        
        if(this.gameObject.tag == "Player")
        {
            int index;
            index = this.gameObject.GetComponent<Player>().stateMachine.ComboIndex;

            if(index == 0)
            {
                GameManager.Instance.SoundManager.SFX("PlayerAttack1");
            }
            else if (index == 1)
            {
                GameManager.Instance.SoundManager.SFX("PlayerAttack2");
            }
            else if(index == 2)
            {
                GameManager.Instance.SoundManager.SFX("PlayerAttack3");
            }
        }
        else if(this.gameObject.tag == "Enemy")
        {
            GameManager.Instance.SoundManager.SFX("EnemyAttack");
        }

        if (health == 0)
            OnDie?.Invoke();
    }
}