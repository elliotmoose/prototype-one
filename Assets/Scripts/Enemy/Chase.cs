using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public Transform goal; //a variable of type Transform -- position, rotation and scale of object 

    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
}
