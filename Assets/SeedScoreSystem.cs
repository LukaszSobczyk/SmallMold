using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScoreSystem : MonoBehaviour {

    int collectedSeeds = 0;
    public void AddSeed()
    {
        collectedSeeds++;
        Debug.Log(collectedSeeds);
    }
}
