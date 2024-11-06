using UnityEngine;

public class BulletScript : MonoBehaviour
{
    
    public float speed;
    public bool fromPlayer;

    [SerializeField] Color playerBullet;
    [SerializeField] Color enemyBullet;
    void Start()
    {
       
        Destroy(gameObject, 2f);
    }
    void Update()
    {
        if(fromPlayer) gameObject.GetComponent<SpriteRenderer>().material.color = Color.yellow;
        else gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
    }

    void FixedUpdate()
    {
        transform.position += transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       //The player damage part is at "PlayerControl.cs"
        if(other.gameObject.tag == "Enemy")
        {
            if(!fromPlayer) return;
            other.GetComponent<EnemyBehaviour>().TakeHit(1);
            Destroy(gameObject);
        }
    }
}
