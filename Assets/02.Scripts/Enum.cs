// 방향 Enum
public enum Direction
{
    NONE = -1,

    UP,
    UP_RIGHT,

    RIGHT,
    RIGHT_DOWN,

    DOWN,
    DOWN_LEFT,

    LEFT,
    LEFT_UP,

    StandInPlace
}

// Enemy 상태 Enum
public enum EnemyState
{
    NONE = -1,

    ATTACK,
    MOVE
}

// Enemy Type Enum
public enum EnemyType
{
    NONE = -1,

    A,
    B
}

public enum TrapState
{
    NONE = -1,

    STOP,
    ATTACK
}

// Player 상태 Enum
public enum PlayerState
{
    NONE = -1,

    ATTACK,
    MOVE
}

// Player Skill Enum
public enum PlayerSkillType
{
    None = -1,

    Swing,
    StampingFeet,
    SonicWave,
    ThrowingBobber,
    CrackTheSky,
    RisingSlash,
    ForwardSlash,
    Digging,
    ThrowingBoom,
    SideSlash,
    SpinningSlash,
    SmashSlam,
    HitTheMark,
    SlingShot,
    DiffusionExplosion
}