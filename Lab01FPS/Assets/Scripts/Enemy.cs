using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent agent;
    public GameObject player;
    [SerializeField] GameObject m1;
    [SerializeField] GameObject m2;
    [SerializeField] GameObject m3;
    [SerializeField] GameObject m4;
    [SerializeField] GameObject m5;
    [SerializeField] GameObject mflash;
    private bool isFlashing = false;
    private float flashTime = 0.05f;
    private int ran;
    private float gunCooldown = 5f;
    private bool gunCD = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        decideMarker();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5)
            decideMarker();
        NavMeshHit hit;
        if(!agent.Raycast(player.transform.position, out hit))
        {
            Shoot();
        }
        if(isFlashing)
        {
            flashTime -= Time.deltaTime;
            if (flashTime <= 0)
            {
                mflash.SetActive(false);
                isFlashing = false;
                flashTime = 0.05f;
            }
        }
        if(!gunCD)
        {
            gunCooldown -= Time.deltaTime;
            if (gunCooldown <= 0)
            {
                gunCD = true;
                gunCooldown = 5f;
            }
        }
    }

    void decideMarker()
    {
        ran = Random.Range(1, 6);
        if (ran == 1)
            agent.SetDestination(m1.transform.position);
        else if (ran == 2)
            agent.SetDestination(m2.transform.position);
        else if (ran == 3)
            agent.SetDestination(m3.transform.position);
        else if (ran == 4)
            agent.SetDestination(m4.transform.position);
        else
            agent.SetDestination(m5.transform.position);
    }

    void Shoot()
    {
        if (gunCD)
        {
            mflash.SetActive(true);
            isFlashing = true;
            player.GetComponent<PlayerController>().Damage(5);
            gunCD = false;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
