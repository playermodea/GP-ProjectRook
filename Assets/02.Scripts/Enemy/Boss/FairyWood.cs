using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyWood : Enemy, IEnemy
{
    private int count = 0;
    private int nextPattern = 0;
    private int waveCount = 0;
    private int maxWave = 0;

    private float shakeTime;
    private float shakeAmount;
    private float alphaAmount;

    private bool isAlphaDead = false;
    private bool isShake = false;
    private Vector3 originPos;

    public GameObject[] waveAttackArray;
    public GameObject textCanvas;

    protected override void Start()
    {
        startHP = 150.0f;
        shakeTime = 0.5f;
        shakeAmount = 0.1f;
        alphaAmount = 255.0f;
        state = EnemyState.ATTACK;

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
        animator.SetBool("isAttack", true);

        switch (nextPattern)
        {
            case 0:
                PatternA();
                break;
            case 1:
                PatternB();
                break;
            case 2:
                PatternC();
                break;
            case 3:
                PatternD();
                break;
            case 4:
                PatternE();
                break;
            case 5:
                PatternF();
                break;
            default:
                break;
        }

        for (int i = 0; i < waveAttackArray.Length; i++)
        {
            if (waveAttackArray[i].GetComponent<FairyWoodWaveAttack>().isActive)
            {
                maxWave++;
            }
            waveAttackArray[i].GetComponent<FairyWoodWaveAttack>().Action();
        }
    }

    private void PatternA()
    {
        if (count == 0)
        {
            waveAttackArray[0].GetComponent<FairyWoodWaveAttack>().isActive = true;
        }
        count++;

        if (count >= 4)
        {
            nextPattern = 1;
            count = 0;
        }
    }

    private void PatternB()
    {
        if (count == 0)
        {
            waveAttackArray[1].GetComponent<FairyWoodWaveAttack>().isActive = true;
        }
        count++;

        if (count >= 4)
        {
            nextPattern = 2;
            count = 0;
        }
    }

    private void PatternC()
    {
        if (count == 0)
        {
            waveAttackArray[2].GetComponent<FairyWoodWaveAttack>().isActive = true;
        }
        count++;

        if (count >= 4)
        {
            nextPattern = 3;
            count = 0;
        }
    }

    private void PatternD()
    {
        if (count == 0)
        {
            waveAttackArray[3].GetComponent<FairyWoodWaveAttack>().isActive = true;
        }
        count++;

        if (count >= 4)
        {
            nextPattern = 4;
            count = 0;
        }
    }

    private void PatternE()
    {
        if (count == 0)
        {
            attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.BossBasicAttackRangeInfo, Direction.DOWN));
            //StartCoroutine(AttackCheck());
        }
        count++;

        if (count >= 2)
        {
            nextPattern = 5;
            count = 0;
        }
    }

    private void PatternF()
    {
        attack.Attack(attackDamage, EnemyPatternManager.Instance.ConvertArray(EnemyPatternManager.Instance.FairyWoodAttack_2_RangeInfo, Direction.UP + count));
        //StartCoroutine(AttackCheck());

        count += 2;

        if (count >= 6)
        {
            nextPattern = 0;
            count = 0;
        }
    }

    public override void OnDamage(int row, int column, float damage)
    {
        hp -= damage;

        UpdateHealthUI();
        EffectManager.Instance.CreateHitEffect(transform.position);
        animator.SetBool("OnDamage", true);

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

            if (!movement.isShake)
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

    public void CheckWaveAttack()
    {
        waveCount++;

        if (waveCount == maxWave)
        {
            waveCount = 0;
            maxWave = 0;

            StartCoroutine(AttackCheck());
        }
    }

    protected override void SetAnimation()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("OnDamage", false);
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