using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    private Vector3 originPos = new Vector3(500.0f, 500.0f, 0.0f);
    private FloorTile floorTile;

    private void Update()
    {
        if (!floorTile.SelectTile(gameObject) || !UIManager.Instance.IsPointerOverUIObject())
        {
            transform.position = originPos;
        }
    }

    private void OnEnable()
    {
        floorTile = GetComponentInParent<Player>().movement.floorTile;
    }

    private void OnDisable()
    {
        transform.position = originPos;
    }
}
