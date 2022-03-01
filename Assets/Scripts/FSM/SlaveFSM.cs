using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Author: Muhammad Rifdi bin Sabbri
 * Created: 27/2/2022
 */

public class SlaveFSM : CFSM
{
    [SerializeField]
    private GameObject Body;
    [SerializeField]
    private CageInteraction Cage;
    private NavMeshAgent agent;

    [SerializeField]
    private Transform xPointMin, xPointMax, zPointMin, zPointMax;
    private Vector3 goal;
    private bool SetPatrolPoint = false;

    // Update is called once per frame
    private void Awake()
    {
        agent = Body.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (CurrentFSM)
        {
            case FSM.IDLE:
                agent.velocity *= 0;
                if (FSMCounter > 2 && Cage.doorOpen)
                {
                    CurrentFSM = FSM.PATROL;
                    FSMCounter = 0;
                }
                FSMCounter += Time.deltaTime;
                break;
            case FSM.PATROL:
                if (FSMCounter > 3)
                {
                    SetPatrolPoint = false;
                    CurrentFSM = FSM.IDLE;
                    FSMCounter = 0;
                }
                else if (!SetPatrolPoint)
                {
                    goal = new Vector3(Random.Range(xPointMin.position.x, xPointMax.position.x), xPointMin.position.y, Random.Range(zPointMin.position.z, zPointMax.position.z));
                    agent.destination = goal;
                    SetPatrolPoint = true;
                }
                FSMCounter += Time.deltaTime;
                break;
            default:
                break;

        }
    }
}
