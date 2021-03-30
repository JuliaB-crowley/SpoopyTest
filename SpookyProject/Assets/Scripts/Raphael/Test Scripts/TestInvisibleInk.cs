using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInvisibleInk : MonoBehaviour
{
    public MeshRenderer mesh; // Je modifie le material juste pour avoir du feedback de test, pas besoin de le maintenir
    //public bool hasBeenFlashed = false; //C'est en tournant ce bool en 'True' que le Game Object deviens visible
    public float visibleTime = 2f; //Float qui d�termine le temps que l'encre reste visible

    //flash manager � mettre en enfant et sur layer flashable
    public JUB_FlashManager flashManager;
   
    void Start()
    {
        mesh = this.GetComponent<MeshRenderer>();
        mesh.enabled = false;

        flashManager = GetComponentInChildren<JUB_FlashManager>();
    }

   
    void Update()
    {
        if (flashManager.flashed)
        {
            mesh.enabled = true;
        }
        else
        {
            mesh.enabled = false;
        }
    }
}
