using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Windows;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    public LayerMask solidObjectsLayer;

    private Rigidbody2D body;
    private Vector2 axisMovement;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        axisMovement.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        axisMovement.y = UnityEngine.Input.GetAxisRaw("Vertical");

        var targetPos = transform.position;

        if (IsWalkable(targetPos))
            StartCoroutine(Move());

    }

    private void FixedUpdate()
    {
        Move();

        bool movingYpos = axisMovement.y > 0;
        bool movingYneg = axisMovement.y < 0;
        bool movingXpos = axisMovement.x > 0;
        bool movingXneg = axisMovement.x < 0;
        bool Stopped = axisMovement.x == 0 && axisMovement.y == 0;

        if (movingXneg || movingXpos)
        {
            animator.SetFloat("Velocity", Math.Abs(body.velocity.x));
        }
        else if (Stopped)
        {
            animator.SetFloat("Velocity", Math.Abs(body.velocity.x));
        }

        if (movingYneg || movingYpos)
        {
            animator.SetFloat("Velocity", Math.Abs(body.velocity.y));
        }
        else if (Stopped)
        {
            animator.SetFloat("Velocity", Math.Abs(body.velocity.y));
        }
    }

    private IEnumerator Move()
    {
        body.velocity = axisMovement.normalized * speed;
        CheckForFlipping();
        yield return null;
    }

    private void CheckForFlipping()
    {
        bool movingLeft = axisMovement.x < 0;
        bool movingRight = axisMovement.x > 0;

        if (movingLeft)
        {
            transform.localScale = new Vector3(-4f, transform.localScale.y);

        }
        
        if (movingRight)
        {
            transform.localScale = new Vector3(4f, transform.localScale.y);

        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }    
}
