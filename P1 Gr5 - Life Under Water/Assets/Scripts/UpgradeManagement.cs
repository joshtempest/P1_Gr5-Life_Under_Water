using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UpgradeManagement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedPriceText; //The text showing the price for more speed.
    [SerializeField] TextMeshProUGUI growthPriceText; //The text showing the price for more growth (sizeIncrement).
    [SerializeField] private int[] speedPrices; //The collection of prices for more speed
    [SerializeField] private int[] growthPrices; //The collection of prices for more growth
    private int currentSpeedPrice; //The current price for the speed upgrade.
    private int currentGrowthPrice; //The current price for the growth upgrade.
    [SerializeField] private int speedIncrease; //The amount that speed is increased
    [SerializeField] private int growthIncrease; //The amount that growth is increased (by decreasing sizeIncrement).
    private int speedUpgradeLevel; //The amount of purchased speed upgrades
    private int growthUpgradeLevel; //The amount of purchased growth upgrades
    [SerializeField] GameObject speedButton; //The button to upgrade speed
    [SerializeField] GameObject growthButton; //The button to upgrade growth
    [SerializeField] TextMeshProUGUI speedMaxedOutText; //The text that shows when you have bought all speed upgrades
    [SerializeField] TextMeshProUGUI growthMaxedOutText; //The text that shows when you have bought all growth upgrades

    private void Start()
    {
        //Sets the amount of upgrades to 0
        speedUpgradeLevel = 0;
        growthUpgradeLevel = 0;
        //Sets the current price to be the first price
        currentSpeedPrice = speedPrices[0];
        currentGrowthPrice = growthPrices[0];
        //Sets the upgrade buttons to be active
        speedButton.SetActive(true);
        growthButton.SetActive(true);
        //Sets the maxed out text to be not active
        speedMaxedOutText.gameObject.SetActive(false);
        growthMaxedOutText.gameObject.SetActive(false);
        //Sets the text to show the current price for the upgrades
        speedPriceText.text = currentSpeedPrice.ToString();
        growthPriceText.text = currentGrowthPrice.ToString();
    }

    private void FixedUpdate()
    {
        //Sets the current price to match the amount of bought upgrades
        currentSpeedPrice = speedPrices[speedUpgradeLevel];
        currentGrowthPrice = growthPrices[growthUpgradeLevel];
        //Sets the text to show the current price for the upgrades
        speedPriceText.text = currentSpeedPrice.ToString();
        growthPriceText.text = currentGrowthPrice.ToString();

        //If it reaches the last price, which is not meant to be used, then the buttons will be shut off and the text will be active.
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

    //Takes the score and the player speed, "buys" the upgrade, and gives the info back to the player
    public float[] IncreaseSpeed(int score, float playerSpeed)
    {
        float increasedSpeed = 0; //Declares the variable for the increased speed
        float[] results = new float[2]; //The results that we need for the player (index [0] being the new score and [1] being the new speed)

        //If the score is larger than or equal to the current price
        if (score >= currentSpeedPrice)
        {
            results[0] = score - currentSpeedPrice; //The first result is the new score (the old score minus the price)
            increasedSpeed = playerSpeed + speedIncrease; //The new speed is defined as the old speed plus the increase.
            speedUpgradeLevel++; //Sets the amount of bought upgrades up
            results[1] = increasedSpeed; //The second result is the new speed
        }
        else
        {
            //If the score is less than the price, then it returns the old score and speed.
            increasedSpeed = playerSpeed;
            results[0] = score;
            results[1] = increasedSpeed;
        }

        return results;
    }

    //Takes the score and the player size increment, "buys" the upgrade, and gives the info back to the player
    public float[] IncreaseGrowth(int score, float playerIncrement)
    {
        float increasedGrowth = 0; //Declares the variable for the new size increment
        float[] results = new float[2]; //The results that we need for the player (index [0] being the new score and [1] being the new size increment)

        //If the score is larger than or equal to the current price
        if (score >= currentGrowthPrice)
        {
            results[0] = score - currentGrowthPrice; //The first result is the new score (the old score minus the price)
            increasedGrowth = playerIncrement + growthIncrease; //The new size increment is defined as the old minus the increase.
            growthUpgradeLevel++; //Sets the amount of bought upgrades up
            results[1] = increasedGrowth; //The second result is the new size increment
        }
        else
        {
            //If the score is less than the price, then it returns the old score and size increment.
            increasedGrowth = playerIncrement;
            results[0] = score;
            results[1] = increasedGrowth;
        }

        return results;
    }
}