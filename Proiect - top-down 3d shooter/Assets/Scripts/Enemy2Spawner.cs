using System.Collections;
using UnityEngine;
using System;
using System.Threading;

using Random = UnityEngine.Random;

public class Enemy2Spawner : MonoBehaviour
{
    [SerializeField] public GameObject Enemy2Prefab;
    
    public static Enemy2Spawner Instance;

    void Awake()
    {
        Instance = GetComponent<Enemy2Spawner>();
    }


    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));

        GameObject enemy = Instantiate(Enemy2Prefab, spawnPosition, Quaternion.identity);
        //enemy.GetComponent<Canvas>().enabled = true;
        enemy.transform.Find("Canvas").Find("Health bar").gameObject.GetComponent<HealthBar>().fill.fillAmount = 1;
        //enemy.GetComponent<HealthBar>().fill.fillAmount = 1;
        // Destroy(enemy, lifetime);

        yield return new WaitForSeconds(7);

        if (PlayerScript.isAlive)
        {
            StartCoroutine(Spawn());
        }
    }
}