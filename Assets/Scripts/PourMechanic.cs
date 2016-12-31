using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PourMechanic : MonoBehaviour {

    public Transform Liquid;
    [SerializeField] private float currentAmount;
    [SerializeField] public float speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(currentAmount < 100)
        {
            currentAmount += speed * Time.deltaTime;
        }
        else
        {
            currentAmount = 100;
        }
        Liquid.GetComponent<Image>().fillAmount = currentAmount / 100;
	}
}
