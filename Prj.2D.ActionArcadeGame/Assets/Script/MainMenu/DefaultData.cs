using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultData : MonoBehaviour
{
    /// <summary>
    /// 將 InGameData 初始化
    /// </summary>
    public void SetDataToDefaultValue()
    {
        InGameData.thisTurn_Life = 3;
        InGameData.thisTurn_Score = 0;
    }
}
