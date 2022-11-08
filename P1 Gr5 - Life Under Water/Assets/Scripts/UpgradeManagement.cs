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

    public float[] IncreaseSpeed(int score, float playerSpeed)
    {
        float increasedSpeed = 0;
        float[] results = new float[2];

        if (score >= currentSpeedPrice)
        {
            results[0] = score - currentSpeedPrice;
            increasedSpeed = playerSpeed + speedIncrease;
            speedUpgradeLevel++;            
            results[1] = increasedSpeed;
        }
        else
        {
            increasedSpeed = playerSpeed;
            results[0] = score;
            results[1] = increasedSpeed;
        }

        return results;
    }

    public float[] IncreaseGrowth(int score, float playerIncrement)
    {
        float increasedGrowth = 0;
        float[] results = new float[2];

        if (score >= currentGrowthPrice)
        {
            results[0] = score - currentGrowthPrice;
            increasedGrowth = playerIncrement + growthIncrease;
            growthUpgradeLevel++;
            results[1] = increasedGrowth;
        }
        else
        {
            increasedGrowth = playerIncrement;
            results[0] = score;
            results[1] = increasedGrowth;
        }

        return results;
    }
}