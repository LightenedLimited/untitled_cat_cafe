using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuStartScript : MonoBehaviour
{
    public Canvas main_menu_canvas;
    public Canvas options_canvas;
    public Canvas credits_canvas;
    public Canvas controls_canvas;

    public static MainMenuStartScript Instance; 

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Cafe-AI");
        main_menu_canvas.enabled = false;
        options_canvas.enabled = false;
        credits_canvas.enabled = false;
        controls_canvas.enabled = false;
    }

    public void EnableOptions()
    {
        main_menu_canvas.enabled = false;
        options_canvas.enabled = true;
        credits_canvas.enabled = false;
        controls_canvas.enabled = false; 
    }
    public void EnableMainMenu()
    {
        main_menu_canvas.enabled = true;
        options_canvas.enabled = false;
        credits_canvas.enabled = false;
        controls_canvas.enabled = false; 
    }
    public void EnableCredits()
    {
        main_menu_canvas.enabled = false;
        options_canvas.enabled = false;
        credits_canvas.enabled = true;
        controls_canvas.enabled = false; 
    }
    public void EnableControlCanvas()
    {
        main_menu_canvas.enabled = false;
        options_canvas.enabled = false;
        credits_canvas.enabled = false;
        controls_canvas.enabled = true;
    }

}
