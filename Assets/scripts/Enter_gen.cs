using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_gen : MonoBehaviour
{
    public GameObject[] enterList;
    public bool ACT = false;
    // Start is called before the first frame update
    void Start()
    {
        ACT = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ACT){
            ACT = false;
            Instantiate(enterList[Random.Range(0, enterList.Length)], transform.position, transform.rotation);
        }
    }
}
