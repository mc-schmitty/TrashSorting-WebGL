using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMove : MonoBehaviour
{
    public float conveyorSpeed = 0;
    public const float maxSpeed = 15;
    public bool accelerate = false;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Use a physics-less move right by some amount, then use physics to move belt back to original position
        // Moves any rigidbodies on the belt with it by just using Unity's physics (kinda hacky)
        Vector2 pos = rb.position;
        rb.position += Vector2.right * conveyorSpeed * Time.fixedDeltaTime;
        rb.MovePosition(pos);

        conveyorSpeed = Mathf.Min(maxSpeed, conveyorSpeed + Time.fixedDeltaTime * (accelerate?1:0));
        
    }

    // Turn on at x speed for y seconds
    public void ActivateTimed(float newSpeed, float secondsOn)
    {
        conveyorSpeed = newSpeed;
        StartCoroutine(WaitForSecondsCoroutine(secondsOn));
    }

    IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        conveyorSpeed = 0;
    }
}
