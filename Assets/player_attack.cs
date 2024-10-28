using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attack : MonoBehaviour
{
    private GameObject Attack_area = default;

    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Attack_area = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        if(attacking)
        {
            timer += Time.deltaTime;
            
            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                Attack_area.SetActive(attacking);
            }
        }
        
    }

    private void Attack()
    {
        attacking = true;
        Attack_area.SetActive(attacking);
    }

}
