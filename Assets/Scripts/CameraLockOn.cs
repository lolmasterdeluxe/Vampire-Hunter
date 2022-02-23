using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    [SerializeField]
    private GameObject FollowCamera, LockOnCamera, Zombie;
    [SerializeField]
    private Cinemachine.CinemachineBrain brain;
    [HideInInspector]
    public bool LockOn = false, TargetLockOn = false;

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
            transform.LookAt(Zombie.transform.position);

        if (!LockOn)
            brain.ManualUpdate();
    }

    private void FixedUpdate()
    {
        if (LockOn)
            brain.ManualUpdate();
    }
}
