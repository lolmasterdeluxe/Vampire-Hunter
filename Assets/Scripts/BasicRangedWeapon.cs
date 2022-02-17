using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Ho Junliang 
 * Created: 15/2/2022
 */
public class BasicRangedWeapon : RangedWeapon
{
    [SerializeField]
    private Transform Parent;
    [SerializeField]
    private int NumberOfBullets = 1;
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform BulletSpawnPoint;
    [SerializeField]
    private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private bool ShootDelayTrue = false;
    [SerializeField]
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask;


    private Animator Animator;
    private float LastShootTime;
    

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Animator.Play("Shoot");
        }
    }

    public override void Attack(GameObject target)
    {
        Debug.Log("Ranged Weapon Attack.");
        WeaponInfo weaponInfo = (WeaponInfo)itemInfo;

        for (int i = 0; i < NumberOfBullets; ++i)
        {
            if (!ShootDelayTrue || LastShootTime + ShootDelay < Time.time)
            {
                ShootingSystem.Play();
                Vector3 direction = GetDirection();

                if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, Mask))
                {
                    TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                    StartCoroutine(SpawnTrail(trail, hit));

                    LastShootTime = Time.time;
                    DealDamage(target, weaponInfo.damage);
                }
            }
        }

    }

    private Vector3 GetDirection()
    {
        Vector3 direction = Parent.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x), 
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y), 
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z));

            direction.Normalize();
        }
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;
            yield return null;
        }
        Trail.transform.position = Hit.point;
        Instantiate(ImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }
}
