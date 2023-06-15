using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IState
{
    private float timer = 0;
    public void OnEnter(Bot bot)
    {
        bot.StopMove();
    }

    public void OnExecute(Bot bot)
    {
        bot.StopMove();

        // Trong 1s có mục tiêu và có thể tấn công chuyển sang attack ngược lại chuyển sang patrol
        if (timer < 1f) 
        {
            timer += Time.deltaTime;
            if (bot.FindTarget() != Vector3.zero && bot.currentSkin.weapon.IsCanAttack)
            {
                bot.ChangeState(new AttackState());
            }
        }
        else
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
