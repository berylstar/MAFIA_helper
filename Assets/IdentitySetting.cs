using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentitySetting : MonoBehaviour
{
    public GameController GC;
    public GameObject panelIdentity;
    public Text textPlayer, textTouch, textMafia, textButton;

    private float pointerTime = 0f;
    private bool isClick = false;
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
        }
        else
        {
            textPlayer.text = "\"" + GC.nicknames[index] + "\"";
            textTouch.text = "TOUCH";
            textMafia.text = "";
        }
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
        }
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
