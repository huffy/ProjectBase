//功能：状态枚举管理
//创建者: 胡海辉
//创建时间：

using UnityEngine;
using System.Collections;

public class StateEnumManager
{
  
}

public enum EnemyState
{
    Run,
    Idle,
    Attack,
    TurnAround,
}

public enum PlayerState
{
    Idle,
    LeftWalk,
    RightWalk,
    LeftRun,
    RightRun,
    Fetch,
    Push,
    Pull,
    Climb,
    Cross,
}

/// <summary>
/// 房间类型
/// </summary>
public enum RoomType
{
    Room_1,
    Room_2,
    Room_3,
    Room_4,
    Room_5,
    Room_6,
    Room_7,
}

/// <summary>
/// 预制件名字
/// </summary>
public enum PrefabName
{
    TiggerTipsPrefab,    //提示图标
    BigPhoto,            //照片特写
    phonographPrefab,    //留声机
    SpecialGlasses,      //镜子特写
    Tone,                //蓝光
    Mask,                //遮罩
    BrickBlue,
}

/// <summary>
///物体路径类型
/// </summary>
public enum ObjectPathType
{
    Room,
    Tigger,
}
