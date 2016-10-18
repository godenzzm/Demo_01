using UnityEngine;
using System.Collections;

public class GameModeAlpha : IGameMode
{
    private GamePlayer m_GamePlayer; //玩家对象
    private GameState m_MainGameState; //当前游戏数据状态对象
    private GameRoundState m_GameRoundState; //当前的游戏回合状态

    private bool IsShowUIRoundState;
    private bool IsShowUIRoundStateOver;

    public GameModeAlpha()
    {
        m_GamePlayer = new GamePlayer();
        m_MainGameState = new GameState();
        m_GameRoundState = GameRoundState.GameRoundStateNone;
    }

    public void Update()
    {
        switch (m_GameRoundState)
        {
            case GameRoundState.GameRoundStateNone:
                break;
            case GameRoundState.GameRoundStatePlayer:
                break;
            case GameRoundState.GameRoundStateAI:
                break;
            default:
                break;
        }
    }

    public void Start() { }

    #region event callback
    public void OnShowUIRoundStateOverCallback()
    {

    }
    #endregion

    #region round logic
    private void PlayerRound()
    {
        //进入回合首先显示回合状态UI
        if (UIRoundState()) return;

        if (m_GamePlayer.IsPlayerRoundOver())
        {
            m_GameRoundState = GameRoundState.GameRoundStateAI;
        }
    }

    private void AIRound()
    {
        if (UIRoundState()) return;
        //Temperary logic

        m_GameRoundState = GameRoundState.GameRoundStatePlayer;
    }

    private bool UIRoundState()
    {
        bool ret = false;

        if (!IsShowUIRoundState)
        {
            //..Call UI logic
            IsShowUIRoundState = true;
            IsShowUIRoundStateOver = false;
        }

        if (!IsShowUIRoundStateOver) ret = true;

        return ret;
    }
    #endregion


}
