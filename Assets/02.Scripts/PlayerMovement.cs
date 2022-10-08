using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    private int exitMin = 0;
    private int exitMax = 16;
    public Animator animator { get; private set; }
    private SpriteRenderer renderer;
    private FloorTileInfo finalTileInfo;
    public FloorTileInfo moveDamagedTileInfo { get; private set; }
    
    [field: SerializeField] public int cellRow { get; set; }
    [field: SerializeField] public int cellColumn { get; set; }

    [field: SerializeField] public bool isMove { get; private set; }
    [field: SerializeField] public bool isMoveDamaged { get; set; }
    public FloorTile floorTile { get; private set; }

    private void Awake()
    {
        isMove = false;
        isMoveDamaged = false;

        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetMap();

        TurnManager.Instance.SetPlayer();
        TurnManager.Instance.MapSwitching(floorTile);
        floorTile.MiniMapUpdate();
    }

    private void SetMap()
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
                if(!isMove)
                {
                    AudioPlay.Instance.StopMusic();
                }
                MoveAnimation(Direction.NONE);
            }
        }
    }

    private void MoveAnimation(Direction dir)
    {
        if (dir == Direction.DOWN)
        {
            animator.SetBool("isForward", true);
        }
        else if (dir == Direction.UP)
        {
            animator.SetBool("isBack", true);
        }
        else if (dir == Direction.RIGHT || dir == Direction.UP_RIGHT || dir == Direction.RIGHT_DOWN)
        {
            animator.SetBool("isHorizontal", true);
        }
        else if (dir == Direction.LEFT || dir == Direction.LEFT_UP || dir == Direction.DOWN_LEFT)
        {
            animator.SetBool("isHorizontal", true);
            renderer.flipX = true;
        }
        else if (dir == Direction.NONE)
        {
            animator.SetBool("isForward", false);
            animator.SetBool("isBack", false);
            animator.SetBool("isHorizontal", false);
            renderer.flipX = false;
        }
    }

    public bool Move(Direction dir, int point)
    {
        FloorTileInfo destinationTile = floorTile.CheckDestinationTile(cellRow, cellColumn, dir, point);
        FloorTileInfo nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, dir);
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
            nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, dir);
        }

        MoveAnimation(dir);
        isMove = true;
        if(isMove)
        {
            AudioPlay.Instance.PlaySound("Move");
        }

        if ((cellRow != destinationTile.row || cellColumn != destinationTile.column) && nextTileInfo.tileObj != null && nextTileInfo.tileObj.tag == "Enemy")
        {
            isMoveDamaged = true;
            moveDamagedTileInfo = nextTileInfo;
        }

        return true;
    }

    public bool MapSwitching()
    {
        if ((cellRow == exitMin || cellRow == exitMax || cellColumn == exitMin || cellColumn == exitMax) && floorTile.enemyList.Count == 0)
        {
            float movePoint = 4.0f;

            isMove = false;
            floorTile.ExitPlayer(cellRow, cellColumn);
            floorTile.MiniMapUpdate();

            if (cellRow == exitMin)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + movePoint, 0.0f);
            }
            else if (cellRow == exitMax)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movePoint, 0.0f);
            }
            else if (cellColumn == exitMin)
            {
                transform.position = new Vector3(transform.position.x - movePoint, transform.position.y, 0.0f);
            }
            else if (cellColumn == exitMax)
            {
                transform.position = new Vector3(transform.position.x + movePoint, transform.position.y, 0.0f);
            }
            SetMap();
            floorTile.MiniMapUpdate();

            if (floorTile.BossCheck())
            {
                MusicPlay.Instance.PlayBossMapBackgroundMusic(MapManager.Instance.stageNumber);
            }
            else
            {
                MusicPlay.Instance.PlayBackgroundMusic(MapManager.Instance.stageNumber);
            }

            return true;
        }

        return false;
    }

    public void StageSwitching()
    {
        isMove = false;
        AudioPlay.Instance.StopMusic();
        MoveAnimation(Direction.NONE);

        SetMap();
        TurnManager.Instance.MapSwitching(floorTile);
        floorTile.MiniMapUpdate();
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