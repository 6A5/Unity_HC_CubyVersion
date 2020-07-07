using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour, IDamageable
{
    [Header("變數")]
    public bool startMove;
    public bool invicible;
    public LayerMask whatIsGround;
    public float radius = 0.1f;
    public bool move;
    public bool dead;
    public bool faceRight;

    [Header("物件")]
    public Animator m_animator;
    public Rigidbody2D m_rig;
    public PauseSystem pS;
    public Transform wallCheck;
    public Transform lSide;
    public Transform rSide;

    [Tooltip("Auto")]
    public GameObject player;

    [Header("素質")]
    [SerializeField] int hp = 3;
    [SerializeField] int enemyScore = 1000;
    [SerializeField] float speed = 3;

    [Header("動畫")]
    [SerializeField] int idleTimes;
    int cur_idleTimes;
    [SerializeField] int jumpTimes;
    int rndJumpTimes;
    int cur_jumpTimes;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rndJumpTimes = Random.Range(jumpTimes - 2, jumpTimes + 2);
    }

    private void Update()
    {
        // 開始執行
        if (pS.gS == PauseSystem.GameState.Going)
            m_animator.enabled = true;

        if (move)
        {
            if (!faceRight) m_rig.velocity = Vector2.left * speed;
            else m_rig.velocity = Vector2.right * speed;
        }

        WallCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerStep" && !dead)
        {
            EGotHit();
            Dead();
        }
    }

    public void WallCheck()
    {
        if (Physics2D.OverlapCircle(wallCheck.position, radius, whatIsGround))
        {
            transform.Rotate(new Vector2(0, 180));
            faceRight = !faceRight;
        }
    }

    /// <summary>
    /// 轉向玩家
    /// </summary>
    public void AniFlipCheck()
    {
        if (transform.position.x < player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            faceRight = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            faceRight = false;
        }
    }

    /// <summary>
    /// 待機狀態
    /// </summary>
    public void AniIdleCheck()
    {
        cur_idleTimes++;
        AniFlipCheck();

        if (cur_idleTimes == idleTimes) // 當待機循環次數到達指定數量
        {
            cur_idleTimes = 0;

            int r = Random.Range(1, 100); // 隨機 1~100
            if (r > 20)
            {
                m_animator.SetTrigger("StartJump"); // 20 以上出跳躍
                rndJumpTimes = Random.Range(jumpTimes - 2, jumpTimes + 2); // 指定跳躍次數 正負2 為隨機後跳躍次數
            }
            else
            {
                m_animator.SetTrigger("Disappear"); // 20 含以下出瞬移
            }
        }
    }

    /// <summary>
    /// 跳躍攻擊狀態
    /// </summary>
    public void AniJumpCheck()
    {
        cur_jumpTimes++;

        int r = Random.Range(1, 100); // 隨機 1~100
        if (r <= 15)
        {
            AniFlipCheck(); // 機率性轉向玩家
        }

        if (cur_jumpTimes == rndJumpTimes) // 當跳躍次數到達隨機的指定數量
        {
            cur_jumpTimes = 0;
            move = false; // 停止移動

            m_animator.SetTrigger("EndJump"); // 停止跳躍
        }
    }

    /// <summary>
    /// 瞬移狀態
    /// </summary>
    public void AniShowMove()
    {
        transform.position = new Vector2(Random.Range(lSide.position.x, rSide.position.x), transform.position.y); // 隨機跑到L和R之間的其中一個地方
    }

    /// <summary>
    /// 布林開關
    /// </summary>
    public void AniMoveOpen() // 開啟移動
    {
        move = true;
    }
    public void AniInvicibleOpen() // 開啟無敵
    {
        invicible = true;
    }
    public void AniInvicibleCancel() // 取消無敵
    {
        invicible = false;
    }

    /// <summary>
    /// 死亡判定
    /// </summary>
    public void Dead()
    {
        if (hp <= 0)
        {
            m_animator.SetTrigger("Dead");
            dead = true;
            player.GetComponent<PlayerControl>().score += enemyScore;
        }
    }

    /// <summary>
    /// 怪物受傷
    /// </summary>
    public void EGotHit()
    {
        if (!invicible)
        {
            hp--;
            if (hp >0)
            {
                m_animator.SetTrigger("Hurt");
            }

            // 難度變速
            switch (hp)
            {
                case 3:
                    break;

                case 2:
                    speed = speed * 1.5f;
                    idleTimes--;
                    break;

                case 1:
                    speed = speed * 2;
                    jumpTimes = jumpTimes + 2;
                    break;
            }
        }
    }

    /// <summary>
    /// 玩家受傷
    /// </summary>
    /// <param name="headPos"></param>
    public void PGotHit(float headPos)
    {
    }

}
