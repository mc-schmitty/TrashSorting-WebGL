using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int type;
    public bool canScore;
    public bool canGrab;
    Rigidbody2D rb;

    [SerializeField]
    PhysicsMaterial2D[] physList;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get random type
        type = Random.Range(0, 4);

        // Show particles of type
        SpriteRenderer spR = GetComponent<SpriteRenderer>();
        ParticleSystem ps = GetComponent<ParticleSystem>();
        Color colour = GetColor(type);
        spR.color = colour;
        ParticleSystem.MainModule m = ps.main;
        m.startColor = colour;

        // Assign physics material
        rb.sharedMaterial = physList[type];

        // Set startig vars
        canGrab = true;
        canScore = false;
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Collison at" + transform.position);
        if (collision.CompareTag("Receptical"))
        {
            // Score events
            if (collision.GetComponent<Receptical>().type == this.type)
            {
                print("yay good");
            }
            else 
            { 
                print("no bad :(((");
            }
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }*/

    // Change from type 0 to type 1-3
    public int RandomNewType()
    {
        int newT = Random.Range(1, 4);
        type = newT;

        Color newC = GetColor(newT);
        GetComponent<SpriteRenderer>().color = newC;
        ParticleSystem.MainModule m = GetComponent<ParticleSystem>().main;
        m.startColor = newC;
        rb.sharedMaterial = physList[newT];

        return newT;
    }
    
    public void StartEnterBin(Vector2 targetBinPos)
    {
        canGrab = false;
        StartCoroutine(EnterBinCoroutine(targetBinPos));
    }

    IEnumerator EnterBinCoroutine(Vector2 target)
    {
        Rigidbody2D nrb = rb;
        nrb.isKinematic = true;

        while (Vector2.Distance(nrb.position, target) > 0.1f)
        {
            nrb.MovePosition(Vector2.MoveTowards(nrb.position, target, 0.1f));
            nrb.transform.localScale *= 0.99f;
            yield return null;
        }

        this.gameObject.SetActive(false);
    }

    private Color GetColor(int num)
    {
        switch (num)
        {
            case 0: 
                return Color.black;
            case 1:
                return Color.green;
            case 2:
                return Color.blue;
            case 3:
                //return Color.yellow;
                return new Color(1f, 0.64f, 0);
        }
        return Color.magenta;
    }
}
