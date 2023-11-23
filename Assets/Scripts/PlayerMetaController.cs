using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnGameStateChange += GameManagerOnGameStateChange; 
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
        if (obj == GameState.GamePaused) this.transform.GetComponent<PlayerController>().enabled = false;
        else this.transform.GetComponent<PlayerController>().enabled = true;
    }

    public void OnPause()
    {
        if (GameManager.Instance.state == GameState.GamePaused) GameManager.Instance.UpdateGameState(GameState.GameRunning);
        else GameManager.Instance.UpdateGameState(GameState.GamePaused);
    }
}
