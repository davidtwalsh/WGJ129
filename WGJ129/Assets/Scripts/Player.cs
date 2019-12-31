using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    Vector2 input;

    public GameObject pistol;
    [HideInInspector]
    public List<Item> itemsCollidingWith;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        itemsCollidingWith = new List<Item>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        input = new Vector2(horizontal, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.E) && itemsCollidingWith.Count > 0)
        {
            PickupItem(itemsCollidingWith[0]);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
    }

    void PickupItem(Item item)
    {
        if (item.itemName == "Pistol")
        {
            pistol.SetActive(true);
        }
        itemsCollidingWith.Remove(item);
        Destroy(item.gameObject);
    }
}
