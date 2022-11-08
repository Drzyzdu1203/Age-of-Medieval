using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEditor : MonoBehaviour
{

    void Start()
    {
        EndEdit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndEdit()
    {
        GameObject varGameObject = GameObject.FindWithTag("Player");
        //varGameObject.GetComponent<PlayerManager>().enabled = true;
    }

}
