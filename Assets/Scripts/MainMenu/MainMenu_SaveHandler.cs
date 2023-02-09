using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_SaveHandler : MonoBehaviour
{
    public InputField input;
    public GameObject button_continue;
    public GameObject inputHolder;
    void Start()
    {
        button_continue.SetActive(SaveSystem.SaveFileExists());
    }

    public void LoadSave(){
        SaveSystem.Load();
    }

    public void ShowNameInput(){
        inputHolder.SetActive(true);
    }


    public void GetNameInput(){
        SaveSystem.NewSave(input.text);
        BirefingHolder.nextBriefing = "0_Start";
        Loading.LoadScene("Briefing");
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
