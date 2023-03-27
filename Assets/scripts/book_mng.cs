//script responsible for book usage and spells

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book_mng : MonoBehaviour
{
    //prefabs
    public GameObject fireball;
    public GameObject glass;

    //gameobjects
    private GameObject freeOb; 
    public Transform spell_spawn;

    // materials
    public Material red;
    public Material selected;

    private int spell_num = 0; // current spell number
    
    public GameObject[] spells = {}; // list of spell gameobject (in future pages)

    // Start is called before the first frame update
    void Start()
    {
        spells[spell_num].GetComponent<Renderer>().material = selected; // set selected(first) spell material to selected
    }

    // Update is called once per frame
    void Update()
    {
        // choose next spell
        if(Input.GetButtonDown("E")){ // if Q button is being presssed (button to choose next spell)
            spells[spell_num].GetComponent<Renderer>().material = red; // set pervious spell material to deselected
            if(spell_num == 2){ // if previou spell is last in list, select first spell
                spell_num = 0;
            }
            else{ // else select next spell in list
                spell_num++;
            }
            spells[spell_num].GetComponent<Renderer>().material = selected; // set selected spell material to selected
        }

        // choose previous spell
        if(Input.GetButtonDown("Q")){ // if E button is being presssed (button to choose previous spell)
            spells[spell_num].GetComponent<Renderer>().material = red; // set pervious spell material to deselected
            if(spell_num == 0){ // if previou spell is first in list, select last spell
                spell_num = 2;
            }
            else{ // else select previous spell in list
                spell_num--;
            }
            spells[spell_num].GetComponent<Renderer>().material = selected; // set selected spell material to selected
        }

        if(Input.GetButtonDown("R")){ // if R button is being pressed (button to READ spell)
            if(spell_num == 0){
                freeOb = Instantiate(fireball, spell_spawn.position, spell_spawn.rotation);
            }
            if(spell_num == 1){
                freeOb = Instantiate(glass, spell_spawn.position, spell_spawn.rotation);
            }
        }
        freeOb = null;
    }
}
