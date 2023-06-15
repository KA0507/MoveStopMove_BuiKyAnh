using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVictory : UICanvas
{
    // Quay về MainMenu
    public void OpenMainMenu()
    {
        UIManager.Ins.CloseUI<UIVictory>();
        UIManager.Ins.OpenUI<UIMainMenu>().SetCoinPlayer();
        LevelManager.Ins.player.OnInit();
        GameManager.Ins.ChangeGameState(GameState.MAINMENU);
        CameraFollow.Ins.ChangeCameraState(CameraState.MAINMENU);
        LevelManager.Ins.LoadLevel();
    }

    // Chuyển level tiếp theo
    public void NextZone()
    {
        UIManager.Ins.CloseUI<UIVictory>();
        UIManager.Ins.OpenUI<UIGamePlay>().SetPlayerJoystick();
        LevelManager.Ins.player.OnInit();
        GameManager.Ins.ChangeGameState(GameState.GAMEPLAY);
        LevelManager.Ins.LoadLevel();
    }

    // Tăng coin khi kết thúc
    public void AddCoinPlayer()
    {
        PlayerPrefs.SetInt(UserData.KEY_COIN, PlayerPrefs.GetInt(UserData.KEY_COIN) + LevelManager.Ins.player.score);
    }
}
