using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blades : MonoBehaviour
{
    [SerializeField] float vitesseRotation, vitesse;
    
    //pos1 est le debut du trajet pos 2 est la fin du trajet
    [SerializeField] Transform pos1, pos2;

    private bool retour;

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,vitesseRotation);
        if (transform.position.x >= pos1.position.x){
            retour=true;
        }

        if (transform.position.x <= pos2.position.x){
            retour=false;
        }

        if(retour)
        {
            transform.position=Vector2.MoveTowards(transform.position, pos2.position, vitesse*Time.deltaTime);
        }

        else{
            transform.position=Vector2.MoveTowards(transform.position, pos1.position, vitesse*Time.deltaTime);
        }

    }
}
//merci a https://opengameart.org/content/spikey-stuff pour le sprite :)