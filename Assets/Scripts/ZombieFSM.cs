using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 20/2/2022
 */


public class ZombieFSM : CFSM
{
    [SerializeField]
    private GameObject Body, Player;
    private Animator zombieAnimation;
    private NavMeshAgent agent;

    [SerializeField]
    private Transform xPointMin, xPointMax, zPointMin, zPointMax;
    private Vector3 goal;
    private bool IsMoving = false;


    // Update is called once per frame
    private void Awake()
    {
        zombieAnimation = Body.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            //if (other.lay)
            //triggerActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            //triggerActive = false;
        }
    }

    private void Update()
    {
        switch (CurrentFSM)
        {
            case FSM.IDLE:
                zombieAnimation.Play("Idle");
                if (FSMCounter > 2)
                {
                    CurrentFSM = FSM.PATROL;
                    FSMCounter = 0;
                }
                FSMCounter += Time.deltaTime;
                break;
            case FSM.PATROL:
                if (transform.position == goal)
                    zombieAnimation.Play("Idle");
                else
                    zombieAnimation.Play("Walk");
                if (FSMCounter > 3)
                {
                    IsMoving = false;
                    CurrentFSM = FSM.IDLE;
                    FSMCounter = 0;
                }
                else if (!IsMoving)
                {
                    goal = new Vector3(Random.Range(xPointMin.position.x, xPointMax.position.x), xPointMin.position.y, Random.Range(zPointMin.position.z, zPointMax.position.z));
                    agent.destination = goal;
                    IsMoving = true;
                }
                FSMCounter += Time.deltaTime;
                break;
            case FSM.ATTACK:
                FSMCounter += Time.deltaTime;
                break;
            default:
                break;

        }
    }

    private void FixedUpdate()
    {
        //if (m_Rigidbody.velocity.sqrMagnitude > maxVelocity)
        //{
        //    //smoothness of the slowdown is controlled by the 0.99f, 
        //    //0.5f is less smooth, 0.9999f is more smooth
        //    m_Rigidbody.velocity *= 0.8f;
        //}
        
    }
}
