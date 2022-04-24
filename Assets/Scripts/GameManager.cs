using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    ConveyorMove conveyor;
    [SerializeField]
    Spawner spawner;
    [SerializeField]
    Text msgBoard;

    public float conveyorPushSpeed = 5f;
    public float conveyorPushTime = 2f;
    public float nextTrashWaitTime = 3f;
    WaitForSeconds delay;

    void Start()
    {
        if (conveyor == null || spawner == null || msgBoard == null)
            this.enabled = false;

        delay = new WaitForSeconds(conveyorPushTime);
    }

    private void Awake()
    {
        spawner.spawnRandom = false;
        conveyor.accelerate = false;
        StartCoroutine(CountdownToSpawn(nextTrashWaitTime));
    }

    IEnumerator CountdownToSpawn(float secondsTilSpawn)
    {
        float count = secondsTilSpawn;
        while(count > 0)
        {
            msgBoard.text = "NEXT ITEM: " + count;
            count -= Time.deltaTime;
            yield return null;
        }
        spawner.SpawnControlled();
        conveyor.ActivateTimed(conveyorPushSpeed, conveyorPushTime);
        StartCoroutine(WaitForDispense());
    }

    IEnumerator WaitForDispense()
    {
        //float count = secondsMovingSpawn;
        msgBoard.text = "DISPENSING";
        yield return delay;

        StartCoroutine(CountdownToSpawn(nextTrashWaitTime));
    }
}
