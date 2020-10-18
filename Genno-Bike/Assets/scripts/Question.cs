using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public Question(string txt, string reponse){
        QuestionTxt=txt;
        Reponse=reponse;
    }

    public string QuestionTxt{get; set;}
    public string Reponse{get; set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //verifie si la reponse est bonne
    public bool checkReponse(string reponse){
        if(reponse.ToLower().Trim().Equals(this.Reponse.ToLower())) return true;
        return false;
    }
}
