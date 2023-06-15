using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public Level[] levels;
    public Level currentLevel;
    public List<Bot> bots = new List<Bot>();
    public int totalCharacter;
    public int realBot;
    public int indexLevel;


    // Start is called before the first frame update
    void Start()
    {
        indexLevel = 0;
        currentLevel = Instantiate(levels[indexLevel]);
        OnInit();
    }

    // Lấy các chỉ số của level
    public void OnInit()
    {
        currentLevel.player = FindObjectOfType<Player>();
        //player.OnInit();
        this.totalCharacter = currentLevel.totalCharacter;
        this.realBot = currentLevel.realBot;
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra và spawn bot
        if (GameManager.Ins.IsState(GameState.GAMEPLAY) && bots.Count < realBot && bots.Count < totalCharacter - 1)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot);
            bot.OnInit();
            bots.Add(bot);
        }
    }

    // Tăng chỉ số level
    public void NextLevel()
    {
        indexLevel++;
    }

    // Tải level
    public void LoadLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[indexLevel]);
        OnInit();
    }

    // Xóa character khỏi danh sách mục tiêu các character khác
    public void RemoveTarget(Character character)
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].targets.Remove(character);
        }
        player.targets.Remove(character);
    }

    // Despawn tất cả bot
    public void DespawnAllBot()
    {
        for (int i = bots.Count - 1; i >= 0; i--)
        {
            SimplePool.Despawn(bots[i].targetIndicator);
            SimplePool.Despawn(bots[i]);
            bots.Remove(bots[i]);
        }
    }

    // Hoàn thành game
    public void Victory()
    {
        DespawnAllBot();
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UIVictory>().AddCoinPlayer();
        GameManager.Ins.ChangeGameState(GameState.FINISH);
        player.Joystick = null;
        player.ChangeAnim(Constant.ANIM_WIN);
        NextLevel();
    }

    // Thua game
    public void Defeat()
    {
        DespawnAllBot();
        UIManager.Ins.CloseUI<UIGamePlay>();
        UIManager.Ins.OpenUI<UIDefeat>().AddCoinPlayer();
        GameManager.Ins.ChangeGameState(GameState.FINISH);
        player.Joystick = null;

    }
}
