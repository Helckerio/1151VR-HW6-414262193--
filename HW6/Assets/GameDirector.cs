using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [Header("UI 元件")]
    public Text timerText;
    public Text scoreText;

    [Header("控制對象")]
    public ItemFactory factory;
    public BasketController basket;

    [Header("遊戲設定")]
    public float timeLimit = 60.0f;

    private float remainingTime;
    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        remainingTime = timeLimit;
        UpdateUI();
    }

    public void GetApple()
    {
        score += 100;
        UpdateUI();
    }

    public void GetBomb()
    {
        score /= 2;
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isGameOver = true;

            if (factory != null)
            {
                factory.isSpawning = false;
            }
            if (basket != null)
            {
                basket.canMove = false;
            }
        }
        else if (factory != null)
        {
            // 三階段關卡難度設計：隨剩餘時間遞減提高刺激感
            if (remainingTime > 40f)
            {
                // 階段一（60~40秒）：暖身期
                factory.SetParameter(1.0f, 2.5f, 0.20f);
            }
            else if (remainingTime > 20f)
            {
                // 階段二（40~20秒）：加速期
                factory.SetParameter(0.7f, 4.0f, 0.35f);
            }
            else
            {
                // 階段三（20~0秒）：衝刺期
                factory.SetParameter(0.45f, 5.5f, 0.50f);
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + remainingTime.ToString("F1");
        }
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}