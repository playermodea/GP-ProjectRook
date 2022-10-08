using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour//, IStatus
{ 
    public float MAXHP { get; set; } // 체력 최대치
    public float HP { get; set; } // 체력값(맞거나 아이템먹거나 바뀔수치)
    public float AttackPoint { get; set; } // 공격력
    public float AvoidPoint { get; set; } // 회피력
    public float TurnActionPoint { get; set; } // 턴 행동력
    public float UPMovePoint { get; set; } // ↑ 신속력
    public float UP_RightMovePoint { get; set; } // ↗ 신속력
    public float UP_LeftMovePoint { get; set; } // ↖ 신속력
    public float LeftMovePoint { get; set; } // ← 신속력
    public float RightMovePoint { get; set; } // → 신속력
    public float Down_MovePoint { get; set; } // ↓ 신속력
    public float Down_RightMovePoint { get; set; } // ↘ 신속력
    public float Down_LeftMovePoint { get; set; } // ↙ 신속력
    public virtual void OnDamage(int row, int column, float damage)
    {
        HP -= damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        MAXHP = 3;
        HP = MAXHP;
        AttackPoint = 0;
        AvoidPoint = 0;
        TurnActionPoint = 4;

        UPMovePoint = 2.0f;
        UP_LeftMovePoint = 2.0f;
        UP_RightMovePoint = 2.0f;
        LeftMovePoint = 2.0f;
        RightMovePoint = 2.0f;
        Down_MovePoint = 2.0f;
        Down_LeftMovePoint = 2.0f;
        Down_RightMovePoint = 2.0f;
        //플레이어 디폴트 스탯
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
