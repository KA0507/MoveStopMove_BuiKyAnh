using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBar : MonoBehaviour
{
    public GameObject bg;
    public int type;
    public UIShop shop;

    // Thay đổi loại item của shop
    public void ChangeTypeItem()
    {
        if (shop.shopBar != null)
        {
            shop.shopBar.bg.SetActive(true);
        }
        shop.shopBar = this;
        shop.currentShopBar = type;
        shop.ChangeTypeItems(type);
        bg.SetActive(false);
    }
}
