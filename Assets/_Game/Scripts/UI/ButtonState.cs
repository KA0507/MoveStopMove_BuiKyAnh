using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState : MonoBehaviour
{
    [SerializeField] private GameObject[] button;
    [SerializeField] private Text coinItem;

    // Chuyển button
    public void SetState(int state)
    {
        for (int i = 0; i < button.Length; i++)
        {
            button[i].SetActive(false);
        }

        button[state].SetActive(true);
    }

    // Đặt coin với button buy
    public void SetCoin(int coin)
    {
        coinItem.text = coin.ToString();
    }
}
