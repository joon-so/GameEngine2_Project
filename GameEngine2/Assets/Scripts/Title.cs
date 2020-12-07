using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject manual;
    bool manual_look = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            manual_look = false;
            manual.gameObject.SetActive(manual_look);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void ManualButton()
    {
        if (!manual_look)
        {
            manual_look = true;
            manual.gameObject.SetActive(manual_look);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
