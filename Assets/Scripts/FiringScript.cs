using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class FiringScript : MonoBehaviour
{
    public LineRenderer laser;
    public ParticleSystem particles;
    public Animator firingAnimatior;
    public GameObject volume;
    public GameObject normalSight;
    public GameObject redSight;
    public Volume volumePP;
    public AudioSource poweringSound;
    public AudioSource firingSound;
    public AudioSource firingBoom;
    public float rotationSpeed;
    public LayerMask ignoreLayers;
    public Transform firePoint;

    bool firing;
    bool laserOn;
    // Start is called before the first frame update
    void Start()
    {
        firing = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitObject;
        if (!firing)
        {
            Camera.main.transform.Rotate(Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime, 0, 0);
            Camera.main.transform.Rotate(0, Input.GetAxis("Mouse X") * -rotationSpeed * Time.deltaTime, 0);

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitObject, 200, ~ignoreLayers))
            {
                transform.LookAt(hitObject.point);
                Debug.DrawLine(Camera.main.transform.position, hitObject.point);
                if (hitObject.collider.CompareTag("BossPod"))
                {
                    redSight.SetActive(true);
                    normalSight.SetActive(false);
                }
                else
                {
                    redSight.SetActive(false);
                    normalSight.SetActive(true);
                }
            }
        } 
        if (!firing && Input.GetKeyDown(KeyCode.Mouse0) && transform.root.GetComponent<PlayerController>().charges > 0)
        {
            transform.root.GetComponent<PlayerController>().ReduceCharges();
            firing = true;
            firingAnimatior.SetTrigger("Fire");
        }
        
    }

    public IEnumerator FireLaser()
    {
        Debug.Log("Start of fire");
        poweringSound.Stop();
        RaycastHit hitObject;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitObject, 200, ~ignoreLayers))
        {
            Debug.Log("FireLaser Running");
            transform.LookAt(hitObject.point);
            laser.enabled = true;
            laser.SetPosition(0, firePoint.position);
            laser.SetPosition(1, hitObject.point);
            volume.SetActive(true);
            firingBoom.Play();
            firingSound.Play();
            StartCoroutine("ActivatePPE");
            yield return new WaitForSeconds(0.4f);
            StartCoroutine("DeactivatePPE");
            Debug.Log(hitObject.collider.tag.ToString());
            Debug.Log(hitObject.collider.name.ToString());
            if (hitObject.collider.CompareTag("BossPod"))
            {
                Debug.Log("HitPod");
                hitObject.collider.SendMessageUpwards("ShotDown", SendMessageOptions.DontRequireReceiver);
            }
            yield return new WaitForSeconds(0.4f);
            firingSound.Stop();
            volume.SetActive(false);
            laser.enabled = false;
            firing = false;
            Debug.Log("FireLaser Stopping");
        }
    }        

    public void ActivateParticles()
    {
        particles.Play();
        poweringSound.Play();
    }

    IEnumerator ActivatePPE()
    {
        //volumePP.profile.TryGet<Vignette>(out var vignette);
        //vignette.intensity.overrideState = true;
        for(int i = 0; i < 40; i++)
        {
            volumePP.weight += 0.025f;
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    IEnumerator DeactivatePPE()
    {
        //volumePP.profile.TryGet<Vignette>(out var vignette);
        //vignette.intensity.overrideState = true;
        for (int i = 0; i < 40; i++)
        {
            volumePP.weight -= 0.025f;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
