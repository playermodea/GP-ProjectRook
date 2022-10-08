using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Material attackTileMaterial;
    public Material enemyAttackTileMaterial;
    public Material moveTileMaterial;

    private static MaterialManager instance;

    public static MaterialManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<MaterialManager>();

                if (instance == null)
                {
                    Debug.Log("No Singleton Object MaterialManager");
                }
            }

            return instance;
        }
    }
}
