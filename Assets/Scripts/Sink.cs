using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grabbable") && collision.attachedRigidbody.isKinematic)
        {
            
            Trash t = collision.gameObject.GetComponent<Trash>();
            if(t.type == 0)
            {
                t.RandomNewType();
                ps.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grabbable"))
        {
            ps.Stop();
        }
    }
}
