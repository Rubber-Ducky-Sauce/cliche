using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
    [SerializeField] private string keyName;
    // Start is called before the first frame update
    public override  void Collect()
    {
        GameManager.Instance.SetKey(keyName);
        base.Collect();
    }
}
