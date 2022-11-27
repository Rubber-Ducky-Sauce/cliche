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
    [SerializeField] AudioClip cilp1;
    [SerializeField] AudioClip cilp2;
    PlayerController player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();
        if(triggerType == TriggerType.materialize)
        hiddenObject.SetActive(true);
        if(triggerType == TriggerType.activate)
        {
            Debug.Log("activate something");
        }
        GetComponent<PolygonCollider2D>().enabled = false;
        player.playerActive = false;
        StartCoroutine(CollapseSwitch());
    }

    IEnumerator CollapseSwitch()
    {
        GameManager.Instance.PlaySound(cilp1);
        yield return new WaitForSeconds(.01f);
        player.playerActive = true;
        GameManager.Instance.PlaySound(cilp2);
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x, pressedSize);
    }
}
