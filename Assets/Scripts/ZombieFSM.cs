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
    [SerializeField]
    private int DetectionRange, AttackRange;
    private int Attack;
    private float DistanceBtwPlayer;
    private Vector3 goal;
    private bool SetPatrolPoint = false;

    // Update is called once per frame
    private void Awake()
    {
        zombieAnimation = Body.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Attack = Random.Range(1, 3);
    }

    private void Update()
    {
        DistanceBtwPlayer = Vector3.Distance(transform.position, Player.GetComponent<Transform>().position);
        switch (CurrentFSM)
        {
            case FSM.IDLE:
                zombieAnimation.Play("Idle");
                agent.velocity *= 0;
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
                if (DistanceBtwPlayer <= DetectionRange)
                {
                    CurrentFSM = FSM.ATTACK;
                    FSMCounter = 1;
                }
                FSMCounter += Time.deltaTime;
                break;
            case FSM.ATTACK:    
                if (!zombieAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack" + Attack))
                {
                    if (DistanceBtwPlayer <= AttackRange && FSMCounter > 1)
                    {
                        agent.destination = Player.transform.position;
                        Attack = Random.Range(1, 3);
                        zombieAnimation.Play("Attack" + Attack);
                        agent.velocity += (Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward).normalized * 10;
                        FSMCounter = 0;
                    }
                    else if (FSMCounter > 1)
                    {
                        agent.destination = Player.transform.position;
                        zombieAnimation.Play("Walk");
                        agent.speed = 10;
                    }
                    else
                    {
                        zombieAnimation.Play("Idle");
                    }
                    if (DistanceBtwPlayer > DetectionRange)
                    {
                        agent.speed = 5;
                        CurrentFSM = FSM.IDLE;
                        FSMCounter = 0;
                    }
                   
                }
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
