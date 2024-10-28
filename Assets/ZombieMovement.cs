using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float baseSpeed = 1f;
    public float sprintSpeed = 5f;
    public bool sprinting;

    public Image StaminaBar;

    public float Stamina, MaxStamina;

    public float AttackCost;
    public float SprintCost;
    public float ChargeRate;

    private Coroutine recharge;

    public Rigidbody2D rb;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Sprinting");
            moveSpeed = sprintSpeed;
            sprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Not Sprinting");
            moveSpeed = baseSpeed;
            sprinting = false;
        }

        if (sprinting && (movement.x != 0 || movement.y != 0))
        {

            Stamina -= SprintCost * Time.deltaTime;
            if (Stamina < 0)
            {
                Stamina = 0;
                moveSpeed = baseSpeed;
            }
            StaminaBar.fillAmount = Stamina / MaxStamina;


            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());

        }

        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("Attack!");

            Stamina -= AttackCost;

            if (Stamina < 0) Stamina = 0;
            StaminaBar.fillAmount = Stamina / MaxStamina;

            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }

    }
    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while (Stamina < MaxStamina)
        {
            Stamina += ChargeRate / 10f;
            if (Stamina > MaxStamina) Stamina = MaxStamina;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
}
