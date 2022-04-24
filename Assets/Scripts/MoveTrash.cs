using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrash : MonoBehaviour
{
    [SerializeField] float bufferLength = 0.5f;
    [SerializeField] public int maxBufferSize = 30;
    [SerializeField] float inertiaDampener = 200f;
    Queue<Vector3> momentum;

    SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D selectedItem;
    Trash selectedTrash;
    Rigidbody2D mouseRb;
    float mouseBuffer;

    Vector3 oldMousePos;
    Vector3 newMousePos;
    //bool release;

    void Start()
    {
        oldMousePos = Vector3.zero;
        newMousePos = oldMousePos;

        mouseBuffer = 0;
        //release = false;

        mouseRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
        //spriteRenderer.color = Color.white;

        momentum = new Queue<Vector3>();
    }

    void Update()
    {
        if (mouseBuffer > 0)
            mouseBuffer = mouseBuffer - Time.deltaTime;
        //else if(mouseBuffer == 0)
            //spriteRenderer.color = Color.white;
        

        if (Input.GetButtonDown("Fire1"))
        {
            mouseBuffer = bufferLength;
            //spriteRenderer.color = new Color(149f, 209f, 33f, 255f);
            
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            ReleaseHeldItem();
        }

        // Check if held item is no longer valid
        if(selectedTrash != null && selectedTrash.canGrab == false)
        {
            ReleaseHeldItem();
        }

    }

    private void FixedUpdate()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //oldMousePos = newMousePos;
        newMousePos = worldPos; //Input.mousePosition;

        mouseRb.MovePosition(worldPos);
        if (selectedItem != null)
        {
            selectedItem.MovePosition(worldPos);

            momentum.Enqueue(newMousePos);
            if (momentum.Count == maxBufferSize)
            {
                momentum.Dequeue();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (mouseBuffer > 0 && collision.CompareTag("Grabbable"))
        {
            Trash t = collision.GetComponent<Trash>();
            if (t.canGrab)
            {
                selectedItem = collision.attachedRigidbody;
                selectedItem.isKinematic = true;
                selectedItem.velocity = Vector2.zero;
                mouseBuffer = 0;
                t.canScore = true;
                selectedTrash = t;
            }
        }
    }

    // Release item held in cursor
    private void ReleaseHeldItem()
    {
        if (selectedItem != null)
        {
            selectedItem.isKinematic = false;
            //test = newMousePos - oldMousePos;
            Vector2 move = newMousePos - momentum.Dequeue();
            move *= inertiaDampener;
            //Debug.Log(move.magnitude);
            selectedItem.velocity = move;
            selectedItem = null;
            momentum.Clear();
            selectedTrash = null;
        }
    }

}
