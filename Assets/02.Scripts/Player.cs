using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Status
{
    [SerializeField] private float maxHP;
    [SerializeField] private float evaPoint;       // 회피력
    [field: SerializeField] public int maxActionPoint { get; protected set; }       // 행동력 (턴 계획)
    [field: SerializeField] public int curActionPoint { get; set; }
    [SerializeField] private int[] movePoint;        // 이동력 (해당 방향 최대 이동)
    [field: SerializeField] public int gold { get; protected set; }          // 골드

    public PlayerSkillType curSkillType { get; set; }
    public Direction curSkillDir { get; set; }

    private Stack<PlayerState> statePlan = new Stack<PlayerState>();
    private Stack<PlayerDestinationInfo> movePlan = new Stack<PlayerDestinationInfo>();
    private Stack<PlayerAttackPlanInfo> attackPlan = new Stack<PlayerAttackPlanInfo>();

    [field: SerializeField] public bool isTurn { get; set; }
    [field: SerializeField] public bool isPlaning { get; set; }
    [field: SerializeField] public bool isAttackPlaning { get; set; }
    [field: SerializeField] public bool isMovePlaning { get; set; }
    [field: SerializeField] public bool isDamaged { get; set; }

    private SpriteRenderer renderer;
    public GameObject selectTile;
    public GameObject[] afterimage = new GameObject[10];
    public PlayerMovement movement { get; protected set; }
    public PlayerAttackSystem attack { get; protected set; }
    public PlayerSkill skill { get; protected set; }

    // 초기화 관련 함수
    private void Awake()
    {
        // 기본 스탯 설정
        maxHP = 8.0f;
        startHP = 8.0f;
        attackDamage = 1.0f;
        evaPoint = 0.0f;
        gold = 0;
        maxActionPoint = 4;
        curActionPoint = 0;
        movePoint = new int[8];
        for (int i = 0; i < 8; i++)
        {
            movePoint[i] = 2;
        }

        // 제한사항 설정
        isDead = false;
        isTurn = false;
        isPlaning = false;
        isDamaged = false;
        isAttackPlaning = false;
        isMovePlaning = false;
        curSkillType = PlayerSkillType.None;
        curSkillDir = Direction.RIGHT;

        // 컴포넌트 설정
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttackSystem>();
        skill = GetComponent<PlayerSkill>();
        renderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        EffectManager.Instance.SetPlayer();
        UIManager.Instance.SetPlayer();
        UIManager.Instance.SetPlayerDMG(attackDamage);
        UIManager.Instance.SetPlayerEva(evaPoint);
        UIManager.Instance.SetPlayerGold(gold);
        UIManager.Instance.SetSlot(PlayerSkillType.RisingSlash, 1, true);
        UIManager.Instance.SetSlot(PlayerSkillType.StampingFeet, 2, true);
        UIManager.Instance.SetHPSlot(hp, maxHP);
    }

    // 업데이트 함수
    private void Update()
    {
        if (!isDead && isPlaning)
        {
            Plan();

            if (Input.GetKeyDown(KeyCode.W))
            {
                curSkillDir = Direction.UP;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                curSkillDir = Direction.LEFT;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                curSkillDir = Direction.DOWN;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                curSkillDir = Direction.RIGHT;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UIManager.Instance.OnSkillSlot_1();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UIManager.Instance.OnSkillSlot_2();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UIManager.Instance.OnSkillSlot_3();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                UIManager.Instance.OnSkillSlot_4();
            }
            if (Input.GetMouseButtonDown(1))
            {
                isAttackPlaning = false;
                isMovePlaning = true;
            }
        }
        else if (isDead)
        {
            movement.floorTile.RefreshTile();
        }
    }

    // 계획 관련 함수
    private void Plan()
    {
        if (curActionPoint < maxActionPoint)
        {
            if (isMovePlaning)
            {
                movement.floorTile.RefreshTile();
                for (int i = 0; i < 8; i++)
                {
                    movement.floorTile.ShowPlayerMoveTile(movement.cellRow, movement.cellColumn, Direction.UP + i, movePoint[i], movePlan);
                }
                if (movement.floorTile.ShowMovePlanMousePoint(movement.cellRow, movement.cellColumn, movePlan, statePlan))
                {
                    PlayerDestinationInfo desInfo = movePlan.Peek();

                    for (int i = 0; i < 10; i++)
                    {
                        if (!afterimage[i].activeSelf)
                        {
                            afterimage[i].transform.position = new Vector3(movement.floorTile.tileInfo[desInfo.row, desInfo.column].tilePos.x,
                                                                        movement.floorTile.tileInfo[desInfo.row, desInfo.column].tilePos.y,
                                                                        0);
                            afterimage[i].SetActive(true);
                            break;
                        }
                    }

                    curActionPoint = statePlan.Count;
                    UIManager.Instance.SetEnergySlot(maxActionPoint - curActionPoint, maxActionPoint);
                }
            }
            else if (isAttackPlaning)
            {
                int tempRow = movePlan.Count != 0 ? movePlan.Peek().row : movement.cellRow;
                int tempColumn = movePlan.Count != 0 ? movePlan.Peek().column : movement.cellColumn;

                movement.floorTile.RefreshTile();
                attack.ShowAttackRange(tempRow, tempColumn, skill.GetPlayerSkill(curSkillType, curSkillDir));
                if (movement.floorTile.ShowAttackPlanMousePoint(curSkillType, curSkillDir, attackPlan, statePlan))
                {
                    isAttackPlaning = false;
                    isMovePlaning = true;
                    curActionPoint = statePlan.Count;
                    UIManager.Instance.SetEnergySlot(maxActionPoint - curActionPoint, maxActionPoint);
                }
            }
        }
        else
        {
            movement.floorTile.RefreshTile();
        }
    }

    public void UndoPlan()
    {
        if (statePlan.Count != 0)
        {
            if (statePlan.Peek() == PlayerState.MOVE)
            {
                movePlan.Pop();

                for (int i = 9; i >= 0; i--)

                {
                    if (afterimage[i].activeSelf)
                    {
                        afterimage[i].transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
                        afterimage[i].SetActive(false);
                        break;
                    }
                }
            }
            else if (statePlan.Peek() == PlayerState.ATTACK)
            {
                attackPlan.Pop();
            }

            statePlan.Pop();
            curActionPoint = statePlan.Count;
            UIManager.Instance.SetEnergySlot(maxActionPoint - curActionPoint, maxActionPoint);
        }
    }

    // 턴 관련 함수
    private void Move()
    {
        PlayerDestinationInfo desInfo = movePlan.Pop();

        movement.Move(desInfo.dir, desInfo.movePoint);
        StartCoroutine(MoveCheck());
    }

    private void AttackEffect()
    {
        movement.animator.SetBool("isAttack", true);
        EffectManager.Instance.CreatePlayerSkillEffect(transform.position, attackPlan.Peek().type, attackPlan.Peek().dir);
    }

    public void Attack()
    {
        PlayerAttackPlanInfo attackInfo = attackPlan.Pop();

        attack.Attack(skill.GetSkillDamage(attackDamage, attackInfo.type), skill.GetPlayerSkill(attackInfo.type, attackInfo.dir));
        StartCoroutine(AttackCheck());
    }

    public void Action()
    {
        if (isTurn)
        {
            if (statePlan.Count != 0)
            {
                switch (statePlan.Pop())
                {
                    case PlayerState.MOVE:
                        Move();
                        break;
                    case PlayerState.ATTACK:
                        AttackEffect();
                        break;
                    default:
                        break;
                }
            }
            else if (!isDamaged)
            {
                TurnManager.Instance.PlayerTurnEnd();
            }
        }
    }

    // 데미지 피해 및 스탯 수정 관련 함수
    public override void OnDamage(int row, int column, float damage)
    {
        if (!isDamaged)
        {
            float evaCalc = (float)(evaPoint * 0.01);
            bool evaSuccess = (Random.value < evaCalc);
            GameObject target = movement.floorTile.tileInfo[row, column].tileObj;
            Direction dir;

            if (!evaSuccess || movement.isMoveDamaged || (target != null && target.GetComponent<EnemyMovement>().isMoveAttack))
            {
                base.OnDamage(row, column, damage);

                if (movement.isMoveDamaged)
                {
                    movement.isMoveDamaged = false;
                }

                renderer.color = new Color(255.0f / 255.0f, 0.0f, 0.0f);
                movement.animator.SetBool("OnDamage", true);
                EffectManager.Instance.CreateHitEffect(transform.position);
                ScoreManager.Instance.SetTotalDamagedCount();
                UIManager.Instance.SetHPSlot(hp, maxHP);

                isDamaged = true;
                dir = movement.floorTile.KnockbackDirection(movement.cellRow, movement.cellColumn, row, column);
                dir = movement.KnockbackCheck(dir);
                movement.Move(dir, 1);
                movePlan.Clear();
                statePlan.Clear();
                attackPlan.Clear();
                curActionPoint = 0;

                UIManager.Instance.CameraShake();

                if (target != null && target != gameObject && target.GetComponent<EnemyMovement>().isMoveAttack)
                {
                    StartCoroutine(MoveCheck(target));
                }
                else
                {
                    StartCoroutine(MoveCheck());
                }

                if (hp <= 0.0f)
                {
                    isDead = true;
                    maxActionPoint = 0;
                    UIManager.Instance.Gameover();
                }
                if (hp > 0.0f)
                {
                    if (isDamaged)
                    {
                        AudioPlay.Instance.PlaySound("Under_Attack");
                    }
                }

                return;
            }

            if (evaSuccess)
            {
                UIManager.Instance.Dialog_Text("공격을 회피했습니다.");
                UIManager.Instance.Full_Dialog_Text("공격을 회피했습니다.");
            }
        }
    }

    public override void SetHP(float value)
    {
        base.SetHP(value);

        if (hp > maxHP)
        {
            hp = maxHP;
        }

        UIManager.Instance.SetHPSlot(hp, maxHP);
    }

    public void SetMaxHP(float value)
    {
        maxHP += value;

        if (maxHP > 20.0f)
        {
            maxHP = 20.0f;
        }

        SetHP(value);
    }

    public override void SetDamage(float value)
    {
        base.SetDamage(value);

        UIManager.Instance.SetPlayerDMG(attackDamage);
    }

    public void SetMaxActionPoint(int value)
    {
        maxActionPoint += value;

        if (maxActionPoint > 8)
        {
            maxActionPoint = 8;
        }
        else if (maxActionPoint < 1)
        {
            maxActionPoint = 1;
        }
    }

    public void SetGold(int value)
    {
        gold += value;

        UIManager.Instance.SetPlayerGold(gold);
    }

    public void SetEvasion(float value, bool isCheat = false)
    {
        evaPoint += value;

        if (evaPoint > 20.0f && !isCheat)
        {
            evaPoint = 20.0f;
        }

        UIManager.Instance.SetPlayerEva(evaPoint);
    }

    public void SetMovePoint(int value, Direction dir)
    {
        switch(dir)
        {
            case Direction.UP:
                movePoint[0] += value;
                break;
            case Direction.UP_RIGHT:
                movePoint[1] += value;
                break;
            case Direction.RIGHT:
                movePoint[2] += value;
                break;
            case Direction.RIGHT_DOWN:
                movePoint[3] += value;
                break;
            case Direction.DOWN:
                movePoint[4] += value;
                break;
            case Direction.DOWN_LEFT:
                movePoint[5] += value;
                break;
            case Direction.LEFT:
                movePoint[6] += value;
                break;
            case Direction.LEFT_UP:
                movePoint[7] += value;
                break;
        }

        for (int i = 0; i < movePoint.Length; i++)
        {
            if (movePoint[i] > 16)
            {
                movePoint[i] = 16;
            }
            else if (movePoint[i] < 1)
            {
                movePoint[i] = 1;
            }
        }
    }

    // Sorting
    public void SortingPlanStack()
    {
        Queue<PlayerState> tempStateQ = new Queue<PlayerState>();
        Queue<PlayerDestinationInfo> tempMoveQ = new Queue<PlayerDestinationInfo>();
        Queue<PlayerAttackPlanInfo> tempAttackQ = new Queue<PlayerAttackPlanInfo>();

        while (statePlan.Count != 0)
        {
            tempStateQ.Enqueue(statePlan.Pop());
        }
        while (movePlan.Count != 0)
        {
            tempMoveQ.Enqueue(movePlan.Pop());
        }
        while (attackPlan.Count != 0)
        {
            tempAttackQ.Enqueue(attackPlan.Pop());
        }

        while (tempStateQ.Count != 0)
        {
            statePlan.Push(tempStateQ.Dequeue());
        }
        while (tempMoveQ.Count != 0)
        {
            movePlan.Push(tempMoveQ.Dequeue());
        }
        while (tempAttackQ.Count != 0)
        {
            attackPlan.Push(tempAttackQ.Dequeue());
        }
    }

    // 맵 이동 관련 함수
    private void MapSwitching()
    {
        isTurn = false;
        isPlaning = false;
        isDamaged = false;
        isAttackPlaning = false;
        isMovePlaning = false;
        curSkillType = PlayerSkillType.None;
        curSkillDir = Direction.RIGHT;
        statePlan.Clear();
        movePlan.Clear();
        attackPlan.Clear();
        movement.floorTile.RefreshTile();
        TurnManager.Instance.MapSwitching(movement.floorTile);
    }

    public void StageSwitching()
    {
        isTurn = false;
        isPlaning = false;
        isDamaged = false;
        isAttackPlaning = false;
        isMovePlaning = false;
        selectTile.SetActive(false);
        curSkillType = PlayerSkillType.None;
        curSkillDir = Direction.RIGHT;
        statePlan.Clear();
        movePlan.Clear();
        attackPlan.Clear();
        movement.StageSwitching();
    }

    // 행동 체크 관련 함수
    private IEnumerator MoveCheck(GameObject target = null)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!movement.isMove)
            {
                if (movement.MapSwitching())
                {
                    MapSwitching();
                    break;
                }

                if (movement.isMoveDamaged)
                {
                    OnDamage(movement.moveDamagedTileInfo.row, movement.moveDamagedTileInfo.column, movement.moveDamagedTileInfo.tileObj.GetComponent<Enemy>().attackDamage);
                    isDamaged = false;
                    break;
                }

                renderer.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                movement.animator.SetBool("OnDamage", false);

                if (!isDamaged)
                {
                    TurnManager.Instance.PlayerTurnEnd();
                    break;
                }
                else
                {
                    if (target != null && target.GetComponent<EnemyMovement>().isMoveAttack)
                    {
                        target.GetComponent<Enemy>().CheckEnemyAvailablePoint();
                    }
                    isDamaged = false;
                    break;
                }
            }
        }
    }

    private IEnumerator AttackCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!attack.isAttack)
            {
                movement.animator.SetBool("isAttack", false);
                movement.floorTile.RefreshTile();
                TurnManager.Instance.PlayerTurnEnd();
                break;
            }
        }
    }
}
