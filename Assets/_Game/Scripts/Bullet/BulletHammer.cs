using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHammer : Bullet
{
    [SerializeField] private Transform child;

    Vector3 target;

    public override void OnInit(Character character, Vector3 target, float size)
    {
        base.OnInit(character, target, size);
        Vector3 direction = (target - TF.position - (target.y - TF.position.y) * Vector3.up).normalized;
        this.target = character.TF.position + direction * character.rangeAttack * size * 1.2f;
    }

    private void Update()
    {
        // hammer quay và di chuyển tới target
        TF.position = Vector3.MoveTowards(TF.position, target, speed * Time.deltaTime);
        child.Rotate(Vector3.forward * -6, Space.Self);
        if (Vector3.Distance(TF.position, target) < 0.1f)
        {
            OnDespawn();
        }
    }
}