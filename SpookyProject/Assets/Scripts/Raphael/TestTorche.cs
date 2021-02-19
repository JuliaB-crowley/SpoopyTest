using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class TestTorche : MonoBehaviour
{
    public Material torcheMaterial; // Je modifie le material juste pour avoir du feedback de test, pas besoin de le maintenir
    public bool isLit = false; //Bool qui d�termine si la torche est active ou pas

    //C'est Pierre qui � fait �a
    public UnityEvent wasUnlit = new UnityEvent();
    List<Collider2D> litObjects = new List<Collider2D>();

    //flash manager � mettre en enfant et sur layer flashable
    public JUB_FlashManager flashManager;

    private void Start()
    {
        flashManager = GetComponentInChildren<JUB_FlashManager>();
        wasUnlit.AddListener(UnlightEverything);
        torcheMaterial.color = Color.black;
    }
    void Update()
    {
        if (flashManager.burned)
        {
            LitTorch();
        }
    }

    void LitTorch()
    {
        torcheMaterial.color = Color.red;
        GetComponent<CircleCollider2D>().radius = 2;
        isLit = true;
        Debug.Log("torche allum�e ?");
    }

    void UnlitTorch()
    {
        torcheMaterial.color = Color.black;
        isLit = false;
        wasUnlit.Invoke();
    }

    //Cette m�thode fau=it que quand la torche s'�teint lorsqu'un object est en train d'int�ragir avec, cet objet s'�tein aussi
    void UnlightEverything()
    {
        GetComponent<CircleCollider2D>().radius = 0;
        foreach (Collider2D col in litObjects)
        {
            col.GetComponent<TestInvisibleInk>().mesh.enabled = false;
        }
        litObjects.Clear();
    }

    //Contrairement au flash qui dit � l'object d�t�ct� � s'allumer, c'est la torche elle m�me qui vas r�v�ler les objets invisibles
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLit)
        {
            litObjects.Add(collision);
            if (collision.GetComponent<TestInvisibleInk>())
            {
                collision.GetComponent<TestInvisibleInk>().mesh.enabled = true;
            }
        }
    }

    //La torche �tein l'object en contact lorsqu'il sors de la zone ilumin�
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isLit)
        {
            litObjects.Remove(collision);
            if (collision.GetComponent<TestInvisibleInk>())
            {
                collision.GetComponent<TestInvisibleInk>().mesh.enabled = false;
            }
        }
    }
}
