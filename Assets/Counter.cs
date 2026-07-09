using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UIを使うためのライブラリ

public class Counter : MonoBehaviour
{
    public Text count;
    public Text bulletText;

    public int hitCount = 0;
    public int ClearScore = 15;

    public int BulletCount = 20;

    public GameTimer gameTimer;
    
    void Start()
    {
        hitCount = 0;

        UpdateScoreText();
        UpdateBulletText();
    }

    void Update()
    {
        if (!GameTimer.isGameStarted || GameTimer.isGameEnded)
        {
            return;
        }

        if (BulletCount <= 10)
        {
            bool blink = Mathf.FloorToInt(Time.time * 6) % 2 == 0;

            if (blink)
            {
                bulletText.color = Color.red;
            }
            else
            {
                bulletText.color = Color.black;
            }
        }
        else
        {
        bulletText.color = Color.black;
        }
    }

    public void SetGameSetting(int clearScore, int bulletCount)
    {
        ClearScore = clearScore;
        BulletCount = bulletCount;
        hitCount = 0;

        UpdateScoreText();
        UpdateBulletText();
    }

    public void AddScore(int point)
    {
        if (GameTimer.isGameEnded)
        {
            return;
        }

        hitCount += point;

        if (hitCount < 0)
        {
            hitCount = 0;
        }

        UpdateScoreText();

        if (hitCount >= ClearScore)
        {
            gameTimer.Clear();
        }
    }

    public bool UseBullet()
    {
        if (GameTimer.isGameEnded)
        {
            return false;
        }

        if (BulletCount <= 0)
        {
            gameTimer.BulletGameOver();;
            return false;
        }

        BulletCount -= 1;
        UpdateBulletText();

        return true;
    }

    public void CheckBulletEmpty()
    {
        if (GameTimer.isGameEnded)
        {
            return;
        }

        if (BulletCount <= 0)
        {
            gameTimer.BulletGameOver();
        }
    }

    void UpdateScoreText()
    {
        count.text = "Score: " + hitCount.ToString();
        count.color = Color.black;
    }

    void UpdateBulletText()
    {
        bulletText.text = "Bullets: " + BulletCount.ToString();

        if (BulletCount <= 10)
        {
            bulletText.color = Color.red;
        }
        else
        {
            bulletText.color = Color.black;
        }
}
}