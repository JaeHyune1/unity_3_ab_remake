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

        //reset Story Models ��ư ����
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

    public Stats stats;
    public GAMESTATE currentState;
    public int currentStoryIndex = 1;
    public StoryModel[] storyModels;

#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryModel>(""); //���ҽ� ���� �Ʒ� ��� ���丮 ���� �ҷ��´�.
    }
#endif

    public void StoryShow(int Number)
    {
        StoryModel temStoryModels = FindStoryModel(number);

        //StorySystem.Instace,currentStoryModel = tempStoryModels;
        //StorySystem.Instance.Co.ShowText();
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

        tempStoryModels = storyModelsList[Random.Range(0, storyModelsList.Count)]; //����Ʈ���� �������� �ϳ� ����
        currentStoryIndex = tempStoryModels.storyNumber;
        return tempStoryModels;
    }
}