using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyQueen : Enemy, IEnemy
{
    private int afterimageCount = 0;
    private int spawnEnemyCount = 0;
    private int downAttackCount = 0;
    private int diamondAttackCount = 0;

    private float shakeTime;
    private float shakeAmount;
    private float alphaAmount;

    private bool isAlphaDead = false;
    private bool isShake = false;
    private bool isSpawnEnemyAttack = false;
    private bool isDownAttack = false;
    private bool isDiamondAttack = false;

    private Vector3 originPos;
    private Direction attackDir;

    public GameObject bomb;
    public GameObject[] enemyArray;
    public GameObject[] afterimage;
    public GameObject textCanvas;

    protected override void Start()
    {
        startHP = 150.0f;
        shakeTime = 0.5f;
        shakeAmount = 0.1f;
        alphaAmount = 255.0f;
        state = EnemyState.ATTACK;
        attackDir = Direction.NONE;

        base.Start();

        animator = transform.parent.GetComponent<Animator>();
        renderer = transform.parent.GetComponent<SpriteRenderer>();
        StartCoroutine(InitBoss());
    }

    private void Update()
    {
        if (isShake)
        {
            if (shakeTime >= 0.0f)
            {
                transform.parent.position = Random.insideUnitSphere * shakeAmount + originPos;
                shakeTime -= Time.deltaTime;
            }
            else
            {
                isShake = false;
                shakeTime = 0.5f;
                transform.parent.position = originPos;
                renderer.color = new Color(255.0f, 255.0f, 255.0f);
            }
        }

        if (isAlphaDead)
        {
            alphaAmount -= Time.deltaTime * 150.0f;
            renderer.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, alphaAmount / 255.0f);

            if (alphaAmount <= 0.0f)
            {
                isAlphaDead = false;
            }
        }
    }

    public virtual void Action()
    {
        if (textCanvas != null)
        {
            StartCoroutine(DestroyText());
        }

        if (isTurn && !isDead)
        {
            switch (state)
            {
                case EnemyState.MOVE:
                    Move();
                    break;
                case EnemyState.ATTACK:
                    Attack();
                    break;
                default:
                    break;
            }
        }
    }

    public virtual void Move()
    {

    }

    public virtual void Attack()
    {
        if (!isSpawnEnemyAttack && !isDownAttack && !isDiamondAttack)
        {
            int randomAttack = Random.Range(0, 6);

            switch (randomAttack)
            {
                case 0:
                    SpawnTrapAttack();
                    break;
                case 1:
                    SpawnEnemyAttack();
                    isSpawnEnemyAttack = true;
                    break;
                case 2:
                    DownAttack();
                    isDownAttack = true;
                    break;
                case 3:
                    DiamondAttack();
                    isDiamondAttack = true;
                    break;
                case 4:
                    ShowAfterimage(Direction.UP);
                    break;
                case 5:
                    ShowAfterimage(Direction.RIGHT);
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (isSpawnEnemyAttack)
            {
                SpawnEnemyAttack();
            }
            else if (isDownAttack)
            {
                DownAttack();
            }
            else if (isDiamondAttack)
            {
                DiamondAttack();
            }
        }
    }

    private void SpawnTrapAttack()
    {
        Vector3 spawnPos;

        animator.SetBool("isAttack", true);
        for (int i = 0; i < 5; i++)
        {
            int row = Random.Range(1, 16);
            int column = Random.Range(1, 16);

            while (!movement.floorTile.tileInfo[row, column].isPassable || movement.floorTile.tileInfo[row, column].isTrap)
            {
                row = Random.Range(1, 16);
                column = Random.Range(1, 16);
            }
            spawnPos = movement.floorTile.tileInfo[row, column].tilePos;
            Instantiate(bomb, spawnPos, Quaternion.identity);
        }
        StartCoroutine(AttackCheck());
    }

    private void SpawnEnemyAttack()
    {
        if (movement.floorTile.EnemyCount() > 1 && spawnEnemyCount == 0)
        {
            isSpawnEnemyAttack = false;
            Attack();
            return;
        }

        if (spawnEnemyCount == 0)
        {
            animator.SetBool("isAttack", true);

            int randomEnemy_1 = Random.Range(0, 6);
            int randomEnemy_2 = Random.Range(0, 6);

            while (randomEnemy_1 == randomEnemy_2)
            {
                randomEnemy_2 = Random.Range(0, 6);
            }

            Vector3 spawnPos_1 = movement.floorTile.tileInfo[movement.cellRow + 2, movement.cellColumn].tilePos;
            Vector3 spawnPos_2 = movement.floorTile.tileInfo[movement.cellRow + 2, movement.cellColumn + 1].tilePos;
            GameObject spawnEnemy_1 = Instantiate(enemyArray[randomEnemy_1], spawnPos_1, Quaternion.identity);
            GameObject spawnEnemy_2 = Instantiate(enemyArray[randomEnemy_2], spawnPos_2, Quaternion.identity);

            spawnEnemy_1.transform.parent = transform.parent.parent;
            spawnEnemy_2.transform.parent = transform.parent.parent;

            spawnEnemyCount++;
        }
        else
        {
            spawnEnemyCount++;

            if (spawnEnemyCount >= 2)
            {
                isSpawnEnemyAttack = false;
                spawnEnemyCount = 0;
            }
        }
        StartCoroutine(AttackCheck());
    }

    private void DownAttack()
    {
        if (downAttackCount == 0)
        {
            animator.SetBool("isAttack", true);

            attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.BossBasicAttackRangeInfo, Direction.DOWN));
            downAttackCount++;
        }
        else
        {
            isDownAttack = false;
            downAttackCount = 0;
        }
        StartCoroutine(AttackCheck());
    }

    private void DiamondAttack()
    {
        animator.SetBool("isAttack", true);

        switch (diamondAttackCount)
        {
            case 0:
                attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.BossBasicAttackRangeInfo, Direction.DOWN));
                break;
            case 1:
                attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.BossBasicAttackRangeInfo, Direction.LEFT));
                break;
            case 2:
                attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.BossBasicAttackRangeInfo, Direction.UP));
                break;
            case 3:
                attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.BossBasicAttackRangeInfo, Direction.RIGHT));
                break;
            default:
                break;
        }
        StartCoroutine(AttackCheck());
        diamondAttackCount++;

        if (diamondAttackCount > 3)
        {
            isDiamondAttack = false;
            diamondAttackCount = 0;
        }
    }

    private void DownUpAttack()
    {
        attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.FairyQueenAttackRangeInfo, Direction.UP));
        StartCoroutine(AttackCheck());
    }

    private void LeftRightAttack()
    {
        attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.FairyQueenAttackRangeInfo, Direction.RIGHT));
        StartCoroutine(AttackCheck());
    }

    private void ShowAfterimage(Direction dir)
    {
        animator.SetBool("isAttack", true);

        if (dir == Direction.UP)
        {
            afterimage[0].SetActive(true);
            afterimage[0].GetComponent<EnemyAfterimage>().SetDestination(transform.parent.position + new Vector3(0.0f, 4.0f, 0.0f));
            afterimage[1].SetActive(true);
            afterimage[1].GetComponent<EnemyAfterimage>().SetDestination(transform.parent.position + new Vector3(0.0f, -4.0f, 0.0f));
        }
        else if (dir == Direction.RIGHT)
        {
            afterimage[0].SetActive(true);
            afterimage[0].GetComponent<EnemyAfterimage>().SetDestination(transform.parent.position + new Vector3(4.0f, 0.0f, 0.0f));
            afterimage[1].SetActive(true);
            afterimage[1].GetComponent<EnemyAfterimage>().SetDestination(transform.parent.position + new Vector3(-4.0f, 0.0f, 0.0f));
        }

        attackDir = dir;
    }

    public void AfterimageArriveDestination()
    {
        afterimageCount++;

        if (afterimageCount == 2)
        {
            if (attackDir == Direction.UP)
            {
                DownUpAttack();
            }
            else if (attackDir == Direction.RIGHT)
            {
                LeftRightAttack();
            }

            attackDir = Direction.NONE;
            afterimageCount = 0;
        }
    }

    public override void OnDamage(int row, int column, float damage)
    {
        hp -= damage;

        UpdateHealthUI();
        EffectManager.Instance.CreateHitEffect(transform.position);

        isDamaged = true;
        renderer.color = new Color(255.0f / 255.0f, 0.0f, 0.0f);
        AudioPlay.Instance.PlaySound("Attack_Boss");
        isShake = true;
        originPos = transform.parent.position;
        StartCoroutine(ShakeCheck());
    }

    public override void OnDie()
    {
        GameObject dropI = Instantiate(dropItem[0], movement.floorTile.tileInfo[movement.cellRow, movement.cellColumn].tilePos, Quaternion.identity);
        dropI.transform.parent = transform.parent.parent;

        isDead = true;
        movement.floorTile.SetBossDieTile(gameObject);
        renderer.enabled = false;
        TurnManager.Instance.EnemyDeadAnimationComplete();
    }

    protected override IEnumerator AttackCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (!attack.isAttack)
            {
                isTurn = false;
                SetAnimation();
                TurnManager.Instance.EnemyTurnEnd();
                break;
            }
        }
    }

    protected override IEnumerator ShakeCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (isShake)
            {
                if (hp <= 0.0f)
                {
                    TurnManager.Instance.EnemyDeadAnimationDelay();
                    AudioPlay.Instance.PlaySound("Stage_Clear");
                    AudioPlay.Instance.PlaySound("Boss_Dead");
                    isAlphaDead = true;
                    StartCoroutine(AlphaCheck());
                }
                else
                {
                    SetAnimation();
                }
                break;
            }
        }
    }

    protected override IEnumerator AlphaCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (!isAlphaDead)
            {
                OnDie();
                break;
            }
        }
    }

    protected override void SetAnimation()
    {
        animator.SetBool("isAttack", false);
    }

    private IEnumerator InitBoss()
    {
        yield return new WaitForSeconds(1.0f);

        movement.floorTile.InitTileBossObject(gameObject);
    }

    private IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(1.0f);

        Destroy(textCanvas);
    }
}
