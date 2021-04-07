using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlah : MonoBehaviour
{
    //C'est mon test de flash que j'ai fait. Le pauvre, il ne sait pas qu'il va rester à jamais dans le néant, jamais vraiment utilisé...
    public Transform flashTransform;
    public float flashRange;
    [SerializeField] LayerMask affectedLayers;
    public bool hasFlashed = false;
    public Vector2 flashPosition2D;

    void Start()
    {
        flashTransform = this.GetComponent<Transform>();
    }

    void Update()
    {
        flashPosition2D = new Vector2(flashTransform.position.x, flashTransform.position.y);

        
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(flashPosition2D, flashRange);
        if (hasFlashed)
        {
            foreach (Collider2D col in collider2Ds)
            {
                /*if (col.GetComponent<TestInvisibleInk>())
                {
                    //mis en commentaire suite à l'implémentation de mon flash
                    //col.GetComponent<TestInvisibleInk>().hasBeenFlashed = true;
                }*/
            }
            hasFlashed = false;
        }

        /*Collider[] objects = Physics.OverlapSphere(flashTransform.position, flashRange, affectedLayers);
        if (hasFlashed)
        {
            foreach (Collider obj in objects)
            {
                if (obj.GetComponent<TestInvisibleInk>())
                {
                    obj.GetComponent<TestInvisibleInk>().hasBeenFlashed = true;
                }
            }
            hasFlashed = false;
        }*/        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(flashTransform.position, flashRange);
    }
}
