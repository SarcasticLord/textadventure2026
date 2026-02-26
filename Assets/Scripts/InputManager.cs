using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public TMP_Text storyText; // the story 
    public TMP_InputField userInput; // the input field object
    public TMP_Text inputText; // part of the input field where user enters response
    public TMP_Text placeHolderText; // part of the input field for initial placeholder text'

    public ScrollRect scrollRect; // controls how our story scrolls
    
    private string story; // holds the story to display
    private List<string> commands = new List<string>();

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
        commands.Add("go");
        commands.Add("get");
        commands.Add("restart");
        commands.Add("save");

        story = storyText.text;
        userInput.onEndEdit.AddListener(GetInput);


    }

    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    void GetInput(string input)
    {

        userInput.text = "";
        userInput.ActivateInputField();

        if (input != "")
        {
            char[] delims = { ' ' };

            string[] parts = input.ToLower().Split(delims); // parts[0] command, parts[1] direction or pickup

            if (parts.Length > 0)
            {
                if (commands.Contains(parts[0]))
                {
                    UpdateStory(input);
                    if (parts[0] == "go")
                    {
                        if (NavigationManager.instance.SwitchRooms(parts[1]))
                            Debug.Log("direction exists");
                        else
                            UpdateStory("direction doesent exist or is locked");
                    }
                    else if (parts[0] == "get")
                    {
                        if (NavigationManager.instance.getItem(parts[1]))
                            GameManager.instance.inventory.Add(parts[1]);
                    }
                    else if (parts[0] == "save")
                        GameManager.instance.Save();

                    else if (parts[0] == "restart")
                        NavigationManager.instance.GameRestart();
                    else
                        UpdateStory("sorry thats not in this room");
                    
                }
                else
                {
                    UpdateStory("Rut roh... that didnt work. try that again.");
                }
            }
        }


    }

    

    public void UpdateStory(string msg)
    {
        story += "\n" + msg;
        storyText.text = story;
        StartCoroutine("ScrollToBottom");
    }
}
