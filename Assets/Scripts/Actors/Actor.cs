using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    private float m_speed;
    [SerializeField]protected float Speed { get { return m_speed; } set { m_speed = value; } }

    public abstract void Move();
}
