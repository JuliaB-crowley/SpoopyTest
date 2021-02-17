using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUB_EnnemyDamage : MonoBehaviour
{
    public float maxHealth;
    [SerializeField]
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void TakeDamage(float damage)
    {
        //animation dégats
        //son dégats
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //animation mort
        //son mort
    }
}
