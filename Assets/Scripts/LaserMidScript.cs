using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMidScript : MonoBehaviour
{
    // Start is called before the first frame update
    public FiringScript firingScript;

    public void Particles()
    {
        firingScript.ActivateParticles();
    }

    public void Fire()
    {
        firingScript.StartCoroutine(firingScript.FireLaser());
    }
}
