using System;
using System.Collections;
using System.Threading;
using UnityEngine;

using Random = UnityEngine.Random;

public class Enemy1Spawner : MonoBehaviour
{
    [SerializeField] public GameObject Enemy1Prefab;

    public static Enemy1Spawner Instance;

    void Awake()
    {
        Instance = GetComponent<Enemy1Spawner>();
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));

        GameObject enemy = Instantiate(Enemy1Prefab, spawnPosition, Quaternion.identity);
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