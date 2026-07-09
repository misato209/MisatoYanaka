using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private float time;
    private bool canHit = true;

    public float moveInterval = 6.0f;

    public float minX = -8.0f;
    public float maxX = 8.0f;
    public float minY = 0.5f;
    public float maxY = 2.0f;
    public float minZ = 0.0f;
    public float maxZ = 8.0f;

    void Start()
    {
        time = 0.0f;
    }

    void Update()
    {
        if (GameTimer.isGameEnded)
        {
            Destroy(this.gameObject);
            return;
        }

        time += Time.deltaTime;

        if (time >= moveInterval)
        {
            MoveToRandomPosition();
            time = 0.0f;
        }
    }

    public void HitTarget(Counter counter)
    {
        if (!canHit)
        {
            return;
        }

        canHit = false;

        counter.AddScore(1);

        MoveToRandomPosition();
        time = 0.0f;

        Invoke("EnableHit", 0.1f);
    }

    private void MoveToRandomPosition()
    {
        float positionX = Random.Range(minX, maxX);
        float positionY = Random.Range(minY, maxY);
        float positionZ = Random.Range(minZ, maxZ);

        transform.position = new Vector3(positionX, positionY, positionZ);
    }

    private void EnableHit()
    {
        canHit = true;
    }
}