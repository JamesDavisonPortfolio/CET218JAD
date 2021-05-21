using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public int maxHealth;
    public GameObject initialForm;
    public GameObject firstForm;
    public GameObject secondForm;
    public GameObject player;
    public GameObject[] pods;
    public Transform deffensePoint;
    public Vector3 playerStart;
    public Transform[] teleportPoints;
    public int podCount;
    


    private int currentForm;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentForm = 0;
        podCount = pods.Length;
        player = GameObject.FindGameObjectWithTag("Player");
        playerStart = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(int damage)
    {
        currentHealth += damage;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void Heal()
    {
        currentHealth += (int)(maxHealth * 0.1);
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void PodDestroyed()
    {
        podCount--;
        if (podCount <= 0)
        {
            currentForm++;
            
            ChangeForm(currentForm);
        }
    }

    void ChangeForm(int form)
    {
        switch (form)
        {
            case 1:
                initialForm.SetActive(false);
                firstForm.SetActive(true);
                podCount = pods.Length;
                for (int i = 0; i < pods.Length; i++)
                {
                    pods[i].SetActive(true);
                }
                player.transform.position = playerStart;
                break;
            case 2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
}
