using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{

    public Color LineColor;
    public List<Transform> Nodes = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = LineColor;
        var nodes = GetComponentsInChildren<Transform>().ToList();

        nodes.RemoveAll(match => match == transform);

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNodePosition = nodes[i].position;
            if (i < nodes.Count - 1)
            {
                Vector3 nextNodePosition = nodes[i + 1].position;
                Gizmos.DrawLine(currentNodePosition, nextNodePosition);
            }
            Gizmos.DrawWireSphere(currentNodePosition, 0.3f);
        }
    }

}
