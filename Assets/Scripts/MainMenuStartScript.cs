using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuStartScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Cafe"); 
    }
}
