using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Bot : Character
{
    [SerializeField] private Transform indicatorPoint;
    public NavMeshAgent navMeshAgent;
    private IState currentState;

    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;

    public bool IsDestination => Mathf.Sqrt((TF.position.x - Target.x) * (TF.position.x - Target.x) + (TF.position.z - Target.z) * (TF.position.z - Target.z)) < 0.1f;

    public void OnInit()
    {
        mask.SetActive(false);
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        targetIndicator.OnInit(indicatorPoint);
        navMeshAgent.enabled = true;
        level = LevelManager.Ins.currentLevel;
        score = Random.Range(0, LevelManager.Ins.player.score + 1);
        SetSize();
        TF.position = level.RandomPoint();
        ChangeState(new IdleState());
        IsDead = false;
        /*SkinType randomSkin = (SkinType)Random.Range(20, 26);
        if (randomSkin == SkinType.Normal)
        {
            HatType randomHat = (HatType)Random.Range(26, 35);
            ChangeHat(randomHat);
            PantType randomPant = (PantType)Random.Range(0, 9);
            ChangePant(randomPant);
            AccessoryType randomAccessory = (AccessoryType)Random.Range(35, 39);
            ChangeAccessory(randomAccessory);
        } else
        {
            ChangeSkin(randomSkin);
        }*/
        WeaponType randomWeapon = (WeaponType)Random.Range(2, 11);
        ChangeWeapon(randomWeapon);
        
    }

    private void Update()
    {
        if (!GameManager.Ins.IsState(GameState.GAMEPLAY))
        {
            ChangeAnim(null);
            return;
        }
        if (IsDead)
        {
            navMeshAgent.enabled = false;
        }
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }

    // Di chuyển bot
    public void Move()
    {
        Target = level.RandomPoint();
        TF.LookAt(Target - (Target.y - TF.position.y) * Vector3.up);
        navMeshAgent.SetDestination(Target);
        ChangeAnim(Constant.ANIM_RUN);
    }

    // Bot dừng di chuyển
    public void StopMove()
    {
        ChangeAnim(Constant.ANIM_IDLE);
        navMeshAgent.velocity = Vector3.zero;
        
    }

    // Tấn công
    public override void Attack()
    {
        base.Attack();
        Target = FindTarget();
        if (Target != Vector3.zero)
        {
            TF.LookAt(Target - (Target.y - TF.position.y) * Vector3.up);
            counter.Start(Throw, 0.4f);
        } else
        {
            ChangeState(new IdleState());
        }
    }

    // vũ khí
    public void Throw()
    {
        currentSkin.weapon.Throw(this, Target, size);
        currentSkin.weapon.SetActive(false);
        ResetAnim();
        ChangeState(new PatrolState());
        Invoke("SetActiveWeapon", 1f);
    }

    // Bot chết
    public override void Death()
    {
        base.Death();
        navMeshAgent.velocity = Vector3.zero;
        LevelManager.Ins.totalCharacter--;
        LevelManager.Ins.RemoveTarget(this);
        targets.Clear();
        Invoke("OnDeswpan", 2.5f);
    }

    // Despawn bot
    public void OnDeswpan()
    {
        SimplePool.Despawn(this);
        SimplePool.Despawn(targetIndicator);
        LevelManager.Ins.bots.Remove(this);
    }

    // Thay đổi state của bot
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
