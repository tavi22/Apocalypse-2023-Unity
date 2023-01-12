using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    [SerializeField]
    Transform target;                       // target object 

    [SerializeField]
    float movementSpeed = 2f;               // enemy movement speed

    static float distanceToPlayer=10.0f;          // distance between enemy and target

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private ScoreManager playerScoreManager;
    public Canvas playerCanvas;
    private bool pointAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //playerScoreManager = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreManager>();
        playerScoreManager = playerCanvas.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);

        MoveEnemy();
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        
        // daca inamicul a murit, ii oprim miscarea (ca sa nu mai vina spre player in timpul animatiei de moarte)
        if (currentHealth <= 0) 
        {
            movementSpeed = 0.0f;
            if (pointAdded == false)
            {
                playerScoreManager.AddPoint();
                pointAdded = true; 
            }
            
        }
    }

    void MoveEnemy()
    {
        RotateEnemy();
        transform.position += transform.forward * movementSpeed * Time.deltaTime;       // chasing the target
    }
    
    void RotateEnemy()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));         // rotate enemy to face the target
    }

    public static bool isClose()
    {
        if (distanceToPlayer < 5.0f)
            return true;
        else
            return false;
    }

    public bool isDead() 
    {
        if (currentHealth <= 0)
            return true;
        else
            return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            TakeDamage(40);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}