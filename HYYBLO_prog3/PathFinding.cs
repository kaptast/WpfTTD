using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace HYYBLO_prog3
{
    public class Pathfinding
    {
        public MapItem seeker, target;
        Map map;

        public Pathfinding(Map m)
        {
            map = m;
        }

        public List<MapItem> FindPath(MapItem startPos, MapItem targetPos)
        {
            MapItem startNode = startPos;
            MapItem targetNode = targetPos;

            bool pathSuccess = false;

            List<MapItem> openSet = new List<MapItem>();
            HashSet<MapItem> closedSet = new HashSet<MapItem>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                MapItem node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                    {
                        if (openSet[i].hCost < node.hCost)
                            node = openSet[i];
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (MapItem neighbour in map.GetNeighbours(node))
                {
                    if (!(neighbour is Road) || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
            if (pathSuccess)
            {
                return RetracePath(startNode, targetNode);
            }
            return null;
        }

        List<MapItem> RetracePath(MapItem startNode, MapItem endNode)
        {
            List<MapItem> path = new List<MapItem>();
            MapItem currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;

        }

        int GetDistance(MapItem nodeA, MapItem nodeB)
        {
            int dstX = Math.Abs((int)(nodeA.X - nodeB.X));
            int dstY = Math.Abs((int)(nodeA.Y - nodeB.Y));

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}