using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerBehavior : MonoBehaviour
{
    public Transform moveOutOfScreen;
    public GameObject drinkPanel;
    public float moveSpeed;
    public SpriteRenderer readySprite;

    private CustomerManager customer_manager;
    private bool order_fullfilled;
    private bool ready_to_order;
    private bool waiting_to_order;

    private int bar_spot_number;
    private Transform bar_spot_position;
    // Use this for initialization
    void Start()
    {
        customer_manager = GameObject.FindGameObjectWithTag("GM").GetComponent<CustomerManager>();
        waiting_to_order = true;
        ready_to_order = false;
        order_fullfilled = false;

        drinkPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //If there is barSpaceOpen, and you haven't ordered, and you are not close enough to order.
        if (waiting_to_order && !order_fullfilled && !ready_to_order)
        {
            Vector3 bar_spot = customer_manager.GetBarSpot(ref bar_spot_number);

            //Need to return barposition AND bar spot number
            transform.position = Vector2.MoveTowards(transform.position, barOrderPosition.position, moveSpeed * Time.deltaTime);
            if (transform.position == barOrderPosition.position)
            {
                ready_to_order = true;
                readySprite.enabled = true;
            }
        }
        else if (order_fullfilled)
        {
            //Move out of the screen
            transform.position = Vector2.MoveTowards(transform.position, moveOutOfScreen.position, moveSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void WaitForOrder()
    {
        //Logic to wait to be clicked on
    }

    public void OrderFullfilled()
    {
        order_fullfilled = true;
        readySprite.enabled = false;
        drinkPanel.SetActive(false);
    }

    void OnMouseDown()
    {
        if (ready_to_order)
        {
            drinkPanel.SetActive(true);
        }
    }
}
