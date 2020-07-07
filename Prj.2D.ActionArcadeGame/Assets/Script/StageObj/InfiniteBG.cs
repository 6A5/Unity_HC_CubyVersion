using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBG : MonoBehaviour
{
    /*
    [SerializeField] Camera mainCamera; // 攝影機物件
    [SerializeField] Vector3 lastCamPos; // 攝影機前一個位置
    [SerializeField] float disWithCam; // 與攝影機的 X 距離
    [Tooltip("請設置負數")]
    [SerializeField] float rollingSpeed; // 滾動速度
    public float camGo;

    private void Start()
    {
        mainCamera = Camera.main;
        lastCamPos = mainCamera.transform.position;
    }

    void Update()
    {
        camGo = Mathf.Abs(mainCamera.transform.position.x - lastCamPos.x); // Cam 前進距離 ( 上一次 - 目前 )
        transform.position += new Vector3(camGo * rollingSpeed, 0, 0); // 移動圖片
    }

    private void LateUpdate()
    {
        print(mainCamera.transform.position);

        lastCamPos = mainCamera.transform.position; // 紀錄攝影機位置

        print((mainCamera.transform.position - lastCamPos).x);
    }
    */

    private float length, startPos;
    public GameObject cam;
    public float parallaxEffect; // 視差 數字越接近 1 越黏攝影機 越接近 0 向後速度越快

    private void Start()
    {
        startPos = transform.position.x;    // 初始點
        length = GetComponent<SpriteRenderer>().bounds.size.x; // 圖片寬度
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect)); 
        float dist = (cam.transform.position.x * parallaxEffect);       

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z); 

        if (temp > startPos + length) startPos += length; 
        else if (temp < startPos - length) startPos -= length;
    }
}
