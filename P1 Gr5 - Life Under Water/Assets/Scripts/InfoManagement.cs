using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

/// <summary>
/// This script controls everything related to the information popups.
/// </summary>
public class InfoManagement : MonoBehaviour
{
    public GameObject player;
    PlayerManager playerManager;
    public GameObject menuController;
    MenuManager menuManager;
    static int infoIndex = 0;
    public TextMeshProUGUI currentInfoText;
    public TextMeshProUGUI[] infoTexts;

    [SerializeField] int [] currentWeightInfo = {5, 10, 20, 30, 40};

    private void Start()
    {
        playerManager = player.GetComponent<PlayerManager>();
        menuManager = menuController.GetComponent<MenuManager>();
    }

    // Responsible for checking when the score exceeds information thresholds.
    public void Update()
    {
        if(playerManager.score >= currentWeightInfo[infoIndex] && infoIndex < 4)
        {
            ShowInfo();
            infoIndex++;
        }
    }

    // Calls menuManager.InfoMenu() to show the information menu.
    public void ShowInfo()
    {
        menuManager.InfoMenu();
        currentInfoText.text = infoTexts[infoIndex].text;
    }

}
