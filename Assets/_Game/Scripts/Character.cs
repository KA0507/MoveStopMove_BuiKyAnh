using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Character : GameUnit
{
    public Skin currentSkin;
    public GameObject mask;
    public float rangeAttack = 5f;
    public Level level;
    public TargetIndicator targetIndicator = new TargetIndicator();
    public List<Character> targets;
    public int score;
    public float minSize = 1f;
    public float maxSize = 1.5f;

    protected float size;
    private Vector3 target;
    private bool isDead = false;
    private bool isAttack = false;
    private string currentAnimName;

    // Thay đổi animation
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName && currentSkin != null)
        {
            currentSkin.anim.ResetTrigger(Animator.StringToHash(currentAnimName));
            currentAnimName = animName;
            currentSkin.anim.SetTrigger(Animator.StringToHash(currentAnimName));
        }
    }

    public void ResetAnim()
    {
        currentAnimName = "";
    }

    // Tìm mục tiêu gần nhất trong rangeAttack
    public Vector3 FindTarget()
    {
        Vector3 target = Vector3.zero;
        float minDis = Mathf.Infinity;
        foreach (Character cha in targets)
        {
            float dis = Mathf.Sqrt((TF.position.x - cha.TF.position.x) * (TF.position.x - cha.TF.position.x) + (TF.position.z - cha.TF.position.z) * (TF.position.z - cha.TF.position.z));
            if (dis > 0.1f && !cha.isDead)
            {
                target = cha.TF.position;
                minDis = dis;
            }
        }
        if (minDis < rangeAttack * size * 1.2f)
        {
            return target;
        } else
        {
            return Vector3.zero;
        }
    }

    // Thay đổi kích thước
    public void SetSize()
    {
        size = Mathf.Clamp(minSize + score * 0.02f, minSize, maxSize);
        TF.localScale = new Vector3(size, size, size);
        targetIndicator.SetScore(score);
    }

    // Thêm mục tiêu
    public void AddTarger(Character character)
    {
        targets.Add(character);
    }

    // Xóa mục tiêu
    public void RemoveTarger(Character character)
    {
        targets.Remove(character);
    }

    // Tấn công
    public virtual void Attack()
    {
        ChangeAnim(Constant.ANIM_ATTACK);
        isAttack = true;
    }

    // Hiện thị vũ khí khi tấn công xong
    public void SetActiveWeapon()
    {
        currentSkin.weapon.SetActive(true);
        ResetAttack();
    }

    public void ResetAttack()
    {
        isAttack = false;
    }

    public virtual void Death()
    {
        ChangeAnim(Constant.ANIM_DIE);
        isDead = true;
    }
    
    // Thay đổi vũ khí
    public void ChangeWeapon(WeaponType weapon)
    {
        if (currentSkin != null)
        {
            currentSkin.ChangeWeapon(weapon);
        }
    }

    // Thay đổi mũ
    public void ChangeHat(HatType hat)
    {
        currentSkin.ChangeHat(hat);
    }

    // Thay đổi accessory
    public void ChangeAccessory(AccessoryType accessoryType)
    {
        currentSkin.ChangeAccessory(accessoryType);
    }

    // Thay đổi pant
    public void ChangePant(PantType pantType)
    {
        currentSkin.ChangePant(pantType);
    }

    // Thay đổi skin
    public void ChangeSkin(SkinType skinType)
    {
        if (currentSkin != null)
        {
            Weapon weapon = currentSkin.weapon;
            SimplePool.Despawn(currentSkin);
            currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
            if (weapon != null)
            {
                ChangeWeapon((WeaponType)weapon.poolType);
            }
        }
        else
        {
            currentSkin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
            WeaponType randomWeapon = (WeaponType)Random.Range(2, 11);
            ChangeWeapon(randomWeapon);
        }

    }

    public bool IsDead { get { return isDead; } set { isDead = value; } }
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
    public Vector3 Target { get { return target; } set { target = value; } }
}
