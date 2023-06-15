using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PatrolState : IState
{
    private float time = 0f;
    private float timer;
    public void OnEnter(Bot bot)
    {
        bot.Move();
        timer = Random.Range(1f, 3f);
    }

    public void OnExecute(Bot bot)
    {
        if (time < timer)
        {
            time += Time.deltaTime;
            return;
        }
        // Kiểm tra đến đich hoặc tìm thấy mục tiêu chuyển sang idle
        if (bot.IsDestination || bot.FindTarget() != Vector3.zero)
        {
            bot.ChangeState(new IdleState());
        } 
    }

    public void OnExit(Bot bot)
    {
        
    }
}
