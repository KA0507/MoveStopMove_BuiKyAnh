using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.MaterialProperty;

public class UIShop : UICanvas
{
    [SerializeField] private Transform content;
    [SerializeField] private ShopItem item;
    [SerializeField] private Text playerCoin;
    [SerializeField] private Text coinItem;
    [SerializeField] private Player player;
    [SerializeField] private ShopBar icon;

    public ShopItem currentItem;
    public ButtonState button;
    public ShopData shopData;
    public ShopBar shopBar;
    public int currentShopBar;

    MiniPool<ShopItem> shopPool = new MiniPool<ShopItem>();

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shopPool.OnInit(item, 10, content);
    }

    // Cài đặt khi mở shop
    public void OpenShop()
    {
        icon.ChangeTypeItem();
        ChangeTypeItems(0);
        SetCoinPlayer();
    }

    // Thay đổi loại item trong cửa hàng
    public void ChangeTypeItems(int type)
    {
        shopPool.Collect();
        switch(type)
        {
            case 0:
                for (int i = 0; i < shopData.hats.Iteams.Count; i++)
                {
                    shopPool.Spawn().OnInit(this, 0, i);
                }
                break;
            case 1:
                for (int i = 0; i < shopData.pants.Iteams.Count; i++)
                {
                    shopPool.Spawn().OnInit(this, 1, i);
                }
                break;
            case 2:
                for (int i = 0; i < shopData.accessorys.Iteams.Count; i++)
                {
                    shopPool.Spawn().OnInit(this, 2, i);
                }
                break;
            case 3:
                for (int i = 0; i < shopData.skins.Iteams.Count; i++)
                {
                    shopPool.Spawn().OnInit(this, 3, i);
                }
                break;
        }
    }

    // Mua item
    public void BuyItem()
    {
        if (PlayerPrefs.GetInt(UserData.KEY_COIN) >= currentItem.coin)
        {
            currentItem.state = 1;
            PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)currentItem.poolType, 1);
            button.SetState(1);
            PlayerPrefs.SetInt(UserData.KEY_COIN, PlayerPrefs.GetInt(UserData.KEY_COIN) - currentItem.coin);
            SetCoinPlayer();
        }
    }
    
    // Thử item
    public void TryItem()
    {
        switch (currentShopBar)
        {
            case 0:
                player.ChangeHat((HatType)currentItem.poolType);
                break;
            case 1:
                player.ChangePant((PantType)currentItem.poolType);
                break;
            case 2:
                player.ChangeAccessory((AccessoryType)currentItem.poolType);
                break;
            case 3:
                player.ChangeSkin((SkinType)currentItem.poolType);
                break;
        }
    }


    // Trang bị item
    public void EquipItem()
    {
        switch (currentShopBar)
        {
            case 0:
                PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)UserData.Ins.playerHat, 1);
                UserData.Ins.SetEnumData<HatType>(UserData.KEY_PLAYER_HAT, ref UserData.Ins.playerHat, (HatType)currentItem.poolType);
                player.ChangeHat((HatType)currentItem.poolType);
                break;
            case 1:
                PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)UserData.Ins.playerPant, 1);
                UserData.Ins.SetEnumData<PantType>(UserData.KEY_PLAYER_PANT, ref UserData.Ins.playerPant, (PantType)currentItem.poolType);
                player.ChangePant((PantType)currentItem.poolType);
                break;
            case 2:
                PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)UserData.Ins.playerAccessory, 1);
                UserData.Ins.SetEnumData<AccessoryType>(UserData.KEY_PLAYER_ACCESSORY, ref UserData.Ins.playerAccessory, (AccessoryType)currentItem.poolType);
                player.ChangeAccessory((AccessoryType)currentItem.poolType);
                break;
            case 3:
                PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)UserData.Ins.playerSkin, 1);
                UserData.Ins.SetEnumData<SkinType>(UserData.KEY_PLAYER_SKIN, ref UserData.Ins.playerSkin, (SkinType)currentItem.poolType);
                player.ChangeSkin((SkinType)currentItem.poolType);
                break;
        }
        
        PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)currentItem.poolType, 2);
        currentItem.state = 2;
        currentItem.ChangeBorderItem(3);
        button.SetState(2);
        
    }

    // Quay về MainMenu
    public void ButtonBack()
    {
        UIManager.Ins.CloseUI<UIShop>();
        UIManager.Ins.OpenUI<UIMainMenu>().SetCoinPlayer();
        CameraFollow.Ins.ChangeCameraState(CameraState.MAINMENU);
        if (player != null)
        {
            player.ChangeItem();
        }
    }

    // Hiển thị coin
    public void SetCoinPlayer()
    {
        playerCoin.text = PlayerPrefs.GetInt(UserData.KEY_COIN).ToString();
    }
}
