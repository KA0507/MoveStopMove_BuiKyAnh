using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : GameUnit
{
    public Animator anim;

    public Transform rightHand;
    public Transform leftHand;
    public Renderer pant;
    public Transform head;
    public PantData pantData;

    public Weapon weapon;
    public Hat hat;
    public Accessory accessory;

    // Thay đổi vũ khí
    public void ChangeWeapon(WeaponType weaponType)
    {
        if (weapon) SimplePool.Despawn(weapon);
        weapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, rightHand);
    }

    // Thay đổi mũ
    public void ChangeHat(HatType hatType)
    {
        if (hat) SimplePool.Despawn(hat);
        hat = SimplePool.Spawn<Hat>((PoolType)hatType, head);
    }

    // Thay đổi accessory
    public void ChangeAccessory(AccessoryType accessoryType)
    {
        if (accessory) SimplePool.Despawn(accessory);
        accessory = SimplePool.Spawn<Accessory>((PoolType)accessoryType, leftHand);
    }

    // Thay đổi pant
    public void ChangePant(PantType pantType)
    {
        pant.material = pantData.GetMat((int)pantType % pantData.pant.Length);
    }
    
}
