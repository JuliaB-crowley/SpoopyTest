using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using character;

public class JUB_Flash : MonoBehaviour
{
    JUB_Maeve maeve;
    Controller controller;
    public Transform up, down, left, right;
    public LayerMask flashableObjects;
    public float burnRange, flashRange, flashTime;
    public bool flashWasObtained;
    Collider2D[] flashableInRange, burnableInRange;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = new Controller();
        controller.Enable();
        maeve = GetComponent<JUB_Maeve>();

        controller.MainController.Flash.performed += ctx => Flash();
    }

    // Update is called once per frame
    public void Flash()
    {
        if (flashWasObtained)
        {
            maeve.isFlashing = true;
            Transform flashPoint = right;

            switch (maeve.dirAngle)
            {
                case DirectionAngle.Est:
                    flashPoint = right;
                    break;

                case DirectionAngle.North:
                    flashPoint = up;
                    break;

                case DirectionAngle.South:
                    flashPoint = down;
                    break;

                case DirectionAngle.West:
                    flashPoint = left;
                    break;
            }

            flashableInRange = Physics2D.OverlapCircleAll(flashPoint.position, flashRange, flashableObjects);
            burnableInRange = Physics2D.OverlapCircleAll(flashPoint.position, burnRange, flashableObjects);

            foreach (Collider2D flashable in flashableInRange)
            {
                if (flashable.GetComponent<JUB_FlashManager>().flashed == false)
                {
                    flashable.GetComponent<JUB_FlashManager>().flashed = true;
                    flashable.GetComponent<JUB_FlashManager>().FlashEnd();
                }
            }

            foreach (Collider2D burnable in burnableInRange)
            {
                if (burnable.GetComponent<JUB_FlashManager>().burned == false)
                {
                    burnable.GetComponent<JUB_FlashManager>().burned = true;
                    burnable.GetComponent<JUB_FlashManager>().BurnEnd();
                }
            }

            StartCoroutine(EndFlash());

            Debug.Log("flash was performed");
        }
    }

    IEnumerator EndFlash()
    {
        yield return new WaitForSeconds(flashTime);
        maeve.isFlashing = false;
        Debug.Log("fin de flash");
    }

}
