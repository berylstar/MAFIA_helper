using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NicknameSetting : MonoBehaviour
{
    public GameSetting GS;
    public InputField[] inputs;
    public Button buttonGameStart;

    private void Start()
    {
        for (int i = 0; i < GS.person; i++)
        {
            inputs[i].interactable = true;
        }
    }

    private void Update()
    {
        for (int i = 0; i < GS.person; i++)
        {
            if (inputs[i].text == "")
            {
                return;
            }
        }

        buttonGameStart.interactable = true;
    }

    public void ButtonGameStart()
    {
        for (int i = 0; i < GS.person; i++)
        {
            print(inputs[i].text);
        }
    }
}
