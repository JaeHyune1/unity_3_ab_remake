using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

[System.Serializable]

public class PlayerData
{
    public string playerName;
    public int playerLevel;
    public List<string> items = new List<string>();
}

public class ExXMLDataManager : MonoBehaviour
{
    string filePath;
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/playerData.xml";
        Debug.Log(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerData playerData = new PlayerData();       //플레이어 데이터 생성하여
            playerData.playerName = "플레이어 1";           //데이터를 정해준다.
            playerData.playerLevel = 1;
            playerData.items.Add("돌1");
            playerData.items.Add("바위");
            SaveData(playerData);                           //해당 내용을 XML 파일로 저장한다.
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData playerData = new PlayerData();      //받을 플레이어 데이터 객체

            playerData = LoadData();                       //파일에서 로딩한다.

            Debug.Log(playerData.playerName);              //로드된 데이터를 출력한다.
            Debug.Log(playerData.playerLevel);
            for(int i = 0; i < playerData.items.Count; i++)
            {
                Debug.Log(playerData.items[i]);
            }
        }
    }

    void SaveData(PlayerData data)
    {
        XmlSerializer serialzer = new XmlSerializer(typeof(PlayerData)); //XML 직렬화
        FileStream stream = new FileStream(filePath, FileMode.Create); //파일 생성 모드 설정
        serialzer.Serialize(stream, data);                             //파일에 데이터를 저장한다.
        stream.Close();                                            //반드시 close 해줘야한다.
    }

    PlayerData LoadData()
    {
        if(File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
            FileStream stream = new FileStream(filePath, FileMode.Open);
            PlayerData data = (PlayerData)serializer.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
