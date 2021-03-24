using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_PiquesConstantsScript : MonoBehaviour
{
    [SerializeField] JUB_HUDManager hudManager;
    [SerializeField] float timeTilNextDmg = 1f;
    [SerializeField] int damage = 3;
    bool hasDoneDamage = false;

    private void Start()
    {
        hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<JUB_HUDManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDoneDamage)
        {
            // Do Recoil
            StartCoroutine(DoDamage());
        }
    }

    IEnumerator DoDamage()
    {
        hasDoneDamage = true;
        hudManager.currentLife -= damage;
        yield return new WaitForSeconds(timeTilNextDmg);
        hasDoneDamage = false;
    }
}
