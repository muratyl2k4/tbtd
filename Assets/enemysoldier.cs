using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class enemysoldier : MonoBehaviour

{
    enum SoldierAction
    {
        Move,
        Attack,
        Die
    }
    GameObject target;
    Vector3 direction;
    int speed;
    public int health;
    bool isDead;
    SoldierAction soldierAction;
    float attackSpeed;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        soldierAction = SoldierAction.Move;
        health = 100;
        speed = 5;
        target = GameObject.FindGameObjectWithTag("PlayerBase");
        attackSpeed = 1;

    }

    // Update is called once per frame
    void Update()
    {
        switch (soldierAction)
        {

            case SoldierAction.Move:
                Move();
                break;
            case SoldierAction.Attack:
                if (target != null)
                {
                    Attack(target);
                }
                else
                {

                    target = GameObject.FindGameObjectWithTag("PlayerBase");
                    soldierAction = SoldierAction.Move;
                }
                break;
            case SoldierAction.Die:
                Die();
                break;
            default:
                break;
        }

    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void Attack(GameObject target)
    {
        if (target.tag == "PlayerSoldier")
        {
            attackEnemy(target);


        }
        else if (target.tag == "PlayerBase")
        {
            attackBase(target);
        }
    }
    private void attackBase(GameObject target)
    {
        Debug.LogWarning("Base-------");
        PBStat pbstat = target.GetComponent<PBStat>();
        if (attackSpeed <= 0)
        {
            pbstat.GetDamage(20);
            attackSpeed = 1;
        }
        else
        {
            attackSpeed -= Time.deltaTime;
        }

    }
    private void attackEnemy(GameObject enemy)
    {
        Debug.LogWarning("Enemy-------");
        
        
        playersoldier enemystat = enemy.GetComponent<playersoldier>();

        if (!enemystat.checkisDead())
        {
            if (attackSpeed <= 0)
            {
                enemystat.getDamage(20);
                attackSpeed = 1;
            }
            else
            {
                attackSpeed -= Time.deltaTime;
            }
        }

        
        



    }


    private void Move()
    {
        direction = target.transform.position - this.transform.position;
        direction = direction / direction.magnitude;
        direction.y = 0;
        this.transform.Translate(direction * speed * Time.deltaTime);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBase")
        {
            soldierAction = SoldierAction.Attack;
        }
        else if (other.tag == "PlayerSoldier")
        {
            soldierAction = SoldierAction.Attack;
            target = other.gameObject;
        }
        else
        {
            Debug.LogWarning("yuru ariyorum");
            //target = GameObject.FindGameObjectWithTag("PlayerBase");
            //soldierAction = SoldierAction.Move;
        }
    }
    public void getDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            soldierAction = SoldierAction.Die;
        }
    }
    public bool checkisDead()
    {
        return isDead;
    }
}
