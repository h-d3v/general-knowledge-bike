using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour
{
    private string QuestionTxt{get; set;}
    private string Reponse{get; set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public bool checkReponse(string reponse){
        if(reponse.ToLower().Trim().Equals(this.Reponse.ToLower())) return true;
        return false;
    }
}
