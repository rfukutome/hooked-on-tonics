using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour {

    public Transform[] bar_spot_transforms;


    //Array of all the bar spots. Each spot corresponding to an area.
    private bool[] bar_spot_bools;
    private GameObject[] bar_spot_customers;
    private GameObject drink_order_panel;
    private bool bar_spot_open;
    private int num_bar_spots;
    private int num_bar_spots_taken;
    private int active_customer;
    private int money_earned;
    private Text money_text;
	// Use this for initialization
	void Start () {
        drink_order_panel = GameObject.FindGameObjectWithTag("Drink_Panel");
        drink_order_panel.SetActive(false);
        money_text = GameObject.FindGameObjectWithTag("Money_Display").GetComponent<Text>();
        money_earned = 0;
        bar_spot_open = true;
        num_bar_spots = bar_spot_transforms.Length;
        bar_spot_customers = new GameObject[num_bar_spots];
        bar_spot_bools = new bool[num_bar_spots];

        int num_spot = 0;
        foreach (bool spot in bar_spot_bools)
        {
            bar_spot_bools[num_spot] = true;
            num_spot++;
        }
        num_bar_spots_taken = 0;
        active_customer = -1;
	}
	
    /// <summary>
    /// This function returns a Vector3 position of an open bar spot if it
    /// exists. Otherwise it will return an empty vector 3.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBarSpot(ref int arg_bar_spot_pos, GameObject arg_bar_spot_customer)
    {
        if (bar_spot_open)
        {
            int position_in_array = 0;
            //Find an open bar spot;
            foreach (bool spot in bar_spot_bools)
            {
                if(spot == true)
                {
                    bar_spot_bools[position_in_array] = false;
                    bar_spot_customers[position_in_array] = arg_bar_spot_customer;
                    arg_bar_spot_pos = position_in_array;
                    num_bar_spots_taken++;
                    break;
                }
                position_in_array += 1;
            }
            
            
            if(num_bar_spots_taken == num_bar_spots)
            {
                bar_spot_open = false;
            }
            else if(num_bar_spots_taken > num_bar_spots)
            {
                Debug.Log("Customer's exceed number of seats (CUSTOMER MANAGER)");
            }
            
            //Return the transform of the open bar spot
            return bar_spot_transforms[position_in_array].position;  
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    public void SetActiveCustomer(int arg_active_customer)
    {
        active_customer = arg_active_customer;
    }

    public void FinishOrder()
    {
        num_bar_spots_taken--;
        if(num_bar_spots_taken < num_bar_spots)
        {
            bar_spot_open = true;
        }
        bar_spot_customers[active_customer].GetComponent<CustomerBehavior>().OrderFullfilled();
        bar_spot_customers[active_customer] = null;
        bar_spot_bools[active_customer] = true;
        active_customer = -1;

        //CALCULATE AMOUNT OF MONEY GAINED FROM ACCURACY + TIME
        money_earned += + 100;
        money_text.text = money_earned.ToString();
    }

    public GameObject GetDrinkPanel()
    {
        return drink_order_panel;
    }
}
