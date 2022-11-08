using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UpgradeManagement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedPriceText;
    [SerializeField] TextMeshProUGUI growthPriceText;
    [SerializeField] private int[] speedPrices;
    [SerializeField] private int[] growthPrices;
    private int currentSpeedPrice;
    private int currentGrowthPrice;
    [SerializeField] private int speedIncrease;
    [SerializeField] private int growthIncrease;
    private int speedUpgradeLevel;
    private int growthUpgradeLevel;
    [SerializeField] GameObject speedButton;
    [SerializeField] GameObject growthButton;
    [SerializeField] TextMeshProUGUI speedMaxedOutText;
    [SerializeField] TextMeshProUGUI growthMaxedOutText;

    private void Start()
    {
        speedUpgradeLevel = 0;
        growthUpgradeLevel = 0;
        currentSpeedPrice = speedPrices[0];
        currentGrowthPrice = growthPrices[0];
        speedButton.SetActive(true);
        growthButton.SetActive(true);
        speedMaxedOutText.gameObject.SetActive(false);
        growthMaxedOutText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        currentSpeedPrice = speedPrices[speedUpgradeLevel];
        currentGrowthPrice = growthPrices[growthUpgradeLevel];
        speedPriceText.text = currentSpeedPrice.ToString();
        growthPriceText.text = currentGrowthPrice.ToString();

        if (currentSpeedPrice == 0)
        {
            speedButton.SetActive(false);
            speedMaxedOutText.gameObject.SetActive(true);
        }
        if (currentGrowthPrice == 0)
        {
            growthButton.SetActive(false);
            growthMaxedOutText.gameObject.SetActive(true);
        }
    }

    public float IncreaseSpeed(int score, float playerSpeed)
    {
        float increasedSpeed = 0;

        if (score >= currentSpeedPrice)
        {
            increasedSpeed = playerSpeed + speedIncrease;
            speedUpgradeLevel++;
        }
        else
        {
            increasedSpeed = playerSpeed;
        }

        return increasedSpeed;
    }

    public float IncreaseGrowth(int score, float playerIncrement)
    {
        float increasedGrowth = 0;
        
        if (score >= currentGrowthPrice)
        {
            increasedGrowth = playerIncrement - growthIncrease;
            growthUpgradeLevel++;
        }
        else
        {
            increasedGrowth = playerIncrement;
        }

        return increasedGrowth;
    }
}