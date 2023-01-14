using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_up : MonoBehaviour
{   
    public Health_up parent;
    public GameObject pickupEffect;
    private PlayerScript playerscript;
    private ScoreManager scoreManager;
    public GameObject player;
    private Canvas canvas;
    public MeshRenderer renderer;
    private bool hasRespawned = false;
    private int oldScore=0;

    public float duration = 1;
    // Start is called bef
    // ore the first frame update
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Score").GetComponent<Canvas>();
        scoreManager = canvas.GetComponent<ScoreManager>();
    }
    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
           Pickup();
            
        }
    }

    void Pickup()
    {
         Instantiate(pickupEffect, transform.position, transform.rotation);
        
        
        playerscript = player.GetComponent<PlayerScript>();
        playerscript.currentHealth = 100;



         renderer.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        hasRespawned = false;
        

    }
    

    private void Update()
    {
        if (scoreManager.score % 10 == 0 && scoreManager.score > 0 && hasRespawned == false && scoreManager.score!=oldScore)
        {
            hasRespawned = true;
            Debug.Log(scoreManager.score);
            //gameObject.SetActive(true);
            renderer.enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
            oldScore = scoreManager.score;
        }
    }
}
