using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerAI : MonoBehaviour, ISetupable, IDamageable
{
    [Header("Setup")]
    [SerializeField] Transform setupCheck;
    bool setup = false;
    bool visble = false;

    [Header("素質")]
    [SerializeField] int enemyScore = 50;
    [SerializeField] int hp = 1;
    [SerializeField] float speed = 1;
    bool dead;

    [Header("移動")]
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform groundCheck;
    [SerializeField] float radius = 0.1f;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] bool turnBack; //會不會走出邊緣
    bool flip = false;

    [Header("元件")]
    Rigidbody2D m_rig2d;
    [SerializeField] GameObject damage;
    [SerializeField] PlayerControl pC;

    private void Start()
    {
        m_rig2d = GetComponent<Rigidbody2D>();
        pC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    private void Update()
    {
        if (!setup)
        {
            Setup();
        }

        if (setup && !dead && visble)
        {
            if (!flip){ m_rig2d.velocity = new Vector2(speed * -1, m_rig2d.velocity.y); }   //MoveLeft
            else{ m_rig2d.velocity = new Vector2(speed * 1, m_rig2d.velocity.y); }          //MoveRight

            if (Physics2D.OverlapCircle(wallCheck.position, radius, whatIsGround))
            {
                transform.Rotate(new Vector2(0, 180));
                flip = !flip;
            }

            if (turnBack)
            {
                if (!Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround))
                {
                    transform.Rotate(new Vector2(0, 180));
                    flip = !flip;
                }
            }
        }
    }

    //被玩家踩到
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerStep" && !dead)
        {
            EGotHit();
            Dead();
        }
    }

    /*碰到玩家
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("AAAAAAAAA");
        }
    }*/

    //顯示到畫面外
    private void OnBecameInvisible()
    {
        if (dead)
        {
            Destroy(gameObject);
        }
    }

    //顯示在畫面上
    private void OnBecameVisible()
    {
        visble = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.position,radius);
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    #region interface
    public void Setup()
    {
        /*
        if (Physics2D.Raycast(setupCheck.position, Vector2.down, 0.15f))
        {
            setup = true;
        }
        */
        if (Physics2D.OverlapCircle(setupCheck.position, radius, whatIsGround))
        {
            setup = true;
        }
    }
    //角色受傷(Interface引用)
    public void PGotHit(float a) { }
    //受傷
    public void EGotHit()
    {
        hp -= 1;
    }
    //死亡
    public void Dead()
    {
        if (hp <= 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            gameObject.transform.Rotate(180, 0, 0);
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(damage);
            dead = true;
            pC.score += enemyScore;
        }
    }
    #endregion
}
