using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Player player;
    public bool hit;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        hit = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if (hit)
        {
            pos.y -= player.velocity.x * Time.fixedDeltaTime;
            transform.Rotate(Vector3.forward * 100.0f * Time.fixedDeltaTime);
        }
        if (pos.x < -100)
        {
            Destroy(gameObject);
        }
        transform.position = pos;
        
    }

    
}
