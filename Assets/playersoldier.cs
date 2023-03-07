using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class playersoldier : MonoBehaviour

{
    enum SoldierAction
    {
        Move ,
        Attack ,
        Die
    } 
    public GameObject target;
    Vector3 direction;
    int speed;
    int health;
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
        target = GameObject.FindGameObjectWithTag("EnemyBase");
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

                    target = GameObject.FindGameObjectWithTag("EnemyBase");
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
        if (target.tag == "EnemySoldier")
        {
            attackEnemy(target);
            
            
        }else if (target.tag == "EnemyBase")
        {
            attackBase(target);
        }
    }
    private void attackBase(GameObject target)
    {
        Debug.LogWarning("Base-------");
        EBStat ebstat = target.GetComponent<EBStat>();
        if (attackSpeed <= 0)
        {
            ebstat.GetDamage(20);
            attackSpeed = 1;
        }else
        {
            attackSpeed -= Time.deltaTime;
        }

    }    
    private void attackEnemy(GameObject enemy)
    {
        
        

        Debug.LogWarning("Enemy-------");
        
        
        enemysoldier enemystat = enemy.GetComponent<enemysoldier>();

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
        direction= target.transform.position- this.transform.position;
        direction = direction / direction.magnitude;
        direction.y = 0;
        this.transform.Translate(direction * speed * Time.deltaTime);
       
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBase")
        {
            soldierAction= SoldierAction.Attack;
        }else if (other.tag == "EnemySoldier")
        {
            soldierAction = SoldierAction.Attack;
            target = other.gameObject;
        }else
        {
            Debug.LogWarning("yuru ariyorum");
            //target = GameObject.FindGameObjectWithTag("EnemyBase");
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
