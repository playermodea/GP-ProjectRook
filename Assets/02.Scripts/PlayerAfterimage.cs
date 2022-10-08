using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterimage : MonoBehaviour
{
    private LineRenderer line;
    public GameObject target;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
    }

    private void OnEnable()
    {
        Vector3 myVec = new Vector3(transform.position.x, transform.position.y, -0.1f);
        Vector3 targetVec = new Vector3(target.transform.position.x, target.transform.position.y, -0.1f);
        line.SetPosition(0, myVec);
        line.SetPosition(1, targetVec);
    }
}
