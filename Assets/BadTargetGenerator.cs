using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTargetGenerator : MonoBehaviour
{
    private float positionX;
    private float positionY;
    private float positionZ;
    private float time;

    public float badTargetSpawnTime = 5.0f;
    public GameObject badTarget;

    private GameObject currentBadTarget;

    private void BadTargetGenerate()
    {
        if (currentBadTarget != null)
        {
            return;
        }

        positionX = Random.Range(-8.0f, 8.0f);
        positionY = Random.Range(0.5f, 2.0f);
        positionZ = Random.Range(0.0f, 8.0f);

        Vector3 badTargetPosition = new Vector3(positionX, positionY, positionZ);

        currentBadTarget = Instantiate(badTarget, badTargetPosition, Quaternion.identity);
    }

    void Start()
    {
        time = 0.0f;
    }

    void Update()
    {
        if (!GameTimer.isGameStarted || GameTimer.isGameEnded)
        {
            if (GameTimer.isGameEnded && currentBadTarget != null)
            {
                Destroy(currentBadTarget);
                currentBadTarget = null;
            }

            return;
        }

        if (currentBadTarget != null)
        {
            time = 0.0f;
            return;
        }
    

        time += Time.deltaTime;

        if (time >= badTargetSpawnTime)
        {
            time = 0.0f;
            BadTargetGenerate();
        }
    }
}