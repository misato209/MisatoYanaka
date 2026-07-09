using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    private float positionX;
    private float positionY;
    private float positionZ;

    public GameObject target;

    private GameObject currentTarget;

    private void TargetGenerate()
    {
        if (currentTarget != null)
        {
            return;
        }

        positionX = Random.Range(-8.0f, 8.0f);
        positionY = Random.Range(0.5f, 2.0f);
        positionZ = Random.Range(0.0f, 8.0f);

        Vector3 targetPosition = new Vector3(positionX, positionY, positionZ);

        currentTarget = Instantiate(target, targetPosition, Quaternion.identity);
    }

    void Start()
    {
        currentTarget = null;
    }

    void Update()
    {
        if (GameTimer.isGameEnded)
        {
            if (currentTarget != null)
            {
                Destroy(currentTarget);
            }

            return;
        }

        if (!GameTimer.isGameStarted)
        {
            return;
        }

        if (currentTarget == null)
        {
            TargetGenerate();
        }
    }
}