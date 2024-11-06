using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Animator anim;
    Transform playerPos;
    
    [SerializeField] GameObject bulletPrefab;
   [SerializeField] int health = 5;

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
        VerifyDead();
    }

    void VerifyDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void Shoot()
    {
        Vector3 playerDirection = playerPos.position - transform.position;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg);
        BulletScript bs = bullet.GetComponent<BulletScript>();
        bs.fromPlayer = false;
        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(playerDirecion.x, playerDirecion.y).normalized * bs.speed * Time.deltaTime;
        //bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(playerDirecion.x, playerDirecion.y).normalized * bs.speed, ForceMode2D.Impulse);
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

    public void TakeHit(int damage)
    {
        health -= damage;
    }
}
