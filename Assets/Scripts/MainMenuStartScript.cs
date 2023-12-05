using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuStartScript : MonoBehaviour
{
    public Canvas main_menu_canvas;
    public Canvas options_canvas;

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Cafe"); 
    }

    public void EnableOptions()
    {
        main_menu_canvas.enabled = false;
        options_canvas.enabled = true; 
    }
    public void EnableMainMenu()
    {
        main_menu_canvas.enabled = true;
        options_canvas.enabled = false;
    }

}
