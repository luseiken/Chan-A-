using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector2 gridWorldSize = new Vector2(64, 64);
    public Vector2 gridWorldSizeShift;
    public bool DrawGizmos = false;
    public float nodeRadius = 0.25f;


    public Node[,] Grids;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        CreateGrid();
    }
    public bool walkable(Node n)
    {
        if (Grids[n.gridX, n.gridY].walkable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CreateGrid()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        Grids = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");

        bool walkable = true;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                Ray nowRay = new Ray(worldPoint, Vector3.up);
                foreach (var i in blocks)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(nowRay, out _hit) == false) continue;
                    if (_hit.transform.tag != "block") continue;
                    walkable = false;
                    break;

                }
                Grids[x, y] = new Node(walkable, worldPoint, x, y);
                walkable = true;
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(Grids[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - gridWorldSizeShift.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z - gridWorldSizeShift.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return Grids[x, y];
    }

    public List<Node> path;
    void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (Grids != null)
            {
                foreach (Node n in Grids)
                {
                    Gizmos.color = n.walkable ? Color.white : Color.red;



                    if (path != null) if (path.Contains(n)) Gizmos.color = Color.black;
                    if (path != null) if (n == path[0]) Gizmos.color = Color.yellow;
                    if (path != null) if (n == path[path.Count - 1]) Gizmos.color = Color.green;
                    Gizmos.DrawCube(n.worldPosition + Vector3.up * 0.05f, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }

}
