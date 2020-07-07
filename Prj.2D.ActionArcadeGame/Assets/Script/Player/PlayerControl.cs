using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IDamageable
{
    public bool debugMode;

    [Header("移動")]
	[SerializeField] float speed;
    public float jumpHeight;
    [Tooltip("自動抓取 不用調整")]
    public float defaultJumpHeight;
    public Vector3 lastPos; // 前一個位置

    [Header("Health")]
    public int maxHealth = 3;
    public int cur_health;
    public int life = 3;
    public bool dead = false;
    bool invincible; // 無敵

    [Header("分數")]
    public int score;

    [Header("元件")]
    [SerializeField] StepAtk sAtk;
    [SerializeField] Transform feet;
    Animator m_anim;
    Rigidbody2D m_rg2d;
    Collider2D m_col2d;
    AudioSource m_aS;

    [SerializeField] PauseSystem pS;    // 暫停系統
    [SerializeField] DataManager dM;    // 資料管理
    [SerializeField] ScenesLoader sL;   // 場景切換
    [SerializeField] AudioSource aC;    // 音樂
    [SerializeField] FinishUI fUI;      // 通關UI

    [Header("音效")]
    public AudioClip hurtByEnemy;
    public AudioClip fallen;

    bool facingRight; // 保險用 隨時可刪除

	void Awake()
	{
		m_rg2d = gameObject.GetComponent<Rigidbody2D>();
        m_anim = gameObject.GetComponent<Animator>();
        m_col2d = gameObject.GetComponent<Collider2D>();
        m_aS = gameObject.GetComponent<AudioSource>();
        pS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseSystem>();
        dM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DataManager>();
        sL = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScenesLoader>();
        aC = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        fUI = GameObject.Find("FinishUI").GetComponent<FinishUI>();

        cur_health = maxHealth;
        defaultJumpHeight = jumpHeight;
    }

    private void Start()
    {
        if (!debugMode) // 如果不是 Debug 模式才讀取資料
        {
            dM.Load_PlayerData_From_InGameData();
        }
    }

    void Update()
    {
        if (pS.gS == PauseSystem.GameState.Going) // 如果 PauseSystem 下的 GameState 狀態為 GameState.Going 就代表可操作
        {
            if (!dead)
            {
                Move();
                Flip();
                Dead();
            }
            Cur_health_Fixed();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyDamage" && !invincible && !dead) // 受傷
        {
            PGotHit(col.gameObject.GetComponent<TransformRecord>().lastPos.y);
        }

        if (col.gameObject.tag == "DeadZone" && !dead) // 觸碰死亡區
        {
            cur_health -= 99;
            m_aS.clip = fallen;
            m_aS.Play();
            Dead();
        }

        if (col.gameObject.tag == "TrapDamage" && !invincible && !dead)
        {
            cur_health -= 1;
            invincible = true;
            m_anim.SetTrigger("Hurt");
        }

        if (col.gameObject.tag == "Finish" && !dead) // 過關
        {
            fUI.FinishGame();                       // 呼叫 FnishUI 的 FnishGame 副程式
            score += fUI.intBonusScore;             // 分數加上 FinishUI 裡面設定的關卡獎勵分數
            dM.Save_PlayerData_To_InGameData();     // 儲存資料
            pS.FinishGame_Or_PauseGame();           // 停止玩家操作

            sL.Invoke("LoadScene", 1f);
        }
    }

    private void LateUpdate()   // 紀錄上個位置
    {
        lastPos = feet.position;
    }

    #region Update
    void Move()
    {
        #if UNITY_STANDALONE_WIN
            float moveInput = Input.GetAxisRaw("Horizontal");
            m_rg2d.velocity = new Vector2(speed * moveInput, m_rg2d.velocity.y);

#elif (UNITY_IOS || UNITY_ANDROID)
            // Android
            float move;
            move = Input.acceleration.x * 2f;

            if (move > 1)
                move = 1;
            else if (move < -1)
                move = -1;

            m_rg2d.velocity = new Vector2(speed * move, m_rg2d.velocity.y);

#endif

        Debug.Log(m_rg2d.velocity);
    }

    void Flip()
    {
        #if UNITY_STANDALONE_WIN
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                facingRight = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                facingRight = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

        #elif (UNITY_IOS || UNITY_ANDROID)
            // Android
            if (Input.acceleration.x > 0)
            {
                facingRight = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.acceleration.x < 0)
            {
                facingRight = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        #endif
    }

    void Cur_health_Fixed()
    {
        if (cur_health >= 3)
            cur_health = 3;
        else if (cur_health <= 0)
            cur_health = 0;
    }

    #endregion

    void NotInvincible()
    {
        invincible = false;
    }

#region interface
    //受傷 (當主角的腳的Y 小於 敵人的頭的Y 會 受傷)
    public void PGotHit(float enemyHeadY)
    {
        if (lastPos.y < enemyHeadY && sAtk.stepInvincible)
        {
            cur_health -= 1;
            invincible = true;
            m_anim.SetTrigger("Hurt");
            m_aS.clip = hurtByEnemy;
            m_aS.Play();
        }
    }
    //怪物受傷(Interface引用)
    public void EGotHit() { }
    //死亡
    public void Dead()
    {
        if (cur_health <= 0)
        {
            dead = true;
            m_col2d.enabled = false;
            m_rg2d.velocity = new Vector3(0, 10f, 0);
            transform.Rotate(Vector3.right, 180);
            aC.Stop();
            life -= 1;

            dM.Save_PlayerData_To_InGameData();
            sL.Invoke("ReloadScene", 1f);
            //sL.ReloadScene();

            if (life < 0)
            {
                sL.Invoke("BackToMain", 1f); // 暫時
            }
        }
    }
#endregion

}

[System.Serializable]
public class PlayerData
{
    public int score;
    public int highScore;
    public int life;
}
