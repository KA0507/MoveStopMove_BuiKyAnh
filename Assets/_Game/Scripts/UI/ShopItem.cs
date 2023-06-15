using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject[] border;
    public Image icon;
    public int coin;
    public UIShop shop;
    public int state = 0;
    public PoolType poolType;

    // Cài đặt item theo loại item của shop và hiển thị
    public void OnInit(UIShop shop, int type, int n)
    {
        this.shop = shop;
        switch (type)
        {
            case 0:
                this.icon.sprite = shop.shopData.hats.Iteams[n].sprite;
                this.coin = shop.shopData.hats.Iteams[n].cost;
                this.poolType = (PoolType)shop.shopData.hats.Iteams[n].type;
                //PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)poolType, 0);
                this.state = PlayerPrefs.GetInt(UserData.KEY_ITEM + (int)poolType, 0);
                ChangeBorderItem(state);
                if (n == 0)
                {
                    SelectItem();
                }
                break;
            case 1:
                this.icon.sprite = shop.shopData.pants.Iteams[n].sprite;
                this.coin = shop.shopData.pants.Iteams[n].cost;
                this.poolType = (PoolType)shop.shopData.pants.Iteams[n].type;
                //PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)poolType, 0);
                this.state = PlayerPrefs.GetInt(UserData.KEY_ITEM + (int)poolType, 0);
                ChangeBorderItem(state);
                if (n == 0)
                {
                    SelectItem();
                }
                break;
            case 2:
                this.icon.sprite = shop.shopData.accessorys.Iteams[n].sprite;
                this.coin = shop.shopData.accessorys.Iteams[n].cost;
                this.poolType = (PoolType)shop.shopData.accessorys.Iteams[n].type;
                //PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)poolType, 0);
                this.state = PlayerPrefs.GetInt(UserData.KEY_ITEM + (int)poolType, 0);
                ChangeBorderItem(state);
                if (n == 0)
                {
                    SelectItem();
                }
                break;
            case 3:
                this.icon.sprite = shop.shopData.skins.Iteams[n].sprite;
                this.coin = shop.shopData.skins.Iteams[n].cost;
                this.poolType = (PoolType)shop.shopData.skins.Iteams[n].type;
                //PlayerPrefs.SetInt(UserData.KEY_ITEM + (int)poolType, 0);
                this.state = PlayerPrefs.GetInt(UserData.KEY_ITEM + (int)poolType, 0);
                ChangeBorderItem(state);
                if (n == 0)
                {
                    SelectItem();
                }
                break;
        }
    }

    // Khi chọn item hiển thị button tương ứng state của item
    public void SelectItem()
    {
        if (shop.currentItem != null)
        {
            shop.currentItem.ChangeBorderItem(shop.currentItem.state);
        }
        shop.currentItem = this;
        if (state == 2 && (poolType == (PoolType)UserData.Ins.playerHat || poolType == (PoolType)UserData.Ins.playerPant ||
                           poolType == (PoolType)UserData.Ins.playerAccessory || poolType == (PoolType)UserData.Ins.playerSkin))
        {
            shop.button.SetState(state);
        } else if (state == 2)
        {
            shop.button.SetState(1);
        } else
        {
            shop.button.SetState(state);
        }
        shop.TryItem();
        if (state == 0)
        {
            shop.button.SetCoin(coin);
        }
        ChangeBorderItem(3);
    }
    public void EquipItem()
    {

    }

    // Thay đổi viền item theo state của item
    public void ChangeBorderItem(int n)
    {
        for (int i = 0; i < border.Length; i++)
        {
            border[i].SetActive(false);
        }
        border[n].SetActive(true);
    }
}
