using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDefeat : UICanvas
{
    [SerializeField] private Text t;

    // Quay về MainMenu
    public void ButtonHome()
    {
        UIManager.Ins.CloseUI<UIDefeat>();
        UIManager.Ins.OpenUI<UIMainMenu>().SetCoinPlayer();
        LevelManager.Ins.player.OnInit();
        GameManager.Ins.ChangeGameState(GameState.MAINMENU);
        CameraFollow.Ins.ChangeCameraState(CameraState.MAINMENU);
    }

    // Tăng coin khi kết thúc
    public void AddCoinPlayer()
    {
        PlayerPrefs.SetInt(UserData.KEY_COIN, PlayerPrefs.GetInt(UserData.KEY_COIN) + LevelManager.Ins.player.score);
        t.text = "#" + LevelManager.Ins.totalCharacter;
    }
}
