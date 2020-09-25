using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    private Score score;
    public event Action On_forbidden_food;
    public event Action On_allowed_food;
    Food_Type food_I_want;
    public Vector2 movementDirection;
    Vector2 temp_movememnt = Vector2.zero;
    public float runSpeed = 5;

    [Space]
    [Header("Character attributes")]
    public float MOVEMENT_BASE_SPEED = 5f;

    [Space]
    [Header("References")]
    public Rigidbody2D _rb;
    public Animator animator;

    public event Action OnRightFood;
    // Start is called before the first frame update
    void Start()
    {
        
        score = GameObject.FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
        Animate();
    }
    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        runSpeed = Mathf.Clamp(movementDirection.magnitude, 0f, 1f);
        movementDirection.Normalize();

        
    }
    void Move()
    {
        _rb.velocity = movementDirection * runSpeed * MOVEMENT_BASE_SPEED;
    }
    void Animate()
    {

        //this if statement make face the pacman in the right direction
        if (movementDirection.x != 0.0f || movementDirection.y != 0.0f)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
            temp_movememnt = movementDirection;
        }
        else
        {
            //this else statemnet makes the pacman face the last movement direction while it's standing and still
            animator.SetFloat("Horizontal", temp_movememnt.x);
            animator.SetFloat("Vertical", temp_movememnt.y);
            //otherwise looks like everytime you stop it turns around and is not nice to see it.
        }
    }
    public void SetFoodIWant(Food_Type food_I_want)
    {
        this.food_I_want = food_I_want;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            if (collision.GetComponent<Food_Script>().food_type == food_I_want)
            {
                On_allowed_food?.Invoke();
                score.AdjustScore(20);
            }
            else if (collision.GetComponent<Food_Script>().food_type != food_I_want)
            {
                On_forbidden_food?.Invoke();
            }
        }
       
    }
    public Vector2 GetDirection()
    {
        return movementDirection;
    }
    public Vector2 GetTempDirection()
    {
        return temp_movememnt;
    }
}
