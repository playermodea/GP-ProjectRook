using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public GameObject Gold_Prefab;
    public GameObject Heart_Prefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Gold_Prefab, new Vector3(), Quaternion.identity);
        Instantiate(Heart_Prefab, new Vector3(), Quaternion.identity);

    }

}
