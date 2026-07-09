using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTargetControl : MonoBehaviour
{
    private float time;

    public float destroyTime = 2.0f;

    void Start()
    {
        time = 0.0f;
    }

    void Update()
    {
        if (GameTimer.isGameEnded)
        {
            return;
        }

        time += Time.deltaTime;

        if (time >= destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}