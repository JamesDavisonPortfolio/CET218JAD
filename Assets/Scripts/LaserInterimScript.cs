using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInterimScript : MonoBehaviour
{
    public SpawnCoreScript spawnCore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToLaser()
    {
        spawnCore.ActivateLaser();
    }

    public void ToBlades()
    {
        spawnCore.SwapToBladeFromMid();
    }
}
