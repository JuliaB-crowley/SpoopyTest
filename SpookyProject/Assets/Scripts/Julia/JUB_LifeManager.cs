using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JUB_LifeManager : MonoBehaviour
{
    public int maxLife, currentLife;
    public Text displayLife;
    
    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        displayLife.text = currentLife.ToString() + " / " + maxLife.ToString();
        //mettre le sucre d'orge étape par étape un array qui si life = ça numéro array = ça
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
        //anim mort
        //respawn checkpoint
    }
}
