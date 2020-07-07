using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetupable
{
    void Setup(); //用來看怪物是否已經掉在地上才開始行動，避免在空中瘋狂旋轉導致配置錯誤
}
