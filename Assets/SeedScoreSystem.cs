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

    public void AddSeed()
    {
        collectedSeeds++;
        SetLevel();

        Debug.Log(playerLevel);
    }

    void Start()
    {
        ActualizeScoreText();
    }
    void ActualizeScoreText()
    {
        if(scoreText!=null)
            scoreText.GetComponent<Text>().text = collectedSeeds.ToString();
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
