using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExPlayerPrefabData : MonoBehaviour
{
    public int Score;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SavaData(Score);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(LoadData());
        }
    }

    void SavaData(int score)
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
        Debug.Log("���� �Ǿ����ϴ�.");
    }

    int LoadData()
    {
        return PlayerPrefs.GetInt("Score");
    }
}
