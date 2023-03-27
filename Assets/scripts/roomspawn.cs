using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomspawn : MonoBehaviour
{
    private int a;
    private room_mng room_mng;
    public IEnumerable<GameObject> intercect_t;

    public GameObject window;

    private bool spawn = true;
    private bool spawning = true;

    // Start is called before the first frame update
    void Start(){
        room_mng = GameObject.Find("room_mng").GetComponent<room_mng>();
        intercect_t = room_mng.allPrefabs;

        if(spawning){
            Invoke("Spawn",1f);
        }
        else{
            Destroy(GetComponent<roomspawn>());
        }
    }

    void Spawn(){  
        if(room_mng.rooms > 0){
            if(transform.parent.Find("HOLD_CHECK").position - transform.position== new Vector3(0f,9f,9f)){ 
                intercect_t = intercect_t.AsQueryable().Intersect(room_mng.BE);
            }
            else if(transform.parent.Find("HOLD_CHECK").position - transform.position== new Vector3(9f,9f,0f)){ 
                intercect_t = intercect_t.AsQueryable().Intersect(room_mng.LE);
            }
            else if(transform.parent.Find("HOLD_CHECK").position - transform.position== new Vector3(0f,9f,-9f)){ 
                intercect_t = intercect_t.AsQueryable().Intersect(room_mng.TE);
            }
            else if(transform.parent.Find("HOLD_CHECK").position - transform.position== new Vector3(-9f,9f,0f)){ 
                intercect_t = intercect_t.AsQueryable().Intersect(room_mng.RE);
            }

            Collider[] colliders;
            //RIGHT
            if((colliders = Physics.OverlapSphere(transform.localPosition + new Vector3(9f,9f,0f), 2f )).Length > 0 && colliders[0].transform.parent != transform.parent){
                if(colliders[0].GetComponent<room_dscr>().left_entr){
                    intercect_t = intercect_t.AsQueryable().Intersect(room_mng.RE);
                }
                else{
                    intercect_t = intercect_t.AsQueryable().Intersect(room_mng.NRE);
                }
            }
            //BOTTOM
            if((colliders = Physics.OverlapSphere(transform.localPosition + new Vector3(0f,9f,-9f), 2f )).Length > 0 && colliders[0].transform.parent != transform.parent){
                if(colliders[0].GetComponent<room_dscr>().top_entr){
                    intercect_t = intercect_t.AsQueryable().Intersect(room_mng.BE);
                }
                else{
                    intercect_t = intercect_t.AsQueryable().Intersect(room_mng.NBE);
                }
            }
            //LEFT
            if((colliders = Physics.OverlapSphere(transform.localPosition + new Vector3(-9f,9f,0f), 2f )).Length > 0 && colliders[0].transform.parent != transform.parent){
                if(colliders[0].GetComponent<room_dscr>().right_entr){
                    intercect_t = intercect_t.AsQueryable().Intersect(room_mng.LE);
                }
                else{
                    intercect_t = intercect_t.AsQueryable().Intersect(room_mng.NLE);
                }
            }
            //TOP
            if((colliders = Physics.OverlapSphere(transform.localPosition + new Vector3(0f,9f,9f), 2f )).Length > 0 && colliders[0].transform.parent != transform.parent){
                if(colliders[0].GetComponent<room_dscr>().bottom_entr){
                    intercect_t = intercect_t.AsQueryable().Intersect(room_mng.TE);
                }
                else{
                    intercect_t = intercect_t.AsQueryable().Intersect(room_mng.NTE);
                }
            }
            if((colliders = Physics.OverlapSphere(transform.position + new Vector3(0f,9f,0f), 1f )).Length > 0){
                spawn = false;
            }

            a = UnityEngine.Random.Range(0, intercect_t.Count());
            
            if(spawn){
                Instantiate(intercect_t.ElementAt(a), transform.position, transform.rotation);
                room_mng.rooms--;
            }
        }
        else{
            Windows();
        }
    }

    void Windows(){
        Debug.Log("here");
        GameObject hold_check = transform.parent.Find("HOLD_CHECK").gameObject;

        Collider[] colliders;
        if((colliders = Physics.OverlapSphere(transform.localPosition + new Vector3(0f,9f,9f), 2f )).Length == 0f && hold_check.GetComponent<room_dscr>().top_entr){
            Instantiate(window, transform.parent.localPosition + new Vector3(0f, 0.22f, -4.3f), new Quaternion(0f, 0f, 0f, 0f));
        }
        if((colliders = Physics.OverlapSphere(transform.localPosition + new Vector3(9f,9f,0f), 2f )).Length == 0f && hold_check.GetComponent<room_dscr>().left_entr){
            Instantiate(window, transform.parent.localPosition + new Vector3(4.3f, 0.22f, 0f), new Quaternion(0f, 90f, 0f, 0f));
        }
        if((colliders = Physics.OverlapSphere(transform.localPosition + new Vector3(-9f,9f,0f), 2f )).Length == 0f && hold_check.GetComponent<room_dscr>().right_entr){
            Instantiate(window, transform.parent.localPosition + new Vector3(-4.3f, 0.22f, 0f), new Quaternion(0f, -90f, 0f, 0f));
        }
        if((colliders = Physics.OverlapSphere(transform.localPosition + new Vector3(0f,9f,-9f), 2f )).Length == 0f && hold_check.GetComponent<room_dscr>().bottom_entr){
            Instantiate(window, transform.parent.localPosition + new Vector3(0f, 0.22f, 4.3f), new Quaternion(0f, 180f, 0f, 0f));
        }
    }
}
