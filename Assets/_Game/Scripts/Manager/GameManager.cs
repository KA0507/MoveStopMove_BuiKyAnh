using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MAINMENU, GAMEPLAY, FINISH, SETTING
}
public class GameManager : Singleton<GameManager>
{
    public GameState gameState;

    private void Awake()
    {
        UIManager.Ins.OpenUI<UIMainMenu>().SetCoinPlayer();
        LevelManager.Ins.player.OnInit();
        CameraFollow.Ins.ChangeCameraState(CameraState.MAINMENU);
        ChangeGameState(GameState.MAINMENU);
    }

    // So sánh state GameManager
    public bool IsState(GameState gameState) => this.gameState == gameState;

    // Thay đổi state của GameManager
    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;
    }
}
