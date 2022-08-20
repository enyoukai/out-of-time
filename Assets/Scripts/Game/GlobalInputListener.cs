using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInputListener : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard; 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            enableLeaderboard();
        }
    }

    void 

}
