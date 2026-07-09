using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTargetControl : MonoBehaviour
{
    private float time;

    public float destroyTime = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}

  