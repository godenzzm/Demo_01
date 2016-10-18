enum GameRoundState
{
    GameRoundStateNone = 0,
    GameRoundStatePlayer,
    GameRoundStateAI,
}

public class UnitMovementData
{
    public bool IsMovementEnd; //行动结束标志
    public float MovementPower; //行动力点数
    public UnitMovementData()
    {
        MovementPower = 100;
        IsMovementEnd = false;
    }
}
