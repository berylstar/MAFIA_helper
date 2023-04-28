using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// MAFIA : 1
// DOCTOR : 2
// COP : 3
// CITIZEN : 4

public class GameController : MonoBehaviour
{
    [Header ("Game System")]
    public int[] johab = { 0, 0, 0, 0, 0 };     // 총인원, 마피아, 의사, 경찰, 시민
    public int[] players = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public string[] nicknames = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
    public bool[] alives = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
    public GameObject panelGameOver;
    public Text textWin, textMafia;

    public void IdentityShuffle()
    {
        int[] idx = new int[johab[0]];

        for (int i = 0; i < idx.Length; i++)
        {
            idx[i] = i;
        }

        for (int i = 0; i < idx.Length; i++)
        {
            int temp = idx[i];
            int randomIndex = Random.Range(0, idx.Length);
            idx[i] = idx[randomIndex];
            idx[randomIndex] = temp;
        }

        for (int i = 0; i < idx.Length; i++)
        {
            int a = johab[1];
            int b = johab[1] + johab[2];
            int c = johab[1] + johab[2] + johab[3];


            if (i < a)
                players[idx[i]] = 1;
            else if (a <= i && i < b)
                players[idx[i]] = 2;
            else if (b <= i && i < c)
                players[idx[i]] = 3;
            else
                players[idx[i]] = 4;

            alives[i] = true;
        }
    }

    public string ShowIdentity(int i)
    {
        if (i == 1)
            return "마피아";
        else if (i == 2)
            return "의사";
        else if (i == 3)
            return "경찰";
        else if (i == 4)
            return "시민";
        return null;
    }

    public string FindMafia(int my)
    {
        if (johab[1] == 1)
            return "";

        string str = "";

        for (int i = 0; i < johab[0]; i++)
        {
            if (players[i] == 1 && i != my)
                str += "\n<" + nicknames[i] + ">";
        }

        return "마피아 팀" + str;
    }

    [Header ("Morning")]
    public GameObject panelMorning;
    public Button[] buttonMornings;
    public Text textDay, textAllPerson;
    private int day = 0;
    public int[] members = { 0, 0, 0, 0, 0 };

    [Header ("Night")]
    public GameObject panelNight;
    public Button[] buttonNights;
    public Text textPlayer;
    public GameObject buttonNight, scrollNight, buttonNext;
    private int nightIndex = 0;
    private int killermafia, mafiaPick, doctorPick, copPick;
    public Text textCop;

    public void FirstMorning()
    {
        members = (int[])johab.Clone();
    }

    public void TimeToMorning()
    {
        CheckGameOver();

        panelMorning.SetActive(true);

        day += 1;
        textDay.text = day + " 일차 아침";
        textAllPerson.text = "생존 인원 : " + members[0];

        for (int i = 0; i < johab[0]; i++)
        {
            buttonMornings[i].GetComponentInChildren<Text>().text = nicknames[i];
            buttonMornings[i].interactable = alives[i];
        }
    }

    private void Elimination(int idx)
    {
        members[0] -= 1;                // 총원 한 명 감소
        members[players[idx]] -= 1;     // 해당 직업 한 명 감소
        alives[idx] = false;            // 해당 사람 죽음
    }

    public void ButtonVote()
    {
        int thisdie = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        Elimination(thisdie);

        panelMorning.SetActive(false);
        TimeToNight();
    }

    public void TimeToNight()
    {
        CheckGameOver();

        panelNight.SetActive(true);

        nightIndex = 0;
        killermafia = RandomOneOfMafia();
        mafiaPick = 16;
        doctorPick = 16;
        copPick = 16;

        textPlayer.text = "\"" + nicknames[nightIndex] + "\"";

        for (int i = 0; i < johab[0]; i++)
        {
            buttonNights[i].GetComponentInChildren<Text>().text = nicknames[i];
            buttonNights[i].interactable = alives[i];
        }
    }

    public void ButtonTouch()
    {
        if (alives[nightIndex])
        {
            buttonNight.SetActive(false);
            scrollNight.SetActive(true);
            scrollNight.GetComponentInChildren<Scrollbar>().value = 1;
        }
        else
        {
            buttonNext.SetActive(true);
        }
    }

    private int RandomOneOfMafia()
    {
        int[] idx = new int[members[0]];

        for (int i = 0; i < idx.Length; i++)
        {
            idx[i] = i;
        }

        for (int i = 0; i < idx.Length; i++)
        {
            int temp = idx[i];
            int randomIndex = Random.Range(0, idx.Length);
            idx[i] = idx[randomIndex];
            idx[randomIndex] = temp;
        }

        for (int i = 0; i < idx.Length; i++)
        {
            if (players[idx[i]] == 1 && alives[idx[i]])
                return idx[i];
        }

        return 16;
    }

    public void ButtonNightPick()
    {
        int pick = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        if (players[nightIndex] == 1)
        {
            if (nightIndex == killermafia)
            {
                mafiaPick = pick;
            }
        }
        else if (players[nightIndex] == 2)
        {
            doctorPick = pick;
        }
        else if (players[nightIndex] == 3)
        {
            copPick = pick;

            textCop.gameObject.SetActive(true);
            if (players[copPick] == 1)
                textCop.text = "O";
            else
                textCop.text = "X";
        }
        else
        {
            // 시민 ㅇㅅㅇ
        }

        scrollNight.SetActive(false);
        buttonNext.SetActive(true);
    }

    public void ButtonNightNext()
    {
        nightIndex += 1;
        textPlayer.text = "\"" + nicknames[nightIndex] + "\"";

        textCop.gameObject.SetActive(false);

        buttonNight.SetActive(true);
        if (nightIndex == johab[0] - 1)
            EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "END";

        else if (nightIndex == johab[0])
        {
            if(doctorPick == mafiaPick || mafiaPick == 16)
            {
                // 아무일도 없었다...!
            }
            else
            {
                Elimination(mafiaPick);
            }

            panelNight.SetActive(false);
            TimeToMorning();
        }

        buttonNext.SetActive(false);
    }

    public void CheckGameOver()
    {
        if (members[1] <= 0)
        {
            textWin.text = "시민 승리 !";
            panelGameOver.SetActive(true);
        }
            
        else if (members[1] >= members[2] + members[3] + members[4])
        {
            textWin.text = "마피아 승리 !";
            panelGameOver.SetActive(true);
        }

        string strr = "<마피아>\n";

        for (int i = 0; i < johab[0]; i++)
        {
            if (players[i] == 1)
                strr += nicknames[i].ToString() + "\n";
        }

        textMafia.text = strr;
    }

    public void ButtonRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}