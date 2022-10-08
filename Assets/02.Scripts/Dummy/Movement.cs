using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private FloorTileInfo finalTileInfo;

    [field: SerializeField] public int cellRow { get; set; }
    [field: SerializeField] public int cellColumn { get; set; }

    [field: SerializeField] public bool isMove { get; private set; }
    [field: SerializeField] public FloorTile floorTile { get; private set; }

    private void Start()
    {
        isMove = false;
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
            transform.position = Vector3.Lerp(transform.position, finalTileInfo.tilePos, 0.05f);

            if (Vector3.Distance(finalTileInfo.tilePos, transform.position) <= 0.01f)
            {
                transform.position = finalTileInfo.tilePos;
                isMove = false;
            }
        }
    }

    public void Move(Direction dir, int point)
    {
        FloorTileInfo destinationTile = floorTile.CheckDestinationTile(cellRow, cellColumn, dir, point);
        FloorTileInfo nextTileInfo = floorTile.CheckNextTile(cellRow, cellColumn, dir);

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

        isMove = true;
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
