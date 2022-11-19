using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D),typeof(BoxCollider2D))]
public class Switch : MonoBehaviour
{
    private enum TriggerType { materialize, activate}
    [SerializeField] TriggerType triggerType;
    [SerializeField] GameObject hiddenObject;
    [SerializeField] GameObject triggeredObject;
    [SerializeField] float pressedSize = .73f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("you got me");
        if(triggerType == TriggerType.materialize)
        hiddenObject.SetActive(true);
        if(triggerType == TriggerType.activate)
        {
            Debug.Log("activate something");
        }
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x, pressedSize);
    }
}
