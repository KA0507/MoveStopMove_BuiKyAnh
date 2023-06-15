using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.MaterialProperty;

public class UIWeapon : UICanvas
{
    [SerializeField] private Text playerCoin;
    [SerializeField] private Transform parent;
    [SerializeField] private ButtonState button;
    [SerializeField] private Player player;

    private Weapon currentWeapon;
    private int state = 0;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        /*for (int i = 2; i < 11; i++)
        {
            PlayerPrefs.SetInt(UserData.KEY_ITEM + i, 0);
        }*/
    }

    // Cài đặt khi mở UIWeapon
    public void OpenUIWeapon()
    {
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)2, parent);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        playerCoin.text = PlayerPrefs.GetInt(UserData.KEY_COIN, 0).ToString();

        state = PlayerPrefs.GetInt(UserData.KEY_ITEM + (int)currentWeapon.poolType, 0);
        SetButton();
    }

    // Chuyển sang vú khí kế tiếp
    public void NextWeapon()
    {
        if ((int)currentWeapon.poolType < 10)
        {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = SimplePool.Spawn<Weapon>((PoolType)((int)currentWeapon.poolType + 1), parent);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;

            state = PlayerPrefs.GetInt(UserData.KEY_ITEM + (int)currentWeapon.poolType, 0);
            SetButton();

        }
    }

    // Chuyển sang vũ khí trước
    public void PreWeapon()
    {
        if ((int)currentWeapon.poolType > 2)
        {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = SimplePool.Spawn<Weapon>((PoolType)((int)currentWeapon.poolType - 1), parent);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;

            state = PlayerPrefs.GetInt(UserData.KEY_ITEM + (int)currentWeapon.poolType, 0);
            SetButton();
        }
    }

    // Thay đổi button
    public void SetButton()
    {
        if (state == 0)
        {
            button.SetState(0);
            button.SetCoin(currentWeapon.coin);
        }
        else if (state == 2 && (WeaponType)currentWeapon.poolType == UserData.Ins.playerWeapon)
        {
            button.SetState(2);

        }
        else
        {
            button.SetState(1);
        }
    }

    // Mua weapon
    public void BuyWeapon()
    {
        if (PlayerPrefs.GetInt(UserData.KEY_COIN) > currentWeapon.coin)
        {
            PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)currentWeapon.poolType, 1);
            button.SetState(1);
            PlayerPrefs.SetInt(UserData.KEY_COIN, PlayerPrefs.GetInt(UserData.KEY_COIN) - currentWeapon.coin);
            SetCoinPlayer();
        }
    }

    // Trang bị weapon
    public void EquipWeapon()
    {
        PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)UserData.Ins.playerWeapon, 1);    
        UserData.Ins.SetEnumData<WeaponType>(UserData.KEY_PLAYER_WEAPON, ref UserData.Ins.playerWeapon, (WeaponType)currentWeapon.poolType);
        player.ChangeWeapon((WeaponType)currentWeapon.poolType);
        PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)currentWeapon.poolType, 2);
        ButtonBack();
    }

    // Quay về MainMenu
    public void ButtonBack()
    {
        SimplePool.Despawn(currentWeapon);
        UIManager.Ins.CloseUI<UIWeapon>();
        UIManager.Ins.OpenUI<UIMainMenu>().SetCoinPlayer();
    }

    // Hiển thị coin
    public void SetCoinPlayer()
    {
        playerCoin.text = PlayerPrefs.GetInt(UserData.KEY_COIN).ToString();
    }
}
