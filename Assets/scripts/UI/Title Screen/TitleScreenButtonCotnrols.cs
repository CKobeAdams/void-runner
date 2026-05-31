using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtonCotnrols : MonoBehaviour
{
    public void ReturnTitle()
    {
        SceneManager.LoadScene("Title Screen", LoadSceneMode.Single);
    }

    public void LoadMainRun()
    {
        SceneManager.LoadScene("main run", LoadSceneMode.Single);
    }

    public void LoadControlsOverview()
    {
        SceneManager.LoadScene("1-Overview", LoadSceneMode.Single);
    }

    public void LoadControlsDeathWall()
    {
        SceneManager.LoadScene("2-Death Wall", LoadSceneMode.Single);
    }

    public void LoadControlsMovement()
    {
        SceneManager.LoadScene("3-Movement", LoadSceneMode.Single);
    }

    public void LoadControlsFlippingOut()
    {
        SceneManager.LoadScene("4-Flipping Out", LoadSceneMode.Single);
    }

    public void LoadControlsTricks()
    {
        SceneManager.LoadScene("5-Tricks", LoadSceneMode.Single);
    }
}
