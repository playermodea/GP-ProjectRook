using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    private float shakeTime;
    private float shakeAmount;
    private float alphaAmount;
    private FloorTileInfo finalTileInfo;
    private FloorTileInfo attackTileInfo;

    public Vector3 originPos { get; set; }

    [field: SerializeField] public int cellRow { get; set; }
    [field: SerializeField] public int cellColumn { get; set; }
    [field: SerializeField] public int availablePoint { get; set; }

    [field: SerializeField] public bool isMove { get; private set; }
    [field: SerializeField] public bool isShake { get; set; }
    [field: SerializeField] public bool isDead { get; set; }
    [field: SerializeField] public bool isMoveAttack { get; set; }
    [field: SerializeField] public Direction moveDirection { get; set; }

    public FloorTile floorTile { get; private set; }
    public SpriteRenderer renderer { get; set; }
    public EnemyAttackSystem attack { get; private set; }

    private void Awake()
    {
        isMove = false;
        isShake = false;
        isMoveAttack = false;
        moveDirection = Direction.NONE;
        availablePoint = 0;
        shakeTime = 0.5f;
        shakeAmount = 0.1f;
        alphaAmount = 255.0f;

        renderer = GetComponent<SpriteRenderer>();
        attack = GetComponent<EnemyAttackSystem>();
    }

    private void Start()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Floor");
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.forward, 10.0f, layerMask);
        if (rayHit)
        {
            floorTile = rayHit.transform.GetComponent<FloorTile>();
            floorTile.InitTileObject(gameObject);
        }
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.Lerp(transform.position, finalTileInfo.tilePos, 5.0f * Time.deltaTime);

            if (Vector3.Distance(finalTileInfo.tilePos, transform.position) <= 0.05f)
            {
                transform.position = finalTileInfo.tilePos;
                isMove = false;

                if (isMoveAttack)
                {
                    attack.MoveAttack(attackTileInfo.row, attackTileInfo.column);
                }
            }
        }

        if (isShake)
        {
            if (shakeTime >= 0.0f)
            {
                transform.position = Random.insideUnitSphere * shakeAmount + originPos;
                shakeTime -= Time.deltaTime;
            }
            else
            {
                isShake = false;
                shakeTime = 0.5f;
                transform.position = originPos;
                renderer.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            }
        }

        if (isDead)
        {
            alphaAmount -= Time.deltaTime * 250.0f;
            renderer.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, alphaAmount / 255.0f);

            if (alphaAmount / 255.0f <= 0.0f)
            {
                isDead = false;
            }
        }
    }

    public bool Move(Direction dir, int point)
    {
        moveDirection = dir;
        availablePoint = point;
        FloorTileInfo destinationTile = floorTile.CheckDestinationTile(cellRow, cellColumn, moveDirection, availablePoint);
        FloorTileInfo nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, moveDirection);
        finalTileInfo = floorTile.tileInfo[cellRow, cellColumn];

        while (nextTileInfo.isPassable)
        {
            if (cellRow == destinationTile.row && cellColumn == destinationTile.column)
            {
                break;
            }

            floorTile.SetTileInformation(gameObject, nextTileInfo);
            finalTileInfo = nextTileInfo;
            cellRow = nextTileInfo.row;
            cellColumn = nextTileInfo.column;
            nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, moveDirection);
            availablePoint--;
        }

        isMove = true;

        if (nextTileInfo.tileObj != null && nextTileInfo.tileObj.tag == "Player" && (cellRow != destinationTile.row || cellColumn != destinationTile.column))
        {
            isMoveAttack = true;
            attackTileInfo = nextTileInfo;
            //attack.MoveAttack(nextTileInfo.row, nextTileInfo.column);
        }
        else if (((nextTileInfo.tileObj == null || nextTileInfo.tileObj.tag == "Enemy") && !nextTileInfo.isPassable) || (cellRow == 0 || cellRow == 16 || cellColumn == 0 || cellColumn == 16))
        {
            //isMoveAttack = false;
            return false;
        }

        return true;
    }

    public Direction KnockbackCheck(Direction dir)
    {
        FloorTileInfo nextTileInfo;

        nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, dir);

        if (nextTileInfo.isPassable)
        {
            return dir;
        }

        for (int i = 1; i < 4; i++)
        {
            if (dir + i > Direction.LEFT_UP)
            {
                nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, dir + i - 8);

                if (nextTileInfo.isPassable)
                {
                    return dir + i - 8;
                }
            }
            else
            {
                nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, dir + i);

                if (nextTileInfo.isPassable)
                {
                    return dir + i;
                }
            }

            if (dir - i < Direction.UP)
            {
                nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, dir - i + 8);

                if (nextTileInfo.isPassable)
                {
                    return dir - i + 8;
                }
            }
            else
            {
                nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, dir - i);

                if (nextTileInfo.isPassable)
                {
                    return dir - i;
                }
            }
        }

        return dir;
    }
}
