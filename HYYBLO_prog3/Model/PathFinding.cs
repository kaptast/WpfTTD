//-----------------------------------------------------------------------
// <copyright file="PathFinding.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <author>HYYBLO</author>
//-----------------------------------------------------------------------
namespace Hyyblo_Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Pathfinding utility class
    /// </summary>
    public class Pathfinding
    {
        /// <summary>
        /// Reference to the map of the game
        /// </summary>
        private Map map;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pathfinding"/> class.
        /// </summary>
        /// <param name="m">Map of the game</param>
        public Pathfinding(Map m)
        {
            this.map = m;
        }

        /// <summary>
        /// Finds a path between the points on the map
        /// </summary>
        /// <param name="startPos">Starting position of the path</param>
        /// <param name="targetPos">Final target of the path</param>
        /// <returns>List of the path's items</returns>
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
                    if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
                    {
                        if (openSet[i].HCost < node.HCost)
                        {
                            node = openSet[i];
                        }
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (MapItem neighbour in this.map.GetNeighbours(node))
                {
                    if (!(neighbour is Road) || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = node.GCost + 1;
                    if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = 1;
                        neighbour.Parent = node;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }

            if (pathSuccess)
            {
                return this.RetracePath(startNode, targetNode);
            }

            return null;
        }

        /// <summary>
        /// Builds the path from the item's parents set in the findPath method
        /// </summary>
        /// <param name="startNode">Starting position of the path</param>
        /// <param name="endNode">Final target of the path</param>
        /// <returns>List of the path's items</returns>
        private List<MapItem> RetracePath(MapItem startNode, MapItem endNode)
        {
            List<MapItem> path = new List<MapItem>();
            MapItem currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();
            return path;
        }
    }
}