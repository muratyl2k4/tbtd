using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject soldier;
    public GameObject spawnpoint;
    float spawnrate;
    public Vector3 origin = Vector3.zero;
    Vector3 vector;
    public float radius = 10;
    void Start()
    {
        // Finds a position in a sphere with a radius of 10 units.
        
        spawnrate = 8;
        Instantiate(soldier, spawnpoint.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        spawnrate -= Time.deltaTime;
        if (spawnrate <= 0) {
            Instantiate(soldier, spawnpoint.transform.position, Quaternion.identity) ;
            spawnrate= 8;
        }
    }
}
