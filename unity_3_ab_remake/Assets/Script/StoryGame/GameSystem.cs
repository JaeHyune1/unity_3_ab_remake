using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameSystem gameSystem = (GameSystem)target;

        //reset Story Models 버튼 생성
        if(GUILayout.Button("Rest Story Modes"))
        {
            gameSystem.ResetStoryModels();
        }
    }
}
#endif
public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public enum GAMESTATE
    {
        STORYSHOW,
        WAITSELECT,
        STORYEND
    }

    public Stats Stats;
    public GAMESTATE currentState;
    public int currentStoryIndex = 1;
    public StoryModel[] storyModels;

    public void Start()
    {
        ChangeState(GAMESTATE.STORYSHOW);
    }



#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryModel>(""); //리소스 폴더 아래 모든 스토리 모델을 불러온다.
    }
#endif

    public void StoryShow(int Number)
    {
        StoryModel temStoryModels = FindStoryModel(number);

        StorySystem.instance.currentStoryModel = tempStoryModels;
        StorySystem.instance.CoShowText();
    }

    StoryModel FindStoryModel(int number)
    {
        StoryModel tempStoryModels = null;
        for(int i = 0; i < storyModels.Length; i++)
        {
            if (storyModels[i].storyNumber == number)
            {
                tempStoryModels = storyModels[i];
                break;
            }
        }

        return tempStoryModels;
    }

    StoryModel RandomStory()
    {
        StoryModel tempStoryModels = null;

        List<StoryModel> storyModelsList = new List<StoryModel>();
        
        for (int i = 0; i < storyModels.Length; i++)
        {
            if (storyModels[i].storyType == StoryModel.STORYTYPE.MAIN)
            {
                storyModelsList.Add(storyModels[i]);
            }
        }

        tempStoryModels = storyModelsList[Random.Range(0, storyModelsList.Count)]; //리스트에서 랜덤으로 하나 선택
        currentStoryIndex = tempStoryModels.storyNumber;
        return tempStoryModels;
    }

    public void ChangeState(GAMESTATE temp)
    {
        currentState = temp;
        if(currentState == GAMESTATE.STORYSHOW)
        {
            StoryShow(currentStoryIndex);
        }
    }

    public void ChangeStats(StoryModel.Result result)
    {
        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
        if (result.stats.spPoint > 0) stats.spPoint += result.stats.spPoint;

        if (result.stats.currentHpPoint > 0) stats.currentHpPoint += result.stats.currentHpPoint;
        if (result.stats.currentHpPoint > 0) stats.currentSpPoint += result.stats.currentSpPoint;
        if (result.stats.currentHpPoint > 0) stats.currentXpPoint += result.stats.currentXpPoint;

        if (result.stats.strength > 0) stats.strength += result.stats.strength;
        if (result.stats.dextreity > 0) stats.dextreity += result.stats.dextreity;
        if (result.stats.consitution > 0) stats.consitution += result.stats.consitution;
        if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
        if (result.stats.Intelligence > 0) stats.Intelligence += result.stats.Intelligence;
        if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;

    }
    public void ApplyChoice(StoryModel.Result result)
    {
        switch(result.resultType)
        {
            case StoryModel.Result.ResultType.ChangeHp:
                stats.currentHpPoint += result.value;
                ChangeStats(result);
                break;

            case StoryModel.Result.ResultType.AddExperience:
                stats.currentXpPoint += result.value;
                ChangeStats(result);
                break;

            case StoryModel.Result.ResultType.GoToNextStory:
                currentStoryIndex += result.value;
                ChangeState(GAMESTATE.STORYSHOW);
                ChangeStats(result);
                break;

            case StoryModel.Result.ResultType.GoToRandomStory:
                RandomStory();
                ChangeState(GAMESTATE.STORYSHOW);
                ChangeStats(result);
                break;
            default:
                Debug.LogError("Unknown type");
                break;

        }
    }

    public void StoryShow(int nymber)
    {
        StoryModel tempStoryModels = FindStoryModel(number);

        //StorySystem.Instace.currentStoryModel = tempStoryModel
        //StoryModel.Intance.CoShowText();

    }

    StoryModel FindStoryModel(int number)
    {
        StoryModel tempStoryModels = null;
        for(int i = 0; i < storyModels.Length; i++)
        {
            if(storyModels[i].storyNumber == number)
            {
                tempStoryModels = storyModels[i];
                break;
            }
        }
        return tempStoryModels;
    }
}