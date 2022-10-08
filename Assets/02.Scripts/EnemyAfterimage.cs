using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAfterimage : MonoBehaviour
{
    private bool isMove = false;
    private Vector3 desPos;

    private void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.Lerp(transform.position, desPos, 0.05f);

            if (Vector3.Distance(desPos, transform.position) <= 0.01f)
            {
                isMove = false;
                gameObject.SetActive(false);
                transform.position = transform.parent.position;
                transform.parent.parent.GetChild(0).GetComponent<FairyQueen>().AfterimageArriveDestination();
            }
        }
    }

    public void SetDestination(Vector3 pos)
    {
        isMove = true;
        desPos = pos;
    }
}
