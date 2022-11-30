using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private new Light2D light;
    GameObject player;
    private float baseIntensity;
    private float maxIntensity;
    public bool hiding;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        light = GetComponent<Light2D>();
        maxIntensity = light.intensity;
        baseIntensity = .7f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        light.intensity = hiding ? baseIntensity : maxIntensity;
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        light.transform.position = new Vector3(player.transform.position.x,
        player.transform.position.y,
        transform.position.z);
    }
    
}
