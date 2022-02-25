using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    [SerializeField]
    private GameObject FollowCamera, LockOnCamera, Zombie;
    [SerializeField]
    private float LockOnSpeed = 1f;
    [HideInInspector]
    public bool LockOn = false, TargetLockOn = false;
    private Coroutine LookCoroutine;
    private Quaternion lookRotation;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            LockOn = !LockOn;
            TargetLockOn = !TargetLockOn;
            FollowCamera.SetActive(!FollowCamera.activeSelf);
            LockOnCamera.SetActive(!LockOnCamera.activeSelf);
        }
        if (TargetLockOn)
        {
            StartCoroutine(LookAt());
            /*if (lookRotation == transform.rotation)
                transform.LookAt(Zombie.transform);*/
        }
    }

    private void FixedUpdate()
    {
        
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
        lookRotation = Quaternion.LookRotation(Zombie.transform.position - transform.position);
        lookRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * LockOnSpeed;
            yield return null;
        }
    }

}
