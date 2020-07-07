using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameBorder;
    [SerializeField] float followRange = 1f;
    [SerializeField] float smoothSpeed = 2.5f;
    [SerializeField] bool marioMode;
    bool notSetPlayer;

    private void Start()
    {
        try
        {
            gameBorder.GetComponent<BoxCollider2D>().size = new Vector2(1f, Camera.main.orthographicSize * 4);
        }
        catch
        {
            Debug.Log("Not set 'gameBorder' gameobject");
        }
    }

    private void Update()
    {
        //檢查有沒有設置玩家
        if (!notSetPlayer)
        {
            try
            {   //如果是MarioModeCamera，玩家只能往右跑。
                if (player.transform.position.x + followRange > transform.position.x && marioMode)
                {
                    transform.position = new Vector3(player.transform.position.x + followRange, transform.position.y, transform.position.z);
                }
                //如果不是MarioModeCamera，玩家可以左右跑。
                else if (Mathf.Abs(transform.position.x - player.transform.position.x) > followRange && !marioMode)
                {
                    transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, smoothSpeed * Time.deltaTime),
                        transform.position.y, transform.position.z);
                }
            }
            catch
            {   //通常情況下是Player變數沒有設置，會跑下來這條。
                Debug.Log("Player gameobject is Null, Set 'player' gameobject.");
                notSetPlayer = true;
            }
        }
    }
}
