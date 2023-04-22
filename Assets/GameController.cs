using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MAFIA : 1
// DOCTOR : 2
// COP : 3
// CITIZEN : 4

public class GameController : MonoBehaviour
{
    public int[] johab = { 0, 0, 0, 0, 0 };     // 총인원, 마피아, 의사, 경찰, 시민
    public int[] players = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public string[] nicknames = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

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
        string str = "";

        for (int i = 0; i < johab[0]; i++)
        {
            if (players[i] == 1 && i != my)
                str += "\"" + nicknames[i] + "\"";
        }

        return "다른 마피아는\n" + str + "\n입니다.";
    }
}