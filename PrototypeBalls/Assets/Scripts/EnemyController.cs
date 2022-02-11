using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private GameObject player;
    private Rigidbody enemyRB;

    public int speed = 3;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            enemyRB.isKinematic = true;
        }
        else
        {
            // Gets the direction that the enemy needs to follow to get to the player position
            Vector3 playerPosition = (player.transform.position - transform.position).normalized;
            enemyRB.AddForce(playerPosition * speed);

            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
    }
}
