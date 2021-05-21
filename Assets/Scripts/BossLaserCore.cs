using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserCore : MonoBehaviour
{
    public SecondFormScript controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Smashed()
    {
        controller.coreCount--;
        gameObject.SetActive(false);
    }
}
