using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JUB_HUDManager : MonoBehaviour
{
    public int maxLife, currentLife, currentBonbons;
    public Text displayLife, displayBonbons;
    
    void Start()
    {
        currentLife = maxLife;
    }

    void Update()
    {
        //displayBonbons.text = "" + currentBonbons;
        displayLife.text = currentLife.ToString() + " / " + maxLife.ToString();
        if(currentLife > maxLife)
        {
            currentLife = maxLife;
        }         
        //mettre le sucre d'orge �tape par �tape un array qui si life = �a num�ro array = �a
    }

    public void TakeDamages(int damages)
    {
        currentLife -= damages;
        if(currentLife <= 0)
        {
            currentLife = 0;
            Die();
        }
    }

    public void Heal(int heal)
    {
        currentLife += heal;
        if(currentLife > maxLife)
        {
            currentLife = maxLife;
        }
    }

    public void MaxUpgrades(int upgrade)
    {
        maxLife += upgrade;
        currentLife += upgrade;
    }

    public void Die()
    {
        //RIP
        //anim mort
        //respawn checkpoint
    }
}
