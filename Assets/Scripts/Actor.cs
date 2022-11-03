using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    private float m_speed;
    public float Speed { get { return m_speed; } set { m_speed = value; } }

    public abstract void Move();
}
