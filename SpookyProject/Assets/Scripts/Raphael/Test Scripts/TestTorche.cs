using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class TestTorche : MonoBehaviour
{
    public bool isLit = false, hasBeenBurned = false; //Bool qui détermine si la torche est active ou pas
    public SpriteRenderer spriteRenderer; //Sprite Renderer de la torche
    public Sprite litTorch, unlitTorch; //Possibles Sprites que la torche peux avoir
    [SerializeField] RPP_ButtonsPuzzleManager torchesManager; //Script qui gère les puzzles des torches

    //C'est Pierre qui à fait ça
    public UnityEvent wasUnlit = new UnityEvent();
    List<Collider2D> litObjects = new List<Collider2D>();

    //flash manager à mettre en enfant et sur layer flashable
    public JUB_FlashManager flashManager;

    private void Start()
    {
        torchesManager = GetComponentInParent<RPP_ButtonsPuzzleManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashManager = GetComponent<JUB_FlashManager>();
        //wasUnlit.AddListener(UnlightEverything);

        if (!isLit)
        {
            spriteRenderer.sprite = unlitTorch;
        }
        else
        {
            spriteRenderer.sprite = litTorch;
        }
    }
    void Update()
    {
        if (flashManager.burned && !hasBeenBurned)
        {
            hasBeenBurned = true;
            LitTorch();
        }
    }

    void LitTorch()
    {
        //GetComponent<CircleCollider2D>().radius = 2;
        isLit = true;
        spriteRenderer.sprite = litTorch;
        torchesManager.buttonsActive++;
        Debug.Log("torche allumée");
    }

    void UnlitTorch()
    {
        spriteRenderer.sprite = unlitTorch;
        isLit = false;
        wasUnlit.Invoke();
    }

    //Cette méthode fau=it que quand la torche s'éteint lorsqu'un object est en train d'intéragir avec, cet objet s'étein aussi
    /*void UnlightEverything()
    {
        GetComponent<CircleCollider2D>().radius = 0;
        foreach (Collider2D col in litObjects)
        {
            col.GetComponent<RPP_InvisibleInkScript>().sprite.enabled = false;
        }
        litObjects.Clear();
    }

    //Contrairement au flash qui dit à l'object détécté à s'allumer, c'est la torche elle même qui vas révéler les objets invisibles
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLit)
        {
            litObjects.Add(collision);
            if (collision.GetComponent<RPP_InvisibleInkScript>())
            {
                collision.GetComponent<RPP_InvisibleInkScript>().sprite.enabled = true;
            }
        }
    }

    //La torche étein l'object en contact lorsqu'il sors de la zone iluminé
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isLit)
        {
            litObjects.Remove(collision);
            if (collision.GetComponent<RPP_InvisibleInkScript>())
            {
                collision.GetComponent<RPP_InvisibleInkScript>().sprite.enabled = false;
            }
        }
    }*/
}
