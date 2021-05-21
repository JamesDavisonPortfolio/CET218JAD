using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoreScript : MonoBehaviour
{
    public GameObject laserForm;
    public GameObject bladeForm;
    public GameObject laserInterim;
    public GameObject bladeInterim;
    public void SwapToLaserFromBlade()
    {
        bladeForm.SetActive(false);
        bladeInterim.SetActive(true);
        bladeInterim.GetComponent<Animator>().SetTrigger("ToLaser");
    }

    public void SwapToLaserFromMid()
    {
        bladeInterim.SetActive(false);
        laserInterim.SetActive(true);
        laserInterim.GetComponent<Animator>().SetTrigger("ToLaser");
    }
    public void SwapToBlade()
    {
        laserForm.SetActive(false);
        laserInterim.SetActive(true);
        laserInterim.GetComponent<Animator>().SetTrigger("ToBlade");
    }

    public void SwapToBladeFromMid()
    {
        laserInterim.SetActive(false);
        bladeInterim.SetActive(true);
        bladeInterim.GetComponent<Animator>().SetTrigger("ToBlade");
    }
    
    public void ActivateLaser()
    {
        laserInterim.SetActive(false);
        laserForm.SetActive(true);
    }

    public void ActivateBlades()
    {
        bladeInterim.SetActive(false);
        bladeForm.SetActive(true);
    }
}
