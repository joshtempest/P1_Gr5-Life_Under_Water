using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// This script controls everything related the the ending scene.
/// </summary>
public class EndScene : MonoBehaviour
{
    public static int totalScore;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    // Changes the text on the TextMeshPro object when the scene is opened.
    private void Start()
    {
        totalScoreText.text = "Total vægt:\n" + totalScore.ToString() + " kg";
    }

    // This method simply loads scene "0" in the load order when called.
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}