using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private new Light2D light;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        light.transform.position = new Vector3(player.transform.position.x,
    player.transform.position.y,
    transform.position.z);
    }
}
