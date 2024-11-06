using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerPos;
    [SerializeField] float minX, minY, maxX, maxY;
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position = playerPos.position;
        //MinPosition
        if(playerPos.position.x <= minX) transform.position = new Vector2(minX, playerPos.position.y);
        if(playerPos.position.x <= minY) transform.position = new Vector2(playerPos.position.x, minY);

        //MaxPosition
        if(playerPos.position.x >= maxX) playerPos.position = new Vector2(maxX, playerPos.position.y);
        if(playerPos.position.x >= maxY) playerPos.position = new Vector2(playerPos.position.x, maxY);
    }
}
