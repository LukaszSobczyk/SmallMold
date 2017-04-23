using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScoreSystem : MonoBehaviour {

    int collectedSeeds = 100;
    int playerLevel = 1;
    public int[] playerLevelScore;
    public int seedLose = 3;
    public void AddSeed()
    {
        collectedSeeds++;
        SetLevel();

        Debug.Log(playerLevel);
    }

    private void Update()
    {
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
