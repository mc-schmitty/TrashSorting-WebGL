using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptical : MonoBehaviour
{
    // Yeah I just need it to hold an int kinda stupid i know
    public int type;
    public bool ignoreCanScore = false;
    [SerializeField] Scorer score;

    private void Start()
    {
        score = FindObjectOfType<Scorer>(); ;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Collison at" + transform.position);
        if (collision.CompareTag("Grabbable"))
        {
            Trash t = collision.GetComponent<Trash>();

            if (!t.canScore && !ignoreCanScore)
                return;
            
            // Score events
            if (t.type == this.type)
            {
                print("yay good");
                score.givePoint(1);
            }
            else
            {
                print("no bad :(((");
                score.givePoint(-1);
            }

            t.StartEnterBin(transform.position);
            //collision.gameObject.SetActive(false);
            //Destroy(this.gameObject);
         
        }
    }

    private void ScoreObject()
    {

    }
}
