using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Text storyText; // the story 
    public InputField userInput; // the input field object
    public Text inputText; // part of the input field where user enters response
    public Text placeHolderText; // part of the input field for initial placeholder text
    
    private string story; // holds the story to display

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        story = storyText.text;
    }

    public void UpdateStory(string msg)
    {
        story += "\n" + msg;
        storyText.text = story;
    }
}
