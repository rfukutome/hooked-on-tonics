using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerBehavior : MonoBehaviour
{
    
    
    public float moveSpeed;
    public SpriteRenderer ready_sprite;

    private Transform pos_out_of_screen;
    private CustomerManager customer_manager;
    private bool order_fullfilled;
    private bool ready_to_order;
    private bool waiting_to_order;
    private bool waiting_for_barspot;
    private GameObject drink_order_panel;
    [SerializeField]
    private int bar_spot_number;
    private Vector3 bar_spot_position;
    // Use this for initialization
    void Start()
    {
        customer_manager = GameObject.FindGameObjectWithTag("GM").GetComponent<CustomerManager>();
        drink_order_panel = customer_manager.GetDrinkPanel();
        ready_sprite = gameObject.GetComponentsInChildren<SpriteRenderer>()[1];

        pos_out_of_screen = GameObject.FindGameObjectWithTag("Out of Screen").transform;

        ready_sprite.enabled = false;
        waiting_for_barspot = true;
        waiting_to_order = false;
        ready_to_order = false;
        order_fullfilled = false;
        bar_spot_number = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //If you're waiting for barspace, and you haven't ordered, and you are not close enough to order, look for a barspace
        if (waiting_for_barspot && !order_fullfilled && !ready_to_order)
        {
            bar_spot_position = customer_manager.GetBarSpot(ref bar_spot_number, this.gameObject);

            //If a valid position was returned
            if (bar_spot_position != new Vector3(0, 0, 0))
            {
                waiting_for_barspot = false;
                waiting_to_order = true;
            }
        }
        else if (waiting_to_order)
        {
            transform.position = Vector2.MoveTowards(transform.position, bar_spot_position, moveSpeed * Time.deltaTime);
            if (transform.position == bar_spot_position)
            {
                ready_to_order = true;
                ready_sprite.enabled = true;
                waiting_to_order = false;
            }
        }
        else if (order_fullfilled)
        {
            //Move out of the screen
            transform.position = Vector2.MoveTowards(transform.position, pos_out_of_screen.position, moveSpeed * Time.deltaTime);
            if (transform.position == pos_out_of_screen.position)
            {
                Destroy(gameObject);
            }
        }

    }

    void WaitForOrder()
    {
    }

    public void OrderFullfilled()
    {
        order_fullfilled = true;
        ready_sprite.enabled = false;
        drink_order_panel.SetActive(false);
    }

    void OnMouseDown()
    {
        if (ready_to_order)
        {
            customer_manager.SetActiveCustomer(bar_spot_number);
            drink_order_panel.SetActive(true);
        }
    }
}
