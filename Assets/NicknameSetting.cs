using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NicknameSetting : MonoBehaviour
{
    public GameController GC;
    public InputField[] inputs;
    public Button buttonNext;
    public GameObject panelNickname, panelGame;

    private void Start()
    {
        for (int i = 0; i < GC.johab[0]; i++)
        {
            inputs[i].interactable = true;
        }
    }

    private void Update()
    {
        for (int i = 0; i < GC.johab[0]; i++)
        {
            if (inputs[i].text == "")
            {
                return;
            }
        }

        buttonNext.interactable = true;
    }

    public void ButtonGameStart()
    {
        for (int i = 0; i < GC.johab[0]; i++)
        {
            GC.nicknames[i] = inputs[i].text;
        }

        panelNickname.SetActive(false);
        panelGame.SetActive(true);
    }
}
