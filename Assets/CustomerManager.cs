using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour {

    public Transform[] bar_spot_transforms;


    //Array of all the bar spots. Each spot corresponding to an area.
    private bool[] bar_spot_bools;

    private bool bar_spot_open;
    private int num_bar_spots;
    private int num_bar_spots_taken;

	// Use this for initialization
	void Start () {
        bar_spot_open = true;
        num_bar_spots = bar_spot_transforms.Length;

        int num_spot = 0;
        foreach (bool spot in bar_spot_bools)
        {
            bar_spot_bools[num_spot] = false;
        }
        num_bar_spots_taken = 0;
	}
	
    /// <summary>
    /// This function returns a Vector3 position of an open bar spot if it
    /// exists. Otherwise it will return an empty vector 3.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBarSpot(ref int arg_bar_spot_pos)
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
                    num_bar_spots_taken++;
                    break;
                }
                position_in_array += 1;
            }
            //Return the transform of the open bar spot
            if(num_bar_spots_taken == num_bar_spots)
            {
                bar_spot_open = false;
            }
            return bar_spot_transforms[position_in_array].position;  
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }
}
