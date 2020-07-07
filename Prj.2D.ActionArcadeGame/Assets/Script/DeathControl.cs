using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour
{
    public static DeathControl instance;

    public bool playerDeath;
    [SerializeField] AudioSource As;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (playerDeath)
        {
            As.Stop();
            Invoke("SceneReload", 2f);
        }
    }

    void SceneReload()
    {

    }
}
