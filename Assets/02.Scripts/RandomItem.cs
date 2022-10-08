using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public TextMesh text;
    public bool isSell;
    public bool isHealingHeart;
    public GameObject[] Item;
    public GameObject HealingHeart;

    private void Start()
    {
        CreateItem();
    }

    void CreateItem()
    {
        GameObject item;

        if (!isHealingHeart)
        {
            int itemIndex = Random.Range(0, Item.Length);
            item = Instantiate(Item[itemIndex], transform.position, Quaternion.identity);
        }
        else
        {
            item = Instantiate(HealingHeart, transform.position, Quaternion.identity);
        }

        item.GetComponent<Item>().sell = isSell;
        item.transform.parent = transform.parent;

        if (isSell)
        {
            text.gameObject.SetActive(true);
            text.gameObject.transform.parent = item.transform;
            text.text = item.GetComponent<Item>().price.ToString();
        }

        Destroy(gameObject);
    }
}
