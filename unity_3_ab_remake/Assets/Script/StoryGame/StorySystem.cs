using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StorySystem : MonoBehaviour
{
    public static StorySystem instance;

    public StoryModel currentStoryModel;

    public enum TextSystem
    {
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;
    public string fullText;
    private String currentText = "";

    public Text textComponent;
    public Text storyIndex;

    public Image imageComponent;

    public Button[] buttonWay = new Button[3];
    public Text[] buttonWayText = new Text[3];

    private void Awake()
    {
        instance = this;
    }

    public void OnWayClick(int index)
    {
        bool CheckEventTypeNone = false;
        StoryModel playStoryModel = currentStoryModel;
        Debug.Log(index);

        if (playStoryModel.options[index].eventcheck.eventType == StoryModel.EventCheck.EventType.NONE)
        {
            for (int i = 0; i < playStoryModel.options[index].eventCheck.sucessResult.Length; i++)
            {
                GameSystem.instance.ApplyChoice(currentStoryModel.options[index}.eventCheck.sucessResult[i]);
                CheckEventTypeNone = true;
            }
        }
        bool CheckValue = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < buttonWay.Length; i++)
        {
            int wayIndex = i;

            buttonWay[i].onClick.AddListener(() => OnWayClick(wayIndex));
        }

        CoShowText();
    }

    public void CoShowText()
    {
        StoryModellnit();
        ResetShow();
        StartCoroutine(ShowText());
    }

    public void StoryModellnit()
    {
        fullText = currentStoryModel.storyText;

        storyIndex.text = currentStoryModel.storyNumber.ToString();

        for(int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWayText[i].text = currentStoryModel.options[i].buttonText;
        }
    }

    public void ResetShow()
    {
        textComponent.text = "";

        for(int i = 0; i < buttonWay.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(false);
        }
    }

    |Enumerator ShowText()
    {
        if(currentStoryModel.mainimage != null)
        {
            Rect rect = new Rect(0, 0, currentStoryModel.mainImage.width, currentStoryModel.mainImage.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(currentStoryModel.mainImage, rect, pivot);

            imageComponent.sprite = sprite;
        }
        else
        {
            Debug.LogError("텍스쳐의 이상이 있다.");
        }

        for (int i = 0; i < fullText. Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonway[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
