using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [field: SerializeField] public int cellRow { get; set; }
    [field: SerializeField] public int cellColumn { get; set; }

    private FloorTile floorTile;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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

    public void Action()
    {
        if (floorTile.EnemyCount() == 0)
        {
            floorTile.SetDoorOpenTile(gameObject);
            animator.SetTrigger("isOpen");
        }
        else
        {
            floorTile.SetDoorCloseTile(gameObject);
            animator.SetTrigger("isLocked");
        }
    }

    public void Open()
    {
        animator.SetTrigger("Open");
    }

    public void Close()
    {
        animator.SetTrigger("Locked");
    }
}
