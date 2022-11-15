using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManagement : MonoBehaviour
{
    public void StartGame(int gameScene)
    {
        SceneManager.LoadScene(gameScene);
    }
}