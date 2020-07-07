using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRecord : MonoBehaviour
{
    [SerializeField] GameObject head;
    public Vector3 lastPos;

    private void LateUpdate()
    {
        lastPos = head.transform.position; // 紀錄上個位置
    }
}
