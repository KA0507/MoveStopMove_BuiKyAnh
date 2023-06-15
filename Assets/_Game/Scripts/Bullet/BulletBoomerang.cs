using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoomerang : Bullet
{
    [SerializeField] private Transform child;

    Vector3 target;
    int isState = 0;

    public override void OnInit(Character character, Vector3 target, float size)
    {
        base.OnInit(character, target, size);
        Vector3 direction = (target - TF.position - (target.y - TF.position.y) * Vector3.up).normalized;
        this.target = character.TF.position + direction * character.rangeAttack * size * 1.2f;
        isState = 0;
    }

    private void Update()
    {
        // state = 0 boomerang bay tới target
        // state = 1 boomerang từ target quay về character
        switch(isState)
        {
            case 0:
                TF.position = Vector3.MoveTowards(TF.position, target, speed*Time.deltaTime);
                if (Vector3.Distance(TF.position, target) < 0.1f)
                {
                    isState = 1;
                }
                break;
            case 1:
                TF.position = Vector3.MoveTowards(TF.position, character.TF.position, speed * Time.deltaTime);
                if (Vector3.Distance(TF.position, character.TF.position) < 0.1f)
                {
                    isState = 2;
                    OnDespawn();
                }
                break;
        }
        // Xoay boomerang
        child.Rotate(Vector3.up * -6, Space.Self);
    }
}
