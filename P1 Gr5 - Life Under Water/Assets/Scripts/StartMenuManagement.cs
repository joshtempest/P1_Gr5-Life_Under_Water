using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls everything related to the start menu.
/// </summary>
public class StartMenuManagement : MonoBehaviour
{
    public void StartGame(int gameScene)
    {
        SceneManager.LoadScene(gameScene);
    }
}