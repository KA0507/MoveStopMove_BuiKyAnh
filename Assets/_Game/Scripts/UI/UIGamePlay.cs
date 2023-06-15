using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] private Player player;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Text alive;

    private Vector3 positionJoystick;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        positionJoystick = joystick.transform.position;
    }

    public void Update()
    {
        // Kiểm tra kết thúc game
        alive.text = Constant.ALIVE + LevelManager.Ins.totalCharacter;
        if (LevelManager.Ins.totalCharacter == 1)
        {
            LevelManager.Ins.Victory();
        }
    }

    // Gán joystick vào player
    public void SetPlayerJoystick()
    {
        // Kiểm tra xem đối tượng nhân vật và joystick có tồn tại không
        if (player != null && joystick != null)
        {
            // Tìm đối tượng con của nhân vật có chứa component Joystick và gán joystick vào đó
            player.Joystick = joystick;
            player.PositionJoystick = positionJoystick;
        }
    }

    // Mở setting
    public void OpenSetting()
    {
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UISetting>();
        GameManager.Ins.ChangeGameState(GameState.SETTING);
    }
}
