using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Pathfinding : MonoBehaviour
{
    private Grid grid;
    private static Pathfinding instance;

    public static Vector2[] RequestPathAStar(Vector2 from, Vector2 to)
    {
        return instance.FindPathAStar(from, to);
    }

    public static Vector2[] RequestPathPacMan(Vector2 from, Vector2 to, Vector2 lastNode)
    {
        return instance.FindPathPacMan(from, to, lastNode);
    }

    private void Awake()
    {
        grid = GetComponent<Grid>();
        instance = this;
    }

    private Vector2[] FindPathPacMan(Vector2 from, Vector2 to, Vector2 lastNode)
    {
        List<Vector2> waypoints = new List<Vector2>();

        Node startNode = grid.NodeFromWorldPoint(from);
        Node targetNode = grid.NodeFromWorldPoint(to);

        if (!startNode.walkable)
        {
            startNode = grid.ClosestWalkableNode(startNode);
        }

        List<Node> neighbours = GetNeighbours(startNode);

        foreach (Node neighbour in neighbours)
        {
            neighbour.hCost = (neighbour.worldPosition - targetNode.worldPosition).magnitude;
        }

        int chosen = -1;

        for (int i = 0; i < neighbours.Count; i++)
        {
            if (chosen < 0)
            {
                if (neighbours[i].walkable)
                {
                    chosen = i;
                }
            }
            else if (neighbours[i].hCost < neighbours[chosen].hCost && neighbours[i].walkable)
            {
                chosen = i;
            }
        }
         
        waypoints.Add(neighbours[chosen].worldPosition);

        neighbours[chosen].parent = startNode;

        return waypoints.ToArray();
    }

    private Vector2[] FindPathAStar(Vector2 from, Vector2 to)
    {
        bool pathSuccess = false;

        List<Vector2> waypoints = new List<Vector2>();

        Node startNode = grid.NodeFromWorldPoint(from);
        Node targetNode = grid.NodeFromWorldPoint(to);

        if (!startNode.walkable)
        {
            startNode = grid.ClosestWalkableNode(startNode);
        }
        if (!targetNode.walkable)
        {
            targetNode = grid.ClosestWalkableNode(targetNode);
        }

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            openSet = openSet.OrderBy(e => e.FCost).ToList();

            Node currentNode = openSet[0];

            openSet.RemoveAt(0);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                pathSuccess = true;
                break;
            }

            List<Node> neighbours = GetNeighbours(currentNode);

            foreach (Node item in neighbours)
            {
                if (item.walkable && !closedSet.Contains(item))
                {
                    float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, item);

                    if (newMovementCostToNeighbour < item.gCost || !openSet.Contains(item))
                    {
                        item.gCost = newMovementCostToNeighbour;
                        item.hCost = GetDistance(item, targetNode);

                        item.parent = currentNode;

                        if (!openSet.Contains(item))
                        {
                            openSet.Add(item);
                        }
                    }
                }
            }
        }

        // retrace
        if (pathSuccess)
        {
            Node backtrackNode = targetNode;

            while (backtrackNode != startNode)
            {
                waypoints.Add(backtrackNode.worldPosition);
                backtrackNode = backtrackNode.parent;
            }

            waypoints.Reverse();
        }

        return waypoints.ToArray();
    }

    private float GetDistance(Node to, Node from)
    {
        Vector2 delta = from.worldPosition - to.worldPosition;

        float y = Mathf.Min(delta.x, delta.y);
        float x = Mathf.Max(delta.x, delta.y);

        y = Mathf.Abs(y);
        x = Mathf.Abs(x);

        return (Mathf.Sqrt(2) * y) + x - y;
    }

    public List<Node> GetNeighbours(Node current)
    {
        List<Node> neighbours = new List<Node>();

        Vector2Int currentIndex = new Vector2Int(current.gridX, current.gridY);

        // up
        if (currentIndex.y + 1 < grid.GridSize.y)
        {
            neighbours.Add(grid.GetGrid[currentIndex.x, currentIndex.y + 1]);
        }

        // left
        if (currentIndex.x + 1 < grid.GridSize.x)
        {
            neighbours.Add(grid.GetGrid[currentIndex.x + 1, currentIndex.y]);
        }

        // down
        if (currentIndex.y - 1 >= 0)
        {
            neighbours.Add(grid.GetGrid[currentIndex.x, currentIndex.y - 1]);
        }

        // right
        if (currentIndex.x - 1 >= 0)
        {
            neighbours.Add(grid.GetGrid[currentIndex.x - 1, currentIndex.y]);
        }

        return neighbours;
    }
}