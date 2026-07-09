using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 25.0f;
    Counter counter;

    public void SetCounter(Counter c)
    {
        this.counter = c;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, this.speed);

        // 念のため、counterが入っていなければGameDirectorから取得
        if (counter == null)
        {
            counter = GameObject.Find("GameDirector").GetComponent<Counter>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameTimer.isGameEnded)
        {
            return;
        }

        if (counter == null)
        {
            Debug.LogError("Counterが取得できていません");
            Destroy(this.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Target"))
        {
            TargetController targetController = collision.gameObject.GetComponent<TargetController>();

            if (targetController != null)
            {
                targetController.HitTarget(counter);
            }
            else
            {
                Debug.LogError("TargetにTargetControllerがついていません");
            }

            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("BadTarget"))
        {
            counter.AddScore(-3);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Debug.Log("BadTarget Hit");
        }
        else if (collision.gameObject.CompareTag("GoldTarget"))
        {
            counter.AddScore(3);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Debug.Log("GoldTarget Hit");
        }
        else
        {
            Invoke("destroyBullet", 1);
        }
    }

    private void destroyBullet()
    {
        Destroy(this.gameObject);
    }
}
