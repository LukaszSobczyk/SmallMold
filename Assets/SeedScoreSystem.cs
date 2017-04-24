using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedScoreSystem : MonoBehaviour {

    public int collectedSeeds = 100;
    int playerLevel = 1;
    public int[] playerLevelScore;
    public int seedLose = 3;
    public GameObject scoreText;
    public GameObject mainPrgressBar;
    public void AddSeed()
    {
        collectedSeeds++;
        SetLevel();
        ActualizeScoreText();
        ActualizeMainBar();
        Debug.Log(playerLevel);
    }

    void Start()
    {
        ActualizeScoreText();
        ActualizeMainBar();
    }
    void ActualizeScoreText()
    {
        if(scoreText!=null)
            scoreText.GetComponent<Text>().text = collectedSeeds.ToString();
    }
    void ActualizeMainBar()
    {
        if (mainPrgressBar != null)
        {
            float max = playerLevelScore[playerLevelScore.Length - 1];
            float min = 0.0f;
            if(collectedSeeds >max)
            {
                mainPrgressBar.GetComponent<Image>().fillAmount = 1.0f;
            }
            for (int i = playerLevelScore.Length-1; i > -1; i--)
            {
                max = playerLevelScore[i];
                if(i!=0)
                {
                    min = playerLevelScore[i - 1];
                }
                {
                    min = 0.0f;
                }
                if (min <= collectedSeeds && collectedSeeds < max)
                {
                    mainPrgressBar.GetComponent<Image>().fillAmount = (collectedSeeds - min) / (max - min);
                    break;
                }
            }
        }
    }
    private void SetLevel()
    {
        if (playerLevelScore[0] - 1 < collectedSeeds && playerLevelScore[1] >= collectedSeeds)
        {
            playerLevel = 2;
        }
        if (playerLevelScore[1] - 1 < collectedSeeds && playerLevelScore[2] >= collectedSeeds)
        {
            playerLevel = 3;
        }
        if (playerLevelScore[2] - 1 < collectedSeeds)
        {
            playerLevel = 4;
        }
    }
}
