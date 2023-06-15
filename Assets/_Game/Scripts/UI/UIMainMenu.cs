using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    [SerializeField] private Text coinPlayer;
    [SerializeField] private Text zone;

    // Chơi game
    public void PlayGame()
    {
        UIManager.Ins.CloseUI<UIMainMenu>();
        UIManager.Ins.OpenUI<UIGamePlay>().SetPlayerJoystick();
        LevelManager.Ins.LoadLevel();
        LevelManager.Ins.player.OnInit();
        CameraFollow.Ins.ChangeCameraState(CameraState.GAMEPLAY);
        GameManager.Ins.ChangeGameState(GameState.GAMEPLAY);
    }

    // Mở shop
    public void OpenShopItem()
    {
        UIManager.Ins.CloseUI<UIMainMenu>();
        UIManager.Ins.OpenUI<UIShop>().OpenShop();
        CameraFollow.Ins.ChangeCameraState(CameraState.SHOP);
    }

    // Mở shop Weapon
    public void OpenShopWeapon()
    {
        UIManager.Ins.CloseUI<UIMainMenu>();
        UIManager.Ins.OpenUI<UIWeapon>().OpenUIWeapon();
    }

    // Hiển thị coin
    public void SetCoinPlayer()
    {
        coinPlayer.text = PlayerPrefs.GetInt(UserData.KEY_COIN).ToString();
        zone.text = Constant.ZONE + (LevelManager.Ins.indexLevel + 1);
    }
}
