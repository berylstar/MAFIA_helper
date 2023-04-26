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
    public int[] johab = { 0, 0, 0, 0, 0 };     // ���ο�, ���Ǿ�, �ǻ�, ����, �ù�
    public int[] players = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public string[] nicknames = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
    public bool[] alives = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

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
            return "���Ǿ�";
        else if (i == 2)
            return "�ǻ�";
        else if (i == 3)
            return "����";
        else if (i == 4)
            return "�ù�";
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

        return "���Ǿ� �� : \n" + str;
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

    public void FirstMorning()
    {
        members = (int[])johab.Clone();
    }

    public void TimeToMorning()
    {
        // ���� �׾�����

        CheckGameOver();

        panelMorning.SetActive(true);

        day += 1;
        textDay.text = day + " ���� ��ħ";
        textAllPerson.text = "���� �ο� : " + members[0];

        for (int i = 0; i < johab[0]; i++)
        {
            buttonMornings[i].GetComponentInChildren<Text>().text = nicknames[i];
            buttonMornings[i].interactable = alives[i];
        }
    }

    public void ButtonVote()
    {
        int thisdie = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        members[0] -= 1;                    // �ѿ� �� �� ����
        members[players[thisdie]] -= 1;     // �ش� ���� �� �� ����
        alives[thisdie] = false;            // �ش� ��� ����

        panelMorning.SetActive(false);
        TimeToNight();
    }

    public void TimeToNight()
    {
        CheckGameOver();

        panelNight.SetActive(true);

        for (int i = 0; i < johab[0]; i++)
        {
            buttonNights[i].GetComponentInChildren<Text>().text = nicknames[i];
            buttonNights[i].interactable = alives[i];
        }
    }

    public void ButtonToMorning()
    {
        panelNight.SetActive(false);
        TimeToMorning();
    }

    public void CheckGameOver()
    {
        if (members[1] <= 0)
            print("�ù� �¸�");
        else if (members[1] >= members[2] + members[3] + members[4])
            print("���Ǿ� �¸�");
        else
            print("���� ��� ����");
    }
}