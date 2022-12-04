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
    static int infoIndex = 0;
    public TextMeshProUGUI currentInfoText;
    public TextMeshProUGUI[] infoTexts;

    private void Start()
    {
        pm = player.GetComponent<PlayerManager>();
        mm = menuController.GetComponent<MenuManager>();
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