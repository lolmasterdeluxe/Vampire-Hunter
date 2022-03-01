using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Muhammad Rifdi bin Sabbri 
 * Created: 26/2/2022
 */

public class CameraLockOn : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    [SerializeField]
    private Cinemachine.CinemachineTargetGroup TargetGroup;
    private Cinemachine.CinemachineTargetGroup.Target CinemachineTarget;
    [SerializeField]
    private GameObject FollowCamera, LockOnCamera;
    private GameObject TargetEnemy = null;
    private GameObject[] Enemies;
    [SerializeField]
    private float LockOnSpeed = 1f, DetectionRange = 10;
    private float targetDistance = 0, ScaleDistance;
    [SerializeField]
    private RectTransform TargetIcon;
    [HideInInspector]
    public bool LockOn = false, TargetLockOn = false, LockedOn = false;
    private Coroutine LookCoroutine;
    private Quaternion lookRotation;

    private void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    private void Update()
    {
        FindClosestEnemy();
        if (TargetLockOn)
        {
            //StartCoroutine(LookAt());
            transform.LookAt(TargetEnemy.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            TargetIcon.localPosition = new Vector3(TargetEnemy.transform.position.x, TargetEnemy.transform.position.y, TargetEnemy.transform.position.z);
            TargetIcon.transform.LookAt(transform);
            TargetIcon.transform.eulerAngles = new Vector3(0, TargetIcon.transform.eulerAngles.y, 0);
            ScaleDistance = (Vector3.Distance(TargetEnemy.transform.position, transform.position) / 5);
            if (ScaleDistance < 1)
                ScaleDistance = 1;
            TargetIcon.localScale = new Vector3(ScaleDistance, ScaleDistance, ScaleDistance);
        }
    }

    private void StartRotating()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }
        LookCoroutine = StartCoroutine(LookAt());
    }
    private IEnumerator LookAt()
    {
        lookRotation = Quaternion.LookRotation(TargetEnemy.transform.position - transform.position);
        lookRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * LockOnSpeed;
            yield return null;
        }
    }

    public void FindClosestEnemy()
    {
        float distance = DetectionRange;
        if (TargetEnemy != null)
            targetDistance = Vector3.Distance(TargetEnemy.transform.position, transform.position);
        if (!LockOn)
        {
            foreach (GameObject Enemy in Enemies)
            {
                float curDistance = Vector3.Distance(Enemy.transform.position, transform.position);
                if (curDistance < distance && RenderExtension.IsVisibleFrom(Enemy.GetComponentInChildren<Renderer>(), MainCamera) && Enemy.GetComponentInChildren<Health>().hp > 0)
                {
                    CinemachineTarget.target = Enemy.transform;
                    CinemachineTarget.weight = 1;
                    CinemachineTarget.radius = 2;
                    TargetEnemy = Enemy;
                    distance = curDistance;
                    for (int i = 0; i < TargetGroup.m_Targets.Length; i++)
                    {
                        if (TargetGroup.m_Targets[i].target == null || TargetGroup.m_Targets[i].target.CompareTag("Enemy"))
                        {
                            TargetGroup.m_Targets.SetValue(CinemachineTarget, i);
                        }
                    }
                    if (Input.GetKeyUp("f"))
                    {
                        LockOn = true;
                        TargetLockOn = true;
                        FollowCamera.SetActive(false);
                        LockOnCamera.SetActive(true);
                        TargetIcon.gameObject.SetActive(true);
                        return;
                    }
                }
            }
        }
        if (((targetDistance > DetectionRange + 5) || (LockOn && (Input.GetKeyUp("f")))) || (CinemachineTarget.target != null && CinemachineTarget.target.GetComponentInChildren<Health>().hp <= 0) || GetComponentInChildren<Animator>().GetBool("Die") || Input.GetKeyUp("i"))
        {
            for (int i = 0; i < TargetGroup.m_Targets.Length; i++)
            {
                if (TargetGroup.m_Targets[i].target == CinemachineTarget.target)
                {
                    targetDistance = 0;
                    TargetGroup.m_Targets.SetValue(null, i);
                    LockOn = false;
                    TargetLockOn = false;
                    FollowCamera.SetActive(true);
                    LockOnCamera.SetActive(false);
                    TargetIcon.gameObject.SetActive(false);
                    Enemies = GameObject.FindGameObjectsWithTag("Enemy");
                }
            }
        }
    }

}
