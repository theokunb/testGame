using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuView : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(Constants.SceneIndex.GameScene);
    }

    public void Exit()
    {
        Environment.Exit(0);
    }
}