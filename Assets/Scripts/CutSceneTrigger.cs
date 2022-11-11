using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public  abstract class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] private string keyDirector;
    [SerializeField] protected SceneDirector director;

    private void Start()
    {
        SetDirector();
    }

    protected virtual void SetDirector()
    {
        if(director == null)
        {
            if (keyDirector != "")
                CheckKeyDirector();
            if (director != null)
                return;
            else
                AttachToFirstDirector();
            if (director != null)
                Debug.LogWarning($"trigger({gameObject.name}) attached to SceneDirector({director.gameObject.name})");
        }

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
