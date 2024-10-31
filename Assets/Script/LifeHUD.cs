using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeHUD : MonoBehaviour
{
    public TMP_Text text;
    PlayerControl player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Vida: " + player.life;
    }
}