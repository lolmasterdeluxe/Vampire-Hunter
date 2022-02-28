using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Author: Muhammad Rifdi bin Sabbri
 * Created: 27/2/2022
 */
public class VampireFSM : CFSM
{
    [SerializeField]
    private GameObject Body, Player;
    private Animator vampireAnimation;
    private NavMeshAgent agent;

    [SerializeField]
    private Transform xPointMin, xPointMax, zPointMin, zPointMax;
    [SerializeField]
    private int DetectionRange, AttackRange, AttackCooldown;
    private float DistanceBtwPlayer;
    private Vector3 goal, lungeDir;
    private bool SetPatrolPoint = false, ChargeAttack_ = false, LockOnPlayer = false;
    private bool PlaySound = false;
    private bool comboPossible, lunge = false;
    private int comboStep = 0, attack_type;

    private void Awake()
    {
        vampireAnimation = GetComponentInChildren<Animator>();
        agent = Body.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (vampireAnimation.GetCurrentAnimatorStateInfo(0).IsName("Flinch"))
        {
            ComboReset();
            PlayAudio();
            FSMCounter = AttackCooldown;
            return;
        }
        else
            PlaySound = false;

        DistanceBtwPlayer = Vector3.Distance(Body.transform.position, Player.GetComponent<Transform>().position);
        switch (CurrentFSM)
        {
            case FSM.IDLE:
                vampireAnimation.Play("Idle");
                agent.velocity *= 0;
                if (FSMCounter > 2)
                {
                    CurrentFSM = FSM.PATROL;
                    FSMCounter = 0;
                }
                FSMCounter += Time.deltaTime;
                break;
            case FSM.PATROL:
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
                if (!ChargeAttack_)
                {
                    if (FSMCounter > AttackCooldown)
                    {
                        if (DistanceBtwPlayer <= AttackRange)
                        {
                            //Debug.Log("Attack Type: " + attack_type);
                            Attack_();
                        }
                        else if (DistanceBtwPlayer <= AttackRange + 5)
                        {
                            ChargeAttack();
                            ChargeAttack_ = true;
                        }
                        else if (DistanceBtwPlayer >= AttackRange)
                        {
                            vampireAnimation.SetTrigger("ReturnToWalk");
                            comboStep = 0;
                            agent.destination = Player.transform.position;
                            attack_type = Random.Range(1, 3);
                            agent.speed = 10;
                        }
                    }
                    else
                    {
                        vampireAnimation.Play("Idle");
                    }
                }
                if (DistanceBtwPlayer > DetectionRange)
                {
                    agent.speed = 5;
                    CurrentFSM = FSM.IDLE;
                    FSMCounter = 0;
                }
                if (FSMCounter > 15)
                {
                    ComboReset();
                }
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
            if (!LockOnPlayer)
            {
                agent.destination = Player.transform.position;
                agent.velocity += (Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward).normalized * 5;
                FindObjectOfType<AudioManager>().Play("vampireattack");
                lunge = false;
            }
            else
            {
                transform.parent.LookAt(Player.transform);
                lungeDir = Quaternion.Euler(0f, transform.parent.eulerAngles.y, 0f) * (Vector3.forward);
                transform.parent.GetComponent<Rigidbody>().AddForce((lungeDir.normalized * 1000));
                attack_type = Random.Range(1, 3);
                FindObjectOfType<AudioManager>().Play("vampireattack");
                LockOnPlayer = false;
                lunge = false;
            }
        }
    }
    public void Attack_()
    {
        if (comboStep == 0)
        {
            agent.velocity *= 0;
            vampireAnimation.Play("Attack1");
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

    public void ChargeAttack()
    {
        if (comboStep == 0)
        {
            vampireAnimation.Play("ChargeAttack" + attack_type);
            comboStep = 1;
            LockOnPlayer = true;
        }
        if (LockOnPlayer)
        {
            GetComponentInParent<Rigidbody>().isKinematic = false;
            GetComponentInParent<NavMeshAgent>().enabled = false;
            GetComponentInParent<Rigidbody>().velocity *= 0;
            transform.parent.LookAt(Player.transform);
            Debug.Log("LockOnPlayer");
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
            vampireAnimation.Play("Attack2");
        }
        if (comboStep == 3)
        {
            agent.velocity *= 0;
            vampireAnimation.Play("Attack3");
        }
        if (comboStep == 4)
        {
            agent.velocity *= 0;
            vampireAnimation.Play("Attack4");
        }
        if (comboStep == 5)
        {
            agent.velocity *= 0;
            vampireAnimation.Play("Attack5");
        }
        Debug.Log("Combo Step: " + comboStep);
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
        FSMCounter = 0;
        ChargeAttack_ = false;
        GetComponentInParent<Rigidbody>().isKinematic = true;
        GetComponentInParent<NavMeshAgent>().enabled = true;
        Debug.Log("Reset");
    }

    public void Lunge()
    {
        lunge = true;
    }

    private void PlayAudio()
    {
        if (!PlaySound)
        {
            PlaySound = true;
            FindObjectOfType<AudioManager>().Play("vampirehurt");
        }

    }
}
