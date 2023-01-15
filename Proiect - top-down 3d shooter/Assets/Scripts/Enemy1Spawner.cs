using System;
using System.Collections;
using System.Threading;
using UnityEngine;

using Random = UnityEngine.Random;

public class Enemy1Spawner : MonoBehaviour
{
    [SerializeField] public GameObject Enemy1Prefab;

    Transform player;

    public static Enemy1Spawner Instance;

    void Awake()
    {
        Instance = GetComponent<Enemy1Spawner>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float ranX = Random.Range(-25, 25);
        float ranZ = Random.Range(-25, 25);

        if (ranX > -15 && ranX < 0)
        {
            ranX = -15;
        }
        else if (ranX < 15 && ranX >= 0)
        {
            ranX = -15;
        }

        if (ranZ > -15 && ranZ < 0)
        {
            ranZ = -15;
        }
        else if (ranZ < 15 && ranZ >= 0)
        {
            ranZ = -15;
        }


        Vector3 spawnPosition = new Vector3(player.position.x + ranX, 0.5f, player.position.x + ranZ);

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