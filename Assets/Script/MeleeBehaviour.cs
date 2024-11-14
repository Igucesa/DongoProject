using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : MonoBehaviour
{
    Transform playerpos;
    LayerMask player;
    bool playerFound;
    [SerializeField] float radius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindPlayer()
    {
        playerFound = Physics2D.OverlapCircle(transform.position, radius, player);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
