using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScene : MonoBehaviour
{
    public static int totalScore;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    
    private void Start()
    {
        totalScoreText.text = "Total vægt: " + totalScore + " kg";        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}