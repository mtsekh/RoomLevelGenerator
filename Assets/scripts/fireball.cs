using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{ 
    private float elapsedTime = 0f;
    private float self_destruct = 10f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(GameObject.Find("Player1Cam").transform.forward * 100f);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > self_destruct){
            Destroy(gameObject);
        }
    }
}
