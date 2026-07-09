using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnlyDuringGame : MonoBehaviour
{
    private Renderer[] renderers;
    private Collider[] colliders;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();

        SetVisible(false);
    }

    void Update()
    {
        if (GameTimer.isGameEnded)
        {
            Destroy(this.gameObject);
            return;
        }

        if (GameTimer.isGameStarted)
        {
            SetVisible(true);
        }
        else
        {
            SetVisible(false);
        }
    }

    private void SetVisible(bool visible)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = visible;
        }

        foreach (Collider c in colliders)
        {
            c.enabled = visible;
        }
    }
}