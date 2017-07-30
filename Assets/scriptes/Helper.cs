using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Helper : MonoBehaviour, UIActions, UIEnds
{

    [SerializeField]
    private GameObject uiHelpPanel;
    [SerializeField]
    private Text uiHelpText;


    [SerializeField]
    private GameObject uiEndPanel;
    [SerializeField]
    private Text uiEndText;
    [SerializeField]
    private Button uiOkButton;

    public void UIAction(UIMessageAction arg)
    {
        if (arg == null)
            return;

        uiHelpText.text = arg.paramString;
        uiHelpPanel.SetActive(arg.Visible);
    }
    public void UIEnd(UIMessageAction arg)
    {
        uiEndPanel.SetActive(true);
        if (arg.result == 1)
        {
            uiEndText.text = "Sorry. Power off all turret. Game over!";

        }
        else
        {
            uiEndText.text = "All turret full energy! The End!";
        }
    }


    public void Exit()
    {
        Application.LoadLevel("basecamp");
    }
}

public class UIMessageAction
{
    public bool Visible;
    public string paramString;
    public int result;
}

public interface UIActions : IEventSystemHandler
{

    void UIAction(UIMessageAction arg);
}
public interface UIEnds : IEventSystemHandler
{

    void UIEnd(UIMessageAction arg);
}
