using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    public const string KEY_PLAYER_WEAPON = "PlayerWeapon";
    public const string KEY_PLAYER_HAT = "PlayerHat";
    public const string KEY_PLAYER_ACCESSORY = "PlayerAccessory";
    public const string KEY_PLAYER_PANT = "PlayerPant";
    public const string KEY_PLAYER_SKIN = "PlayerSkin";
    public const string KEY_ITEM = "Item";
    public const string KEY_COIN = "Coin";

    public WeaponType playerWeapon = WeaponType.Hammer_1;
    public HatType playerHat = HatType.None;
    public AccessoryType playerAccessory = AccessoryType.None;
    public PantType playerPant = PantType.Pant_1;
    public SkinType playerSkin = SkinType.Normal;

    public int coin = 100;
    public int level = 1;

    private void Awake()
    {
        SetEnumData<WeaponType>(KEY_PLAYER_WEAPON, ref playerWeapon, WeaponType.Hammer_1);
        SetEnumData<HatType>(KEY_PLAYER_HAT, ref playerHat, HatType.None);
        SetEnumData<AccessoryType>(KEY_PLAYER_ACCESSORY, ref playerAccessory, AccessoryType.Shield);
        SetEnumData<PantType>(KEY_PLAYER_PANT, ref playerPant, PantType.Pant_1);
        SetEnumData<SkinType>(KEY_PLAYER_SKIN, ref playerSkin, SkinType.Normal);
        PlayerPrefs.SetInt(KEY_ITEM + (int)playerWeapon, 2);
        PlayerPrefs.SetInt(KEY_ITEM + (int)playerHat, 2);
        PlayerPrefs.SetInt(KEY_ITEM + (int)playerAccessory, 2);
        PlayerPrefs.SetInt(KEY_ITEM + (int)playerPant, 2);
        PlayerPrefs.SetInt(KEY_ITEM + (int)playerSkin, 2);
        PlayerPrefs.SetInt(KEY_COIN, coin);
    }

    // state = 0: chưa mua;
    // state = 1: đã mua, chưa trang bị;
    // state = 2: đã mua, đã trang bị;

    // Thay đổi state của item trong shop trong PlayerPrefs
    public void SetData(string key, int id, int state = 0)
    {
        PlayerPrefs.SetInt(key + id, state);
    }

    // Lấy state của item trong shop trong PlayerPrefs
    public int GetData(string key, int id, int state = 0)
    {
        return PlayerPrefs.GetInt(key + id, state);
    }

    // Thay đổi giá trị của key trong PlayerPrefs và varible
    public void SetEnumData<T>(string key, ref T varible, T value)
    {
        varible = value;
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    // Thay đổi giá trị của key trong PlayerPrefs
    public void SetEnumData<T>(string key, T value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    // Lấy enum theo key
    public T GetEnumData<T>(string key, T defaul)
    {
        return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, Convert.ToInt32(defaul)));
    }
}
