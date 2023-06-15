using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] private GameObject child;
    [SerializeField] private BulletType bulletType;

    public int coin;
    public bool IsCanAttack => child.activeSelf;

    // Tắt/bật vũ khí
    public void SetActive(bool active)
    {
        child.SetActive(active);
    }

    // Tạo ra bullet di chuyển đến target
    public void Throw(Character character, Vector3 target, float size)
    {
        Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, TF.position, Quaternion.identity);
        bullet.OnInit(character, target, size);
    }
}
