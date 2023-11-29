using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaController : MonoBehaviour
{
    public MenuManager menu;

    private bool showingTaskManager; 

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange;
        showingTaskManager = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(GameState obj) {
        //TODO: reset animations during pause
        if (obj == GameState.GamePaused) this.transform.GetComponent<PlayerController>().enabled = false;
        else this.transform.GetComponent<PlayerController>().enabled = true;
    }

    public void OnPause()
    {

        if (GameManager.Instance.state == GameState.GamePaused) {
            GameManager.Instance.UpdateGameState(GameState.GameRunning);
            menu.turnOffPaused();
            return; 
        }
        if (showingTaskManager)
        {
            OnToggleTaskManager();
        }
        if (GameManager.Instance.state == GameState.GameRunning) {
            GameManager.Instance.UpdateGameState(GameState.GamePaused);
            menu.turnOnPaused();
            return;
        }
    }
    public void OnToggleTaskManager()
    {
        if (GameManager.Instance.state == GameState.GamePaused) return; 

        if(showingTaskManager)
        {
            menu.turnOffTaskManger();
            showingTaskManager = false; 
        }
        else
        {
            menu.turnOnTaskManager();
            showingTaskManager = true; 
        }
    }
}
