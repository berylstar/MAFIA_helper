using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentitySetting : MonoBehaviour
{
    public GameController GC;
    public GameObject panelIdentity;
    public Text textPlayer, textTouch, textMafia, textButton;
    public GameObject buttonNext;

    private bool isClick = false;
    private bool isCheck = false;
    private int index = 0;

    private void Update()
    {
        if (isClick)
        {
            textPlayer.text = "당신은\n\"" + GC.ShowIdentity(GC.players[index]) + "\"\n입니다.";
            textTouch.text = "";

            if (GC.players[index] == 1)
                textMafia.text = GC.FindMafia(index);
            else
                textMafia.text = "";

            isCheck = true;
        }
        else
        {
            textPlayer.text = "\"" + GC.nicknames[index] + "\"";
            textTouch.text = "TOUCH";
            textMafia.text = "";
        }

        buttonNext.SetActive(isCheck);

    }

    public void ButtonNext()
    {
        if (index < GC.johab[0] - 2)
            ++index;
        else if (index == GC.johab[0] - 2)
        {
            ++index;
            textButton.text = "START";
        }   
        else
        {
            panelIdentity.SetActive(false);
            GC.FirstMorning();
            GC.TimeToMorning();
        }

        isCheck = false;
    }

    public void PointerDown()
    {
        isClick = true;
    }

    public void PointerUp()
    {
        isClick = false;
    }
}
