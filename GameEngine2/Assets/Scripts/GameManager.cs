using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver,
    }
    public GameState gState;

    GameObject play;

    Player player;

    public static GameManager gm;

    public GameObject optionUI;

    void Awake()
    {
        if (gm == null)
            gm = this;
    }

    void Start()
    {
        gState = GameState.Ready;
        StartCoroutine(GameStart());

        play = GameObject.Find("Player");
        player = play.GetComponent<Player>();

    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(15f);
        gState = GameState.Run;
    }

    void Update()
    {

    }

    public void OpenOptionWindow()
    {
        gState = GameState.Pause;

        Time.timeScale = 0;

        optionUI.SetActive(true);
    }

    public void CloseOptionWindow()
    {
        gState = GameState.Run;

        Time.timeScale = 1.0f;

        optionUI.SetActive(false);
    }

    public void GameRestart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameExit()
    {
        SceneManager.LoadScene("Title");
    }
}
