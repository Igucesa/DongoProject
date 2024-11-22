using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool gamePause;

    [SerializeField] GameObject pausePanel;
    PlayerControl player;


    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && player.inGround)
        {
            gamePause = !gamePause;
            pausePanel.SetActive(!pausePanel.activeSelf);
        }        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
