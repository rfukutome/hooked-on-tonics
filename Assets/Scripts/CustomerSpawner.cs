using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {

    public GameObject customer_prefab;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 0.01f, 4.0f);
    }
	
	// Update is called once per frame
    void Spawn()
    {
        GameObject temp = Instantiate(customer_prefab, this.transform.position, this.transform.rotation);
    }
}
