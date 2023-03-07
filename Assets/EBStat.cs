using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBStat : MonoBehaviour

{

    int health;
    // Start is called before the first frame update
    void Start()
    {
        health= 100;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (health <= 0)
        {
            Debug.LogWarning("You dead");
        }
    }

    public void GetDamage(int damage)
    {
        health -= damage;
    }
}
