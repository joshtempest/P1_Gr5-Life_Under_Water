using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoManagement : MonoBehaviour
{
    public GameObject player;
    PlayerManager pm;
    public GameObject menuController;
    MenuManager mm;
    int infoIndex;
    public TextMeshProUGUI currentInfoText;
    public TextMeshProUGUI[] infoTexts;

    private void Start()
    {
        pm = player.GetComponent<PlayerManager>();
        mm = menuController.GetComponent<MenuManager>();
        infoIndex = 0;
    }

    public void Update()
    {
        if (pm.score >= pm.sizeLimits[infoIndex] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;
        }
    }

    public void ShowInfo()
    {
        mm.InfoMenu();
        currentInfoText.text = infoTexts[infoIndex].text;
    }
}