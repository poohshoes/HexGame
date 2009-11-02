using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    class PathFinding
    {
        // function A*(start,goal)
        public static Stack<IntVector2> AStar(IntVector2 start, IntVector2 goal, World world)
        {
            Node[,] nodeArray = new Node[world.MapSize.X, world.MapSize.Y];

            for (int x = 0; x < world.MapSize.X; x++) 
            {
                for (int y = 0; y < world.MapSize.Y; y++)
                {
                    nodeArray[x, y] = new Node();
                    nodeArray[x, y].Location = new IntVector2(x, y);
                }
            }
            //    closedset := the empty set                 % The set of nodes already evaluated.  
            List<Node> closedset = new List<Node>();
            //    openset := set containing the initial node % The set of tentative nodes to be evaluated.
            List<Node> openset = new List<Node>();
            openset.Add(nodeArray[start.X, start.Y]);
            //    g_score[start] := 0                        % Distance from start along optimal path.
            nodeArray[start.X, start.Y].GScore = 0;
            //    h_score[start] := heuristic_estimate_of_distance(start, goal)
            nodeArray[start.X, start.Y].HScore = huristicDistance(start, goal, world);
            //    f_score[start] := h_score[start]           % Estimated total distance from start to goal through y.
            //nodeArray[start.X, start.Y].FScore = nodeArray[start.X, start.Y].HScore;

            //    while openset is not empty
            while (openset.Count > 0)
            {
                //        x := the node in openset having the lowest f_score[] value
                Node x = openset.Where(n => n.FScore == openset.Min(m => m.FScore)).First();
                //        if x = goal
                if(x.Location == goal)
                {
                //            return reconstruct_path(came_from,goal)
                    return reconstructPath(goal, start, nodeArray);
                }
                //        remove x from openset
                openset.Remove(x);
                //        add x to closedset
                closedset.Add(x);
                //        foreach y in neighbor_nodes(x)
                foreach(IntVector2 y in world.neighborHexes(x.Location))
                {
                    Node yNode = nodeArray[y.X, y.Y];
                    //            if y in closedset
                    //                continue
                    if(closedset.Where(n => n.Location == y).Count() > 0)
                        continue;
                    //            tentative_g_score := g_score[x] + dist_between(x,y)
                    float tentativeGScore = x.GScore + world.getMoveCost(x.Location, y);
                    bool tentativeIsBetter = false;

                    //            if y not in openset
                    //                add y to openset

                    //                tentative_is_better := true
                    //            elseif tentative_g_score < g_score[y]
                    //                tentative_is_better := true
                    //            else
                    //                tentative_is_better := false
                    if(openset.Where(n => n.Location == y).Count() == 0)
                    {
                        openset.Add(yNode);
                        tentativeIsBetter = true;
                    }
                    else if(tentativeGScore < yNode.GScore)
                        tentativeIsBetter = true;

                    //            if tentative_is_better = true
                    //                came_from[y] := x
                    //                g_score[y] := tentative_g_score
                    //                h_score[y] := heuristic_estimate_of_distance(y, goal)
                    //                f_score[y] := g_score[y] + h_score[y]
                    if(tentativeIsBetter)
                    {
                        yNode.cameFrom = x.Location;
                        yNode.GScore = tentativeGScore;
                        yNode.HScore = huristicDistance(y, goal, world);
                    }
                }
            }
            //    return failure
            return null;
        }

        /// <summary>
        /// I tested this huristic and it works perfectly if every move costs 1 point.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="goal"></param>
        /// <param name="world"></param>
        /// <returns></returns>
        static int huristicDistance(IntVector2 current, IntVector2 goal, World world)
        {
            double changeX = Math.Abs(current.X - goal.X);
            double changeY = Math.Abs(current.Y - goal.Y);

            double unroundedAnswer = changeX + Math.Max((changeY - changeX / 2), 0);

            if(world.hexIsSunken(current.X))
                return (int)Math.Floor(unroundedAnswer);
            else
                return (int)Math.Ceiling(unroundedAnswer);
        }

         //function reconstruct_path(came_from,current_node)
        static Stack<IntVector2> reconstructPath(IntVector2 currentNode, IntVector2 start, Node[,] nodeArray)
        {
            //    if came_from[current_node] is set
            //        p = reconstruct_path(came_from,came_from[current_node])
            //        return (p + current_node)
            //    else
            //        return the empty path

            Stack<IntVector2> path = new Stack<IntVector2>();
            IntVector2 current = currentNode;

            while (current != start) 
            {
                path.Push(current);
                current = nodeArray[current.X, current.Y].cameFrom;
            }

            return path;
        }
    }

    /// <summary>
    /// For pathfinding purposes.
    /// </summary>
    class Node 
    {
        public IntVector2 Location;
        public IntVector2 cameFrom;
        public float GScore;
        public float HScore;
        public float FScore
        {
            get 
            {
                return GScore + HScore;
            }
        }
    }
}
