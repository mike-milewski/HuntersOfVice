#pragma warning disable 0649
using UnityEngine;
using TMPro;

public class StatueObstacle : MonoBehaviour
{
    private Character PlayerTarget;

    [SerializeField]
    private ObstacleDamageRadius obstacleDamageRadius;

    [SerializeField]
    private bool IsFollowingPlayer, Attacking;

    [SerializeField]
    private float TimeToStop, DamageTime, TimeToIncrease;

    private float FollowTime;

    private Vector3 Distance;

    private Quaternion LookDir;

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
            Distance = new Vector3(PlayerTarget.transform.position.x - transform.position.x, 0,
                                   PlayerTarget.transform.position.z - transform.position.z).normalized;

            LookDir = Quaternion.LookRotation(Distance).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, LookDir, LookSpeed * Time.deltaTime);
        }

        CheckAOEStatus();
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
                obstacleDamageRadius.enabled = false;
            }
        }
        else return;
    }
}