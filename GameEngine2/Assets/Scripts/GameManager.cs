using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver,
    }

    GameState gState;

    // Start is called before the first frame update
    void Start()
    {
        gState = GameState.Ready;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
