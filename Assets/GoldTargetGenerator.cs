using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTargetGenerator : MonoBehaviour
{
    private float positionX;
    private float positionY;
    private float positionZ;

    private float moveTimer;
    private float respawnTimer;

    public float appearBeforeEnd = 10.0f; // 残り何秒から出すか
    public float moveInterval = 2.0f;     // 何秒ごとに場所を変えるか
    public float respawnTime = 5.0f;      // 消えてから何秒後に再出現するか

    public GameObject goldTarget;
    public GameTimer gameTimer;

    private GameObject currentGoldTarget;
    private bool lastPhaseStarted = false;

    private Vector3 RandomPosition()
    {
        positionX = Random.Range(-8.0f, 8.0f);
        positionY = Random.Range(0.5f, 2.0f);
        positionZ = Random.Range(0.0f, 8.0f);

        return new Vector3(positionX, positionY, positionZ);
    }

    private void GenerateGoldTarget()
    {
        currentGoldTarget = Instantiate(goldTarget, RandomPosition(), Quaternion.identity);
        moveTimer = 0.0f;
        respawnTimer = 0.0f;
    }

    void Start()
    {
        moveTimer = 0.0f;
        respawnTimer = 0.0f;
        lastPhaseStarted = false;
    }

    void Update()
    {
        // ゲーム終了後はGoldTargetを消す
        if (!GameTimer.isGameStarted || GameTimer.isGameEnded || !GameTimer.useGoldTarget)
        {
            if (currentGoldTarget != null)
            {
                Destroy(currentGoldTarget);
                currentGoldTarget = null;
            }

            return;
        }

        // 残り10秒より前は何もしない
        if (gameTimer.GetTimeLeft() > appearBeforeEnd)
        {
            return;
        }

        // 残り10秒になった瞬間、最初のGoldTargetを出す
        if (!lastPhaseStarted)
        {
            lastPhaseStarted = true;
            GenerateGoldTarget();
            return;
        }

        // GoldTargetが存在している間は2秒ごとに移動
        if (currentGoldTarget != null)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer >= moveInterval)
            {
                moveTimer = 0.0f;
                currentGoldTarget.transform.position = RandomPosition();
            }
        }
        // GoldTargetが撃たれて消えた後、5秒後に再出現
        else
        {
            respawnTimer += Time.deltaTime;

            if (respawnTimer >= respawnTime)
            {
                GenerateGoldTarget();
            }
        }
    }
}