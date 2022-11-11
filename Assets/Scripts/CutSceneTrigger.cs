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
        if(director == null && keyDirector != "")
        director = GameObject.Find(keyDirector)?.GetComponent<SceneDirector>();

        if (keyDirector != "" && director == null)
        {
            Debug.LogWarning($"No SceneDirector with name: {keyDirector}");

            director = GameObject.FindObjectOfType<SceneDirector>();

            switch (director)
            {
                case null:
                    Debug.LogWarning($"No SceneDirector in hierarchy, Trigger({gameObject.name}) not set");
                    break;
                default:
                    Debug.LogWarning($"trigger({gameObject.name}) attached to SceneDirector({director.gameObject.name})");
                    break;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        director.PlayOnTrigger();
    }
}
