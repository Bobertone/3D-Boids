using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctreeNode
{
    Bounds nodeBounds;
    float minSize;

    public OctreeNode(Bounds b, float minNodeSize) 
    {
        nodeBounds = b;
        minSize = minNodeSize;
    }

    public void Draw() 
    {
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawWireCube(nodeBounds.center, nodeBounds.size);
    }
}
