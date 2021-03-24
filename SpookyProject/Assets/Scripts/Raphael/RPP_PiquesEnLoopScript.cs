using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPP_PiquesEnLoopScript : MonoBehaviour
{
    [SerializeField] Material loopTrapMaterial;
    [SerializeField] BoxCollider2D trapCollider;
    [SerializeField] JUB_HUDManager hudManager;
    [SerializeField] float timeTilNextDmg = 1f, activeTrapTime = 0.2f;
    [SerializeField] int damage = 3;
    bool trapIsActive = false;

    private void Start()
    {
        trapCollider = this.GetComponent<BoxCollider2D>();
        trapCollider.enabled = false;
        loopTrapMaterial.color = Color.white;
        hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<JUB_HUDManager>();
    }

    private void Update()
    {
        if (!trapIsActive)
        {
            StartCoroutine(DoDamage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Do Recoil
            hudManager.currentLife -= damage;
        }
    }

    IEnumerator DoDamage()
    {
        trapIsActive = true;
        loopTrapMaterial.color = Color.red;
        trapCollider.enabled = true;
        yield return new WaitForSeconds(activeTrapTime);
        loopTrapMaterial.color = Color.white;
        trapCollider.enabled = false;
        yield return new WaitForSeconds(timeTilNextDmg);
        trapIsActive = false;
    }
}
