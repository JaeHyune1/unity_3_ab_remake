using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "NewStory" , menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : MonoBehaviour
{
    public int storyNumber;
    public Texture2D mainimage;

    public enum STORYTYPE
    {
        Main,
        Sub,
        SERIAL
    }

    public STORYTYPE storytype;
    public bool storyDone;

    [TextArea(10, 10)]
    public string storyText;

    public Option[] option;    //선택지 배열
    internal readonly object options;

    [System.Serializable]

    public class Option
    {
        public string optionsText;
        public string buttonText;    //선택지 버튼의 이름

        public EventCheck eventCheck;
    }

    public class Result
    {
        public enum ResultType : int
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToShop,
            GoToNextStory,
            GoToRandomStory
        }

        public ResultType resultType;
        public int value;
        public Stats stats;
    }

    [System.Serializable]
    
    public class EventCheck
    {
        public int checkvalue;
        public enum EventType : int
        {
            NONE,
            GoToBattle,
            CheckSTR,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA
        }

        public EventType eventType;
        public Result[] sucessResult;
        public Result[] failResult;
    }
    
}
