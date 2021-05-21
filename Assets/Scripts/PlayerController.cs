using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float maxHealth;
    public float charges;
    public float maxCharges;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI chargeText;

    public GameObject reticule;

    public GameObject sword;
    public GameObject cannon;

    public bool damaged;

    float currentHealth;
    
    Rigidbody rb;
    float vert, horz = 0;
    Vector3 moveDir;
    Quaternion cameraRotation;
    // Start is called before the first frame update
    void Awake()
    {
        charges = 0;
        cameraRotation = Camera.main.transform.rotation;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraRotation = Camera.main.transform.localRotation;
        chargeText.SetText("Charges: " + charges.ToString());
        healthText.SetText("Health: " + currentHealth.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(cannon.activeSelf != true)
        {
            Movement();
        }
        if(damaged == false)
        {
            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(cannon.activeSelf == true)
            {
                cannon.SetActive(false);
                sword.SetActive(true);
                reticule.SetActive(false);
            }
            else if(sword.activeSelf == true)
            {
                sword.SetActive(false);
                cannon.SetActive(true);
                reticule.SetActive(true);
            }
            Camera.main.transform.localRotation = cameraRotation;
        }
    }

    void FixedUpdate()
    {
        float time = Time.fixedDeltaTime * 50;
        transform.Translate(moveDir * time * speed);
        
    }

    void Movement()
    {
        vert = 0;
        horz = 0;

        if (Input.GetKey(KeyCode.W))
            vert = 1;

        if (Input.GetKey(KeyCode.S))
            vert = -1;

        if (Input.GetKey(KeyCode.A))
            horz = -1;

        if (Input.GetKey(KeyCode.D))
            horz = 1;

        moveDir = new Vector3(horz, 0, vert);
        moveDir.Normalize();
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0);
        Camera.main.transform.Rotate(Input.GetAxis("Mouse Y") * -rotationSpeed * Time.deltaTime, 0, 0);
        RotationCap();
    }

    void RotationCap()
    {
        if (transform.rotation.z > 0 || transform.rotation.z < 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        }
    }

    public void TakeDamage(int damage, Vector3 pushDirection)
    {
        Debug.Log("Trigger E");
        if (!damaged)
        {
            Debug.Log("Trigger F");
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Death();
            }
            damaged = true;
            rb.AddForce(pushDirection);
            Invoke("StopForceAndDamager", 0.2f);
        }
        healthText.SetText("Health: " + currentHealth.ToString());
    }

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    void StopForceAndDamager()
    {
        rb.velocity = Vector3.zero;
        Invoke("StopInvincibility", 1);
    }

    void StopInvincibility()
    {
        damaged = false;
    }

    public void ChargeCannon()
    {
        charges++;
        if(charges > maxCharges)
        {
            charges = maxCharges;
        }
        chargeText.SetText("Charges: " + charges.ToString());
    }
    public void ReduceCharges()
    {
        charges--;
        if(charges < 0)
        {
            charges = 0;
        }
        chargeText.SetText("Charges: " + charges.ToString());
    }
}
