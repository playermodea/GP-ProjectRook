using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private bool isMove = false;
    private Vector3 desPos;
    private GameObject manager;

    private void Start()
    {
        manager = GameObject.Find("Manager");
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.Lerp(transform.position, desPos, 5.0f * Time.deltaTime);

            if (Vector3.Distance(desPos, transform.position) <= 0.05f)
            {
                transform.position = desPos;
                isMove = false;
                manager.GetComponent<TurnManager>().MapSwitchingComplete();
            }
        }
    }

    public void CameraMove(Vector3 mapPos)
    {
        isMove = true;
        desPos = mapPos;
    }
}
