using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInvisibleInk : MonoBehaviour
{
    public MeshRenderer mesh; // Je modifie le material juste pour avoir du feedback de test, pas besoin de le maintenir
    public bool hasBeenFlashed = false; //C'est en tournant ce bool en 'True' que le Game Object deviens visible
    public float visibleTime = 2f; //Float qui détermine le temps que l'encre reste visible
   
    void Start()
    {
        mesh = this.GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }

   
    void Update()
    {
        if (hasBeenFlashed)
        {
            StartCoroutine(InkAppear());
        }
    }

    //Coroutine qui fait apparaitre les invisible ink pendant une petite période
    IEnumerator InkAppear()
    {
        hasBeenFlashed = false;
        mesh.enabled = true;
        yield return new WaitForSeconds(visibleTime);
        mesh.enabled = false;
    }
}
