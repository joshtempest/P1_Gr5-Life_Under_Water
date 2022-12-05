using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class InfoManagement : MonoBehaviour
{
    public GameObject player;
    PlayerManager pm;
    public GameObject menuController;
    MenuManager mm;
    static int infoIndex = 0;
    public int i = 0;
    public TextMeshProUGUI currentInfoText;
    public TextMeshProUGUI[] infoTexts;

    [SerializeField] int [] currentWeightInfo = {5, 10, 20, 30, 40};

    private void Start()
    {
        pm = player.GetComponent<PlayerManager>();
        mm = menuController.GetComponent<MenuManager>();
    }

    public void Update()
    {
        
        //for (int i = 0; i < currentWeightInfo.Length; i++); 

        if (pm.score >= currentWeightInfo[0] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;
            currentWeightInfo[0] = currentWeightInfo[1];
        }
        else if (pm.score >= currentWeightInfo[0] && pm.score < currentWeightInfo[1] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;
            currentWeightInfo[0] = currentWeightInfo[1];

        }
        else if (pm.score >= currentWeightInfo[1] && pm.score < currentWeightInfo[2] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;
            currentWeightInfo[1] = currentWeightInfo[2];

        }
        else if (pm.score >= currentWeightInfo[2] && pm.score < currentWeightInfo[3] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;
            currentWeightInfo[2] = currentWeightInfo[3];

        }
        else if (pm.score >= currentWeightInfo[3] && pm.score < currentWeightInfo[4] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;
            currentWeightInfo[3] = currentWeightInfo[4];

        }
      
    }

    public void ShowInfo()
    {
        mm.InfoMenu();
        currentInfoText.text = infoTexts[infoIndex].text;
    }


    //Indsætte et array af int, som mankan kontrolerer defra. Lav den enten public eller serialized field, så kan man lndre det fra inspectoren. 

     /*public void Update()
    {
         if (pm.score >= pm.sizeLimits[infoIndex] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;

            pm.score[currentWeightInfo]
        }
    }

      else if (pm.score >= currentWeightInfo [0] && pm.score < currentWeightInfo[1] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;
            currentWeightInfo[i] = currentWeightInfo[1];
        }

    */

}
