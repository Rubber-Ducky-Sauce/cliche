using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(BoxCollider2D))]
public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] private string keyDirector;
    [SerializeField] private SceneDirector director;

    private void Start()
    {
        CheckisTrigger();
        if(director == null)
        SetDirector();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        director.PlayScene();
    }

    private void SetDirector()
    {
        if(keyDirector != "")
        CheckKeyDirector();
        if (director == null)
        AttachToFirstDirector();
        if(director != null)
        Debug.LogWarning($"trigger({gameObject.name}) attached to SceneDirector({director.gameObject.name})");
    }

    private void CheckisTrigger()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (!collider.isTrigger) collider.isTrigger = true;
    }

    private void CheckKeyDirector()
    {
        //attempt to set director with key
        director = GameObject.Find(keyDirector)?.GetComponent<SceneDirector>();
        //if key fails
        if (director == null)
            Debug.LogWarning($"No SceneDirector with name: {keyDirector}");
        //or it worked!
    }

    private void AttachToFirstDirector()
    {
        //attempt to attach to first director in hierarchy
        director = GameObject.FindObjectOfType<SceneDirector>();

        if (director == null)
            Debug.LogWarning($"No SceneDirector in hierarchy, Trigger({gameObject.name}) not set");
    }
}
