using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{

    private Animator animator;
    private NavMeshAgent agent;
    private List<GameObject> players;


    private enum states
    {
        Idle,
        Chasing,
        Attack,
    }

    private states actualState = states.Idle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (actualState)
        {
            case states.Idle:
                try
                {
                    players = GameObject.FindGameObjectsWithTag("Player").ToList();
                    if (players.Count >0)
                    {
                        actualState = states.Chasing;
                    }
                }
                catch
                {
                    Debug.Log("Can't find players");
                }
                break;
            case states.Chasing:
                
                players = players.OrderByDescending(x => (x.transform.position - transform.position).sqrMagnitude).ToList();
                agent.destination = players[0].transform.position;
                
                
                break;
        }
    }
}
