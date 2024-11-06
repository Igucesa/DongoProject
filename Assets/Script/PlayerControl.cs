using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
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
    bool inKnockBack;
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
        Parry();
    }

    void FixedUpdate()
    {
        if(inKnockBack) return;
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

    public void StartParry()
    {
        if(inParry) return;
        inParry = true;
        Debug.Log("parry começou");
    }

    public void StopParry()
    {
        inParry = false;
        Debug.Log("parry parou");
    }

#endregion

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet")
        {
            
            if(inParry)
            {
                Transform bulletTransform = other.gameObject.GetComponent<Transform>();
                Destroy(other.gameObject);
                DeflectBullet(bulletTransform);
                return;
            }
            if(other.GetComponent<BulletScript>().fromPlayer) return;
            Destroy(other.gameObject);
            life--;
        }    
    }

#region oldParry
   void DeflectBullet(Transform bulletTransform)
{
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");

    if(new Vector2(horizontal, vertical) == Vector2.zero) horizontal = 1;

    Vector2 direction = new Vector2(horizontal, vertical).normalized;

    if (direction.magnitude > 0) 
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity); 

        bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        bullet.GetComponent<BulletScript>().fromPlayer = true;

        Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();
        if (brb != null)
        {
            //imagine this being a playerbullet speed
            brb.velocity = direction * Time.deltaTime; 
            //brb.AddForce(direction * bullet.GetComponent<BulletScript>().speed * Time.deltaTime); 
        }
         StartCoroutine(ParryKnockBack());
    }
    
    IEnumerator ParryKnockBack()
    {
       // Quaternion knockDir = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
        if(!inGround) {
            inKnockBack = true;
            jumpForce = 0;
        speed = 0;
        rb.velocity = Vector2.zero;
         rb.AddForce(-direction * knockBack, ForceMode2D.Impulse);
          yield return new WaitForSeconds(1f);
        inKnockBack = false;
        jumpForce = maxJumpForce;
        speed = maxSpeed;
        }
    }
}
#endregion
#endregion

}
