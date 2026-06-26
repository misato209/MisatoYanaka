using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    public float speed = 1f;
    public float leftLimit = -5f;
    public float rightLimit = 5f;

    private int direction = 1; // 1 for right, -1 for left

    private void Move()
    {
        
        transform.Translate(Vector3.right * this.speed * this.direction * Time.deltaTime);
        
        if (transform.position.x >= this.rightLimit)
        {
            this.direction = -1;
        }
        else if (transform.position.x <= this.leftLimit)
        {
            this.direction = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("WallControl Start");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
