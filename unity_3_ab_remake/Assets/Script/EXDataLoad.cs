using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXDataLoad : MonoBehaviour
{
    public ExGameData gameData;
    private void Start()
    {
        Debug.Log("Game Name : " + gameData.gameName);
        Debug.Log("Game Score : " + gameData.gameScore);
        Debug.Log("is Game Active : " + gameData.isGameActive);
    }
}
