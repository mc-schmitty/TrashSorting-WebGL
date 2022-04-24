using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject trash;
    float spawnTimer;
    float spawnInterval = 2;
    public bool spawnRandom = true;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        if (!spawnRandom)
            return;

        spawnTimer -= Time.deltaTime;

        if(spawnTimer < 0)
        {
            int r = Random.Range(0, 10);
            if (r > 8)
                SpawnThreeBurst();
            else if (r > 6)
                SpawnTwoBurst();
            else
                SpawnSingle();
            spawnTimer = spawnInterval + Random.Range(-1.9f, 2f);
        }
    }

    public void SpawnControlled()
    {
        GameObject.Instantiate(trash, transform.position + Vector3.down * 4f, transform.rotation);
    }

    private void SpawnSingle()
    {
        GameObject.Instantiate(trash);
    }

    private void SpawnTwoBurst()
    {
        GameObject obj1 = GameObject.Instantiate(trash, transform.position + Vector3.down * 4f, transform.rotation);
        GameObject obj2 = GameObject.Instantiate(trash, transform.position + Vector3.down * 2.5f, transform.rotation);

        Rigidbody2D rb1 = obj1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = obj2.GetComponent<Rigidbody2D>();

        rb1.AddForce(new Vector2(-500, 200));
        rb2.AddForce(new Vector2(-300, 300));
    }

    private void SpawnThreeBurst()
    {
        GameObject obj1 = GameObject.Instantiate(trash, transform.position + Vector3.up * 1f, transform.rotation);
        GameObject obj2 = GameObject.Instantiate(trash, transform.position + Vector3.up * 0.2f, transform.rotation);
        GameObject obj3 = GameObject.Instantiate(trash, transform.position + Vector3.up * -1f, transform.rotation);

        Rigidbody2D rb1 = obj1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = obj2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = obj3.GetComponent<Rigidbody2D>();

        rb1.AddForce(new Vector2(-400, 400));
        rb2.AddForce(new Vector2(-300, 300));
        rb3.AddForce(new Vector2(-400, 50));

    }
}
