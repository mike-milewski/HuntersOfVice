#pragma warning disable 0649
using UnityEngine;
using TMPro;

public class StatueObstacle : MonoBehaviour
{
    private Character PlayerTarget;

    [SerializeField]
    private ObstacleDamageRadius obstacleDamageRadius;

    [SerializeField]
    private Transform LaserParticleOriginPoint;

    [SerializeField]
    private bool IsFollowingPlayer, Attacking;

    [SerializeField]
    private float TimeToStop, DamageTime, ParticleTime;

    private float FollowTime, ParticleWaitTime, TimeToIncrease;

    private Vector3 StatueDistance, LaserPointDistance;

    private Quaternion LookDirection, LaserPointLookDirection;

    [SerializeField]
    private float LookSpeed;

    public Character GetPlayerTarget
    {
        get
        {
            return PlayerTarget;
        }
        set
        {
            PlayerTarget = value;
        }
    }

    public ObstacleDamageRadius GetObstacleDamageRadius
    {
        get
        {
            return obstacleDamageRadius;
        }
        set
        {
            obstacleDamageRadius = value;
        }
    }

    public bool GetIsFollowing
    {
        get
        {
            return IsFollowingPlayer;
        }
        set
        {
            IsFollowingPlayer = value;
        }
    }

    public bool GetIsAttacking
    {
        get
        {
            return Attacking;
        }
        set
        {
            Attacking = value;
        }
    }

    private void OnEnable()
    {
        TimeToIncrease = 0;
        FollowTime = 0;
        ParticleWaitTime = 0;
    }

    private void Update()
    {
        if(!Attacking)
        {
            FollowTime += Time.deltaTime;
            if (FollowTime >= TimeToStop)
            {
                IsFollowingPlayer = false;
                Attacking = true;
                DrawAOE();
                FollowTime = 0;
            }
        }

        if(IsFollowingPlayer && !Attacking)
        {
            EnableAudio();

            StatueDistance = new Vector3(transform.position.x - PlayerTarget.transform.position.x, 0,
                                   transform.position.z - PlayerTarget.transform.position.z).normalized;

            LaserPointDistance = new Vector3(LaserParticleOriginPoint.position.x - PlayerTarget.transform.position.x,
                                             LaserParticleOriginPoint.position.y - PlayerTarget.transform.position.y,
                                             LaserParticleOriginPoint.position.z - PlayerTarget.transform.position.z).normalized;

            LookDirection = Quaternion.LookRotation(StatueDistance).normalized;
            LaserPointLookDirection = Quaternion.LookRotation(LaserPointDistance).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, LookDirection, LookSpeed * Time.deltaTime);
            LaserParticleOriginPoint.rotation = Quaternion.Slerp(LaserParticleOriginPoint.rotation, LaserPointLookDirection, LookSpeed * Time.deltaTime);
        }

        if(Attacking)
        {
            DisableAudio();

            ParticleWaitTime += Time.deltaTime;
            if(ParticleWaitTime >= ParticleTime)
            {
                InvokeParticleEffect();
                CreateLaserExplosionParticleEffect();
                ParticleWaitTime = 0;
            }
        }

        CheckAOEStatus();
    }

    public void CheckPlayerHealth()
    {
        if(PlayerTarget != null)
        {
            if (PlayerTarget.CurrentHealth <= 0)
            {
                Attacking = false;
                this.enabled = false;
                obstacleDamageRadius.enabled = false;
                this.GetComponent<ReturnObjectToOriginalRotation>().enabled = true;
            }
        }
    }

    private void EnableAudio()
    {
        if (!gameObject.GetComponent<AudioSource>().enabled)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else return;
    }

    public void DisableAudio()
    {
        if (gameObject.GetComponent<AudioSource>().enabled)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().enabled = false;
        }
        else return;
    }

    private void DrawAOE()
    {
        obstacleDamageRadius.enabled = true;

        Vector3 Distance = new Vector3(PlayerTarget.transform.position.x - transform.position.x, 0, PlayerTarget.transform.position.z - transform.position.z);

        obstacleDamageRadius.transform.position = transform.position + Distance;
    }

    private void CheckAOEStatus()
    {
        if (obstacleDamageRadius.enabled)
        {
            TimeToIncrease += Time.deltaTime;
            if(TimeToIncrease >= DamageTime)
            {
                Attacking = false;
                IsFollowingPlayer = true;
                TimeToIncrease = 0;
                ParticleWaitTime = 0;
                obstacleDamageRadius.enabled = false;
            }
        }
        else return;
    }

    private void InvokeParticleEffect()
    {
        var StatueLaser = ObjectPooler.Instance.GetStatueLaserParticle();

        StatueLaser.SetActive(true);

        StatueLaser.transform.SetParent(LaserParticleOriginPoint, false);

        StatueLaser.transform.position = new Vector3(LaserParticleOriginPoint.position.x, LaserParticleOriginPoint.position.y, LaserParticleOriginPoint.position.z);

        CheckPlayerHealth();
    }

    private void CreateLaserExplosionParticleEffect()
    {
        var LaserExplosion = ObjectPooler.Instance.GetLaserExplosionParticle();

        LaserExplosion.SetActive(true);

        LaserExplosion.transform.position = new Vector3(obstacleDamageRadius.transform.position.x, obstacleDamageRadius.transform.position.y + 0.3f,
                                                        obstacleDamageRadius.transform.position.z);
    }
}