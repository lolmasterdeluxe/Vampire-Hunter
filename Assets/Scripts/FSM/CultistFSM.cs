using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 21/2/2022
 */

public class CultistFSM : CFSM
{
    [SerializeField]
    private GameObject Body, Player;
    private Animator cultistAnimation;
    private NavMeshAgent agent;

    [SerializeField]
    private Transform xPointMin, xPointMax, zPointMin, zPointMax;
    [SerializeField]
    private int DetectionRange, AttackRange, AttackCooldown;
    private float DistanceBtwPlayer, DistanceToGoal;
    private Vector3 goal;
    private bool SetPatrolPoint = false;

    private bool comboPossible, lunge = false;
    private int comboStep = 0, attack_type;

    private void Awake()
    {
        cultistAnimation = GetComponentInChildren<Animator>();
        agent = Body.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (cultistAnimation.GetCurrentAnimatorStateInfo(0).IsName("Flinch"))
            return;
        DistanceBtwPlayer = Vector3.Distance(Body.transform.position, Player.GetComponent<Transform>().position);
        DistanceToGoal = Vector3.Distance(Body.transform.position, goal);
        switch (CurrentFSM)
        {
            case FSM.IDLE:
                cultistAnimation.Play("Idle");
                agent.velocity *= 0;
                if (FSMCounter > 2)
                {
                    CurrentFSM = FSM.PATROL;
                    FSMCounter = 0;
                }
                FSMCounter += Time.deltaTime;
                break;
            case FSM.PATROL:
                if (DistanceToGoal <= 1)
                    cultistAnimation.Play("Idle");
                else
                    cultistAnimation.Play("Walk");
                if (FSMCounter > 5)
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
                    FSMCounter = AttackCooldown;
                }
                FSMCounter += Time.deltaTime;
                break;
            case FSM.ATTACK:
                if (FSMCounter > AttackCooldown)
                {
                    if (DistanceBtwPlayer <= AttackRange)
                    {
                        //Debug.Log("Attack Type: " + attack_type);
                        Attack_();
                    }
                    else if (DistanceBtwPlayer >= AttackRange)
                    {
                        cultistAnimation.SetTrigger("ReturnToWalk");
                        comboStep = 0;
                        agent.destination = Player.transform.position;
                        attack_type = Random.Range(1, 3);
                        agent.speed = 10;
                        Debug.Log("Walk");
                    }
                }   
                else
                {
                    cultistAnimation.Play("Idle");
                    Debug.Log("Idle");
                }
                if (DistanceBtwPlayer > DetectionRange)
                {
                    agent.speed = 5;
                    CurrentFSM = FSM.IDLE;
                    FSMCounter = 0;
                }
                if (FSMCounter > 15)
                    FSMCounter = 0;
                FSMCounter += Time.deltaTime;
                break;
            default:
                break;

        }
    }
    private void FixedUpdate()
    {
        if (lunge)
        {
            agent.destination = Player.transform.position;
            agent.velocity += (Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward).normalized * 5;
            lunge = false;
        }
    }
    public void Attack_()
    {
        if (comboStep == 0)
        {
            agent.velocity *= 0;
            cultistAnimation.Play("Attack" + attack_type + "_1");
            comboStep = 1;
            return;
        }
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }
    public void Combo()
    {
        if (comboStep == 2)
        {
            agent.velocity *= 0;
            cultistAnimation.Play("Attack" + attack_type + "_2");
        }
        if (comboStep == 3)
        {
            agent.velocity *= 0;
            cultistAnimation.Play("Attack" + attack_type + "_3");
        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
        FSMCounter = 0;
        Debug.Log("Reset");
    }

    public void Lunge()
    {
        lunge = true;
    }
}
