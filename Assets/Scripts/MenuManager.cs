using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas canvas; 

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameManagerOnGameStateChange(GameState obj)
    {
        if (obj == GameState.GamePaused)
        {
            canvas.enabled = true; 
        }
        if(obj == GameState.GameRunning)
        {
            canvas.enabled = false; 
        }
    }
    public void turnOffPaused()
    {
        canvas.enabled = false;
        GameManager.Instance.UpdateGameState(GameState.GameRunning);
    }


    public void StartGame()
    {
        //Debug.Log("TODO: start the game");
         SceneManager.LoadScene("Scenes/Cafe");
    }
}
