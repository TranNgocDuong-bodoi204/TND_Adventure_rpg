using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButonsManagment : MonoBehaviour
{
    [SerializeField] private List<ButtonWarper> listButtons = new List<ButtonWarper>();
    void Awake()
    {
        
    }
    void OnEnable()
    {
        AssignButtonsEvent();
    }
    private void AssignButtonsEvent()
    {
        foreach(var button in listButtons)
        {
            DoAssign(button.buttonId);
        }
    }

    private void DoAssign(int id)
    {
        int index = id - 1;
        switch(id)
        {
            case 1:
                listButtons[index].button.onClick.AddListener(PlayBtn);
            break;
            case 2:
                listButtons[index].button.onClick.AddListener(SettingBtn);
            break;
            case 3:
                listButtons[index].button.onClick.AddListener(ExitBtn);
            break;
        }
    }
    private void PlayBtn()
    {
        SceneManager.LoadScene(1);
    }
    private void SettingBtn()
    {
        Debug.Log("Setting");
    }
    private void ExitBtn()
    {
        Debug.Log("Exit");
    }
}
[Serializable]
public class ButtonWarper
{
    public int buttonId;
    public Button button;

}
