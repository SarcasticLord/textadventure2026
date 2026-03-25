using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public AudioSource bgmSource;

    public Text toggleText;

    private bool music;
    private Toggle toggle;

    void Start()
    {
        //PlayerPrefs.HasKey("darkmode");
        music = PlayerPrefs.GetInt("music", 1) == 1? true : false; // 1 is the default = darkmode is true
        toggle = GetComponent<Toggle>();
        SetTheme();
        toggle.onValueChanged.AddListener(UpdateTheme);

        
    }
    void UpdateTheme(bool isChecked)
    {
        music = isChecked;
        PlayerPrefs.SetInt("music", music ? 1 : 0);
        PlayerPrefs.Save();
        SetTheme();
    }

    void SetTheme()
    {
        if (music)
        {
            toggle.isOn = true; // check the box
            bgmSource.Play();
        }
        else
        {   
            toggle.isOn = false; // uncheck the box
            bgmSource.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
