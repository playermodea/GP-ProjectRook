using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [field: SerializeField] public int cellRow { get; set; }
    [field: SerializeField] public int cellColumn { get; set; }
    [field: SerializeField] public float attackDamage { get; protected set; }
    [field: SerializeField] public bool isTurn { get; set; }
    [field: SerializeField] public bool isDestory { get; protected set; }
    [field: SerializeField] public TrapState state { get; protected set; }

    protected GameObject manager;
    [field: SerializeField] public FloorTile floorTile { get; private set; }

    protected virtual void Awake()
    {
        isTurn = false;
        isDestory = false;
        attackDamage = 1.0f;
        state = TrapState.STOP;
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

        manager = GameObject.Find("Manager");
    }
}
