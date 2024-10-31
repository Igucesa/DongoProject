using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Animator anim;
    Transform playerPos;
    [SerializeField] GameObject bulletPrefab;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(ShootCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }


    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
            
    }
    IEnumerator ShootCooldown()
    {
        while(true)
        {
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(0.4f);
            Shoot();
            yield return new WaitForSeconds(2);

        }
    }
}
