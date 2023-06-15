using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    [SerializeField] private GameObject onSound;
    [SerializeField] private GameObject offSound;
    [SerializeField] private GameObject onVibration;
    [SerializeField] private GameObject offVibration;

    private bool isSound = false;
    private bool isVibration = false;

    // Tắt/Bật Sound
    public void TurnSound()
    {
        onSound.SetActive(isSound); offSound.SetActive(!isSound);
        isSound = !isSound;
    }

    // Tắt/Bật Vibration
    public void TurnVibration()
    {
        onVibration.SetActive(isVibration); offVibration.SetActive(!isVibration);
        isVibration = !isVibration;
    }

    // Quay về MainMenu
    public void ButtonHome()
    {
        UIManager.Ins.CloseUI<UISetting>();
        UIManager.Ins.OpenUI<UIMainMenu>().SetCoinPlayer();
        LevelManager.Ins.player.OnInit();
        LevelManager.Ins.DespawnAllBot();
        GameManager.Ins.ChangeGameState(GameState.MAINMENU);
        CameraFollow.Ins.ChangeCameraState(CameraState.MAINMENU);
    }

    // Tiếp tục chơi
    public void ButtonContinue()
    {
        UIManager.Ins.CloseUI<UISetting>();
        UIManager.Ins.OpenUI<UIGamePlay>();
        GameManager.Ins.ChangeGameState(GameState.GAMEPLAY);
    }
}
