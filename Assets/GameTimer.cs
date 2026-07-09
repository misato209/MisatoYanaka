using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public Text timeText;
    public Text resultText;
    public Text scoreText;
    public Text bulletText;
    public Text bonusText;

    public GameObject settingPanel;
    public GameObject titleBackPanel;
    public GameObject resultBackPanel;
    public Counter counter;

    public Button clear10Button;
    public Button clear15Button;
    public Button clear20Button;

    public Button time30Button;
    public Button time45Button;
    public Button time60Button;

    public Button bullet20Button;
    public Button bullet50Button;
    public Button bullet100Button;

    public Button goldOnButton;
    public Button goldOffButton;

    public Color normalColor = Color.white;
    
    public Color clear10Color = Color.yellow;
    public Color clear15Color = Color.yellow;
    public Color clear20Color = Color.yellow;

    public Color time30Color = Color.cyan;
    public Color time45Color = Color.cyan;
    public Color time60Color = Color.cyan;

    public Color bullet20Color = Color.green;
    public Color bullet50Color = Color.green;
    public Color bullet100Color = Color.green;

    public Color goldOnColor = new Color(1.0f, 0.7f, 0.0f);
    public Color goldOffColor = Color.gray;

    public static bool isGameEnded = false;
    public static bool isGameStarted = false;
    public static bool useGoldTarget = true;

    private float timeLeft;

    private int selectedClearScore = 15;
    private float selectedTimeLimit = 30.0f;
    private int selectedBulletCount = 20;
    private bool selectedGoldTarget = true;

    private bool settingOpened = false;


    void Start()
    {
        isGameEnded = false;
        isGameStarted = false;

        selectedClearScore = 15;
        selectedTimeLimit = 30.0f;
        selectedBulletCount = 20;
        selectedGoldTarget = true;
        useGoldTarget = selectedGoldTarget;

        timeText.text = "Time: --";
        resultText.text = "<size=80><color=#FF6A00>Shooting Game</color></size>\n<size=40><color=#000000>Press Enter</color></size>";

        scoreText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        bulletText.gameObject.SetActive(false);
        bonusText.gameObject.SetActive(false);

        settingPanel.SetActive(false);
        titleBackPanel.SetActive(true);
        resultBackPanel.SetActive(false);

        settingOpened = false;

        UpdateButtonColors();
    }

    void Update()
    {
        //終了後，リスタート
        if (isGameEnded)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            return;
        }

        //タイトル画面
        if (!isGameStarted && !isGameEnded && !settingOpened)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                settingOpened = true;
                resultText.text = "";
                settingPanel.SetActive(true);
                titleBackPanel.SetActive(false);
                UpdateButtonColors();
            }

            return;
        }


        //設定画面
        if (!isGameStarted || isGameEnded)
        {
            return;
        }

        //ゲーム中
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            timeLeft = 0;
        }

        timeText.text = "Time: " + Mathf.Ceil(timeLeft).ToString();

        if (timeLeft <= 10)
        {
            bool blink = Mathf.FloorToInt(timeLeft * 6) % 2 == 0;

            if (blink)
            {
                timeText.color = Color.red;
            }
            else
            {
                timeText.color = Color.black;
            }
        }
        else
        {
            timeText.color = Color.black;
        }

        //bounus time
        if (timeLeft <= 10 && !isGameEnded)
        {
            bool blinkBonus = Mathf.FloorToInt(Time.time * 2) % 2 == 0;
            bonusText.gameObject.SetActive(blinkBonus);
        }
        else
        {
            bonusText.gameObject.SetActive(false);
        }

        if (timeLeft <= 0)
        {
            TimeUp();
        }
    }

    public void SelectClearScore(int score)
    {
        selectedClearScore = score;
        UpdateButtonColors();
    }

    public void SelectTimeLimit(float time)
    {
        selectedTimeLimit = time;
        UpdateButtonColors();
    }

    public void SelectBulletCount(int bullet)
    {
        selectedBulletCount = bullet;
        UpdateButtonColors();
    }

    public void SelectGoldTarget(bool useGold)
    {
        selectedGoldTarget = useGold;
        UpdateButtonColors();
    }

    public void StartGame()
    {
        timeLeft = selectedTimeLimit;

        isGameStarted = true;
        isGameEnded = false;
        useGoldTarget = selectedGoldTarget;

        settingPanel.SetActive(false);
        titleBackPanel.SetActive(false);
        resultBackPanel.SetActive(false);

        scoreText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        bulletText.gameObject.SetActive(true);
        bonusText.gameObject.SetActive(false);
        bonusText.text = "BONUS TIME!";

        timeText.color = Color.black;

        resultText.text = "";
        timeText.text = "Time: " + Mathf.Ceil(timeLeft).ToString();

        counter.SetGameSetting(selectedClearScore, selectedBulletCount);
    }

    private void UpdateButtonColors()
    {

        SetButtonColor(clear10Button, selectedClearScore == 10, clear10Color);
        SetButtonColor(clear15Button, selectedClearScore == 15, clear15Color);
        SetButtonColor(clear20Button, selectedClearScore == 20, clear20Color);

        SetButtonColor(time30Button, selectedTimeLimit == 30.0f, time30Color);
        SetButtonColor(time45Button, selectedTimeLimit == 45.0f, time45Color);
        SetButtonColor(time60Button, selectedTimeLimit == 60.0f, time60Color);

        SetButtonColor(bullet20Button, selectedBulletCount == 20, bullet20Color);
        SetButtonColor(bullet50Button, selectedBulletCount == 50, bullet50Color);
        SetButtonColor(bullet100Button, selectedBulletCount == 100, bullet100Color);

        SetButtonColor(goldOnButton, selectedGoldTarget, goldOnColor);
        SetButtonColor(goldOffButton, !selectedGoldTarget, goldOffColor);
    }

    private void SetButtonColor(Button button, bool isSelected, Color selectedButtonColor)
    {
        if (button == null)
        {
            return;
        }

        Color color = normalColor;

        if (isSelected)
        {
            color = selectedButtonColor;
        }

        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.pressedColor = color;
        colors.selectedColor = color;
        button.colors = colors;
    }

    public void Clear()
    {
        if (isGameEnded) return;

        isGameEnded = true;
        bonusText.gameObject.SetActive(false);

        resultBackPanel.SetActive(true);

        resultText.text =
            "<size=80><color=#00A000>GAME CLEAR</color></size>\n" +
            "<size=50><color=#000000>Score: " + counter.hitCount + "</color></size>\n" +
            "<size=50><color=#000000>Time Left: " + Mathf.Ceil(timeLeft) + "</color></size>\n" +
            "<size=50><color=#000000>Bullets Left: " + counter.BulletCount + "</color></size>\n" +
            "<size=35><color=#000000>Press R to Restart</color></size>";
    }

    public void TimeUp()
    {
        if (isGameEnded) return;

        isGameEnded = true;
        bonusText.gameObject.SetActive(false);

        resultBackPanel.SetActive(true);

        resultText.text =
            "<size=70><color=#FF0000>GAME OVER</color></size>\n" +
            "<size=70><color=#FF0000>TIME UP</color></size>\n" +
            "<size=50><color=#000000>Score: " + counter.hitCount + "</color></size>\n" +
            "<size=50><color=#000000>Bullets Left: " + counter.BulletCount + "</color></size>\n" +
            "<size=35><color=#000000>Press R to Restart</color></size>";
    }

    public void BulletGameOver()
    {
        if (isGameEnded) return;

        isGameEnded = true;
        bonusText.gameObject.SetActive(false);

        resultBackPanel.SetActive(true);

        resultText.text =
            "<size=70><color=#FF0000>GAME OVER</color></size>\n" +
            "<size=70><color=#FF0000>OUT OF BULLETS</color></size>\n" +
            "<size=50><color=#000000>Score: " + counter.hitCount + "</color></size>\n" +
            "<size=50><color=#000000>Time Left: " + Mathf.Ceil(timeLeft) + "</color></size>\n" +
            "<size=35><color=#000000>Press R to Restart</color></size>";
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }
}