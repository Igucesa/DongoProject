using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Variables
    Animator anim;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float maxSpeed, maxJumpForce;
    [SerializeField] float knockBack;
    [SerializeField] float speed, jumpForce;
    Vector2 moveInput;
    public int life;
    [SerializeField] LayerMask layerGround;
    bool inGround;
    Transform foot;
    Rigidbody2D rb;
    public bool inParry;
    #endregion

#region StartAndUpdate
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        foot = GameObject.Find("Foot").GetComponent<Transform>();
    }

    void Start()
    {
        speed = maxSpeed;
        jumpForce = maxJumpForce;        
    }

    void Update()
    {
        VerifyBools();
        Jumping();
        //Parry();
    }

    void FixedUpdate()
    {
        Movement();
    }
    #endregion

 void VerifyBools()
    {
        inGround = Physics2D.OverlapCircle(foot.position, 0.3f, layerGround);
    }

#region MoveAndJump
    void Movement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal") * speed;
        moveInput.y = rb.velocity.y;

        rb.velocity = moveInput;

        transform.eulerAngles = rb.velocity.x != 0 ? (rb.velocity.x < 0 ? Vector2.up * 180 : Vector2.zero) : transform.eulerAngles;
    }

    void Jumping()
    {
        if(inGround)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
#endregion

#region Parry
#region ParryUnessentials
    void Parry()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            
            anim.SetTrigger("Parry");
        }
    }


    public void InParry()
    {
        StartCoroutine(InParryCoroutine());
    }

    public void StartParry()
    {
        inParry = true;
    }

    public void StopParry()
    {
        inParry = false;
    }

#endregion
    IEnumerator InParryCoroutine()
    {
        inParry = true;
        yield return new WaitForSeconds(0.35f);
        inParry = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet")
        {

            Destroy(other.gameObject);
            if(inParry)
            {
                Transform bulletTransform = other.gameObject.GetComponent<Transform>();
                
                DeflectBullet(bulletTransform);
                return;
            }
            life--;
        }    
    }

   void DeflectBullet(Transform bulletTransform)
{
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    Vector2 direction = new Vector2(horizontal, vertical).normalized;

    if (direction.magnitude > 0) 
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity); 

        
        bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        
        Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();
        if (brb != null)
        {
            brb.velocity = direction * Time.deltaTime; 
        }

        // StartCoroutine(ParryKnockBack());
       
    }

    /*IEnumerator ParryKnockBack()
    {
        
        if(!inGround) {
            jumpForce = 0;
        speed = 0;
            rb.velocity = Vector2.zero;
         rb.velocity = -direction * knockBack * Time.deltaTime;
        }

        yield return new WaitForSeconds(0.5f);

        jumpForce = maxJumpForce;
        speed = maxSpeed;
    } */
}
#endregion

}
