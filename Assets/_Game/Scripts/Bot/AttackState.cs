using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Bot bot)
    {
        // Sau 0.4s bot attack
        bot.Counter.Start(() => bot.Attack(), 0.4f);
    }

    public void OnExecute(Bot bot)
    {
        bot.navMeshAgent.velocity = Vector3.zero;
        bot.Counter.Execute();
    }

    public void OnExit(Bot bot)
    {
        
    }
}
