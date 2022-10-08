using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTile : MonoBehaviour
{
    public Tilemap tilemap;

    public void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnMouseOver()
    {
        try
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);
            int layerMask = 1 << LayerMask.NameToLayer("Floor");
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero, 1000.0f, layerMask);
            if (this.tilemap = hit.transform.GetComponent<Tilemap>())
            {
                this.tilemap.RefreshAllTiles();

                int x, y;
                x = this.tilemap.WorldToCell(ray.origin).x;
                y = this.tilemap.WorldToCell(ray.origin).y;

                Vector3Int v3Int = new Vector3Int(x, y, 0);

                this.tilemap.SetTileFlags(v3Int, TileFlags.None);
                this.tilemap.SetColor(v3Int, (Color.red));

                Vector3 vec = new Vector3((float)(x + 0.5) + hit.transform.position.x, (float)(y + 0.5) + hit.transform.position.y, 0);

                FloorTile temp = hit.transform.GetComponent<FloorTile>();
                //Debug.Log(tilemap.GetCellCenterWorld(tilemap.LocalToCell(new Vector3Int(x, y, 0))));
                //Debug.Log(temp.tileInfo[0, 0].tilePos);
                //Debug.Log(vec);
                for (int i = 0; i < 32; i++)
                {
                    bool locking = false;

                    for (int j = 0; j < 32; j++)
                    {
                        if (vec == temp.tileInfo[i, j].tilePos)
                        {
                            Debug.Log(temp.tileInfo[i, j].isPassable);
                            Debug.Log(temp.tileInfo[i, j].tileObj.name);
                            //Debug.Log("row : " + temp.tileInfo[i, j].row);
                            //Debug.Log("column : " + temp.tileInfo[i, j].column);
                            locking = true;
                            break;
                        }
                    }

                    if (locking)
                    {
                        break;
                    }
                }
            }
        }
        catch (NullReferenceException)
        {

        }
    }

    private void OnMouseExit()
    {
        this.tilemap.RefreshAllTiles();

    }




    //public GameObject goCube;



    //private void Update()
    //{
    //    try
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);

    //            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);



    //            if (this.tilemap = hit.transform.GetComponent<Tilemap>())
    //            {
    //                int x, y;
    //                x = this.tilemap.WorldToCell(ray.origin).x;
    //                y = this.tilemap.WorldToCell(ray.origin).y;



    //                Vector3Int v3Int = new Vector3Int(x, y, 0);

    //                StartCoroutine(this.Move(this.tilemap.CellToWorld(v3Int)));
    //            }
    //        }
    //    }
    //    catch (NullReferenceException)
    //    {

    //    }
    //}

    //IEnumerator Move(Vector3 target)
    //{
    //    this.goCube.transform.LookAt(target);
    //    float disOld = 100000;

    //    while (true)
    //    {
    //        this.goCube.transform.Translate(Vector3.forward * Time.deltaTime * 5);
    //        var distance = Vector3.Distance(goCube.transform.position, target);
    //        yield return null;

    //        if (disOld < distance)
    //            break;
    //        disOld = distance;

    //    }





    //    this.goCube.transform.rotation = new Quaternion(0, 0, 0, 0);
    //    this.goCube.transform.position = target;
    //}
}
