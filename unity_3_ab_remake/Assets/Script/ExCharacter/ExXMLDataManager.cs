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
            PlayerData playerData = new PlayerData();       //�÷��̾� ������ �����Ͽ�
            playerData.playerName = "�÷��̾� 1";           //�����͸� �����ش�.
            playerData.playerLevel = 1;
            playerData.items.Add("��1");
            playerData.items.Add("����");
            SaveData(playerData);                           //�ش� ������ XML ���Ϸ� �����Ѵ�.
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData playerData = new PlayerData();      //���� �÷��̾� ������ ��ü

            playerData = LoadData();                       //���Ͽ��� �ε��Ѵ�.

            Debug.Log(playerData.playerName);              //�ε�� �����͸� ����Ѵ�.
            Debug.Log(playerData.playerLevel);
            for(int i = 0; i < playerData.items.Count; i++)
            {
                Debug.Log(playerData.items[i]);
            }
        }
    }

    void SaveData(PlayerData data)
    {
        XmlSerializer serialzer = new XmlSerializer(typeof(PlayerData)); //XML ����ȭ
        FileStream stream = new FileStream(filePath, FileMode.Create); //���� ���� ��� ����
        serialzer.Serialize(stream, data);                             //���Ͽ� �����͸� �����Ѵ�.
        stream.Close();                                            //�ݵ�� close ������Ѵ�.
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
