using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum StoryType    //스토리 타입
    {
        MAIN,
        SUB,
        SERIAL
    }

    public enum EventType    //이벤트 발생시 체크
    {
        NONE,
        GOTOBATTLE = 100,
        CheckSTR = 1000,
    }

    public enum ResultType    //이벤트 결과 열기
    {
        AddExperience,
        GoToNextStory,
        GoToRandoemStory
    }

    [System.Serializable]

    public class Stats
    {
        public int hpPoint;
        public int spPoint;

        public int currentHpPoint;
        public int currentSpPoint;
        public int currentXpPoint;

        public int strength;
        public int dexterity;
        public int consitution;
        public int Intelligence;
        public int wisdom;
        public int charisma;
    }
}
