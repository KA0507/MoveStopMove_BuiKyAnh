using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    protected Character character;
    [SerializeField] protected float speed;
    protected float size;

    public virtual void OnInit(Character character, Vector3 target, float size)
    {
        this.character = character;
        this.size = size;
        TF.forward = (target - TF.position).normalized;
        TF.localScale = new Vector3(size, size, size);
    }

    // Despawn bullet
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Va chạm với character khác
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character c = Cache.GetCharacter(other);
            if (c != character && !c.IsDead)
            {
                c.Death();
                OnDespawn();
                character.RemoveTarger(c);
                character.score += c.score + 1;
                character.SetSize();
            }
        }

        // Va chạm với block
        if (other.CompareTag(Constant.TAG_BLOCK))
        {
            OnDespawn();
        }
    }

}
