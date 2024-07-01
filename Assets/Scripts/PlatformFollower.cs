using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoint;
    private int currentIndex = 0;

    [SerializeField] private float speed = 4f;


    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoint[currentIndex].transform.position, transform.position) < .1f)
        {
            currentIndex++;

            if (currentIndex >= waypoint.Length) { 
                currentIndex = 0;
            }
            
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoint[currentIndex].transform.position, Time.deltaTime * speed);
    }


    
}
