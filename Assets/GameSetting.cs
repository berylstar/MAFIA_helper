using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public GameObject panelSetting;
    public GameObject panelNickname;

    [Header("Person")]
    public Text textPerson;
    public Button buttonAdd, buttonSub;
    public int person = 6;

    [Header("Mafia")]
    public Text textMafia;
    public Slider sliderMafia;
    public int mafia = 2;

    [Header("Doctor")]
    public Toggle toggleDoctor;

    [Header("Cop")]
    public Toggle toggleCop;

    private void Update()
    {
        sliderMafia.maxValue = Mathf.Min(person / 2 - 1, 4);
        mafia = (int)sliderMafia.value;
        textMafia.text = sliderMafia.value.ToString();
    }

    public void ButtonAddPerson()
    {
        ++person;
        textPerson.text = "인원 : " + person;

        if (person >= 15)
            buttonAdd.interactable = false;
        else if (person > 6)
            buttonSub.interactable = true;
    }

    public void ButtonSubPerson()
    {
        --person;
        textPerson.text = "인원 : " + person;

        if (person <= 6)
            buttonSub.interactable = false;
        else if (person < 15)
            buttonAdd.interactable = true;
    }

    public void ButtonNext()
    {
        int doctor = toggleDoctor.isOn ? 1:0;
        int cop = toggleCop.isOn ? 1 : 0;

        print((person, mafia, doctor, cop));

        panelSetting.SetActive(false);
        panelNickname.SetActive(true);
    }
}
