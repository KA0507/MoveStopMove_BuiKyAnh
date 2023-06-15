using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRangeAttack : MonoBehaviour
{
    [SerializeField] Character character;

    private void OnTriggerEnter(Collider other)
    {
        // Va chạm với character thêm mục tiêu
        if (other.gameObject.CompareTag(Constant.TAG_CHARACTER) && GameManager.Ins.IsState(GameState.GAMEPLAY))
        {
            Character c = Cache.GetCharacter(other);
            if (!c.IsDead)
            {
                character.AddTarger(c);
                if (character as Player)
                {
                    c.mask.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Kết thúc va chạm với character xóa mục tiêu
        if (other.gameObject.CompareTag(Constant.TAG_CHARACTER) && GameManager.Ins.IsState(GameState.GAMEPLAY))
        {
            Character c = Cache.GetCharacter(other);
            character.RemoveTarger(c);
            if (character as Player)
            {
                c.mask.SetActive(false);
            }
        }
    }
}
