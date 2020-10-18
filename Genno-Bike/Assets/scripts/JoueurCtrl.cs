using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class JoueurCtrl : MonoBehaviour
{
    // Bouger le joueur en 2d
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;

    public Animator animator;
    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    Collider2D mainCollider;
    // Verifie tous les colliders et ignore Raycast
    LayerMask layerMask = ~(1 << 2 | 1 << 8);
    Transform t;

    private LvlContrl lvlContrl;

    private bool meurt;

    // Initialise le joueur, évite qu'il tombe dans le vide
    void Start()
    {
        meurt=false;
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        gameObject.layer = 8;

        lvlContrl= GameObject.FindGameObjectWithTag("canvasPauseLvl1").GetComponent<LvlContrl>();

        if(mainCamera)
            cameraPos = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Controle de mouvement, gauche ou droite
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (isGrounded || r2d.velocity.x > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }   
        }

        // Change la direction du joueur
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Le saut
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Camera qui suit le joueur
        if(mainCamera)
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);

        //Set la vitesse pour l'animation
        animator.SetFloat("Vitesse", Mathf.Abs(moveDirection));
        animator.SetBool("Meurt",meurt);

        // si il repond mal a ue question, le joueur explose aussi.
        if(lvlContrl.partieEstPerdue){
            meurt=true;
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        //Delimite la position du joueur au sol
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, 0.1f, 0);
        // Verifie si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, 0.23f, layerMask);

        // Applique la velocité du mouvement
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, 0.23f, 0), isGrounded ? Color.green : Color.red);
    }

   
    void OnCollisionEnter2D(Collision2D collision)
    {
        //si le joueur touche un obstacle, c'est gameOver
        if(collision.gameObject.CompareTag("obstacle"))
        {
            meurt=true;
            StartCoroutine(lvlContrl.GameOverCoroutine());
        }

        //si le joueur touche la ligne d'arrivee, il gagne
        if(collision.gameObject.CompareTag("ligneArrivee"))
         {
            lvlContrl.Gagner();
         }
    }
}



//reference tutoriel: https://sharpcoderblog.com/blog/2d-platformer-character-controller 