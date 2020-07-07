using UnityEngine;
using System.Collections;

public class CamConTest : MonoBehaviour
{
    bool draw = false;
    bool gyinfo;
    Gyroscope go;
    void Start()
    {
        gyinfo = SystemInfo.supportsGyroscope;
        go = Input.gyro;
        go.enabled = true;
    }
    void Update()
    {
        if (gyinfo)
        {
            Vector3 a = go.attitude.eulerAngles;
            a = new Vector3(-a.x, -a.y, a.z); //直接使用读取的欧拉角发现不对，于是自己调整一下符号  
            print(a);
            this.transform.eulerAngles = a;
            this.transform.Rotate(Vector3.right * 90, Space.World);
            draw = false;
        }
        else
        {
            draw = true;
        }
    }

    void OnGUI()
    {
        if (draw)
        {
            GUI.Label(new Rect(100, 100, 100, 30), "启动失败");
        }
    }

}
