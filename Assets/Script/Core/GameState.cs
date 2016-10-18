using UnityEngine;
using System.Collections;



public class GameState
{
    private GameRoundState m_GameRoundState;

    public GameState()
    {
        Init();
    }

    private void Init()
    {
        m_GameRoundState = GameRoundState.GameRoundStateNone;
    }

    public void Update() { }
}
