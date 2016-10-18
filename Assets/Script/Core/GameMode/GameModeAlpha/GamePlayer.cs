using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayer
{
    private List<Unit> m_AccessibleUnitList;

    public GamePlayer() { }

    private void Init() { }

    #region game logic
    public bool IsPlayerRoundOver()
    {
        bool ret = true;

        for (int i = 0; i < m_AccessibleUnitList.Count; ++i)
        {
            //只要有一个单位行动力未耗尽，则表示玩家回合尚未结束
            if (!m_AccessibleUnitList[i].MovementData.IsMovementEnd)
            {
                ret = false;
                break;
            }
        }

        return ret;
    }

    #endregion
}
