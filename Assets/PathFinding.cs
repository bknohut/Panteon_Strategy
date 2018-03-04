using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    public Transform player;
    public Vector2 targetIndex;
    public bool hasTarget;

    GameObject[,] tilemap;

    private void Start()
    {
        tilemap = TileManager.instance.tilemap;
        hasTarget = false;
        player = null;
    }
    private void Update()
    {
        if (player != null && hasTarget == true)
        {
            List<Ground> path = FindPath(player.GetComponent<Tank>().index, targetIndex);
            if( path != null )
            {
                player.gameObject.GetComponent<Unit>().MoveOnNewPath(path);
            }
            

            player = null;
            hasTarget = false;
        }
    }
    private List<Ground> FindPath( Vector2 startIndex, Vector2 targetIndex)
    {
        Ground source = GetTileFromIndex(startIndex);
        Ground destination = GetTileFromIndex(targetIndex);

        PriorityQ<Ground> openSet = new PriorityQ<Ground>(tilemap.GetLength(0) * tilemap.GetLength(1)) ;
        HashSet<Ground> closedSet = new HashSet<Ground>();

        openSet.Add(source);

        while(openSet.getSize() > 0 )
        {
            Ground current = openSet.ExtractFirstItem();
            closedSet.Add(current);
            
            if( current == destination )
            {
                return GetPath(source, destination);
            }
            foreach(Ground neighbourTile in GetNeighbourTiles( current ))
            {
                if (neighbourTile != destination )
                {
                    if (neighbourTile.isOccupied == true || closedSet.Contains(neighbourTile))
                    {
                        continue;
                    }
                }
                else
                {
                    if( neighbourTile.hasUnit == false && neighbourTile.isOccupied == true || closedSet.Contains(neighbourTile))
                    {
                        continue;
                    }
                }
                int movementCost = current.gCost + GetDistance(current, neighbourTile);
                if( movementCost < neighbourTile.gCost || !openSet.IsItemInQ(neighbourTile))
                {
                    Ground neighbour = neighbourTile;
                    neighbour.gCost = movementCost;
                    neighbour.hCost = GetDistance( neighbourTile, destination);
                    neighbour.hCost = GetDistance(neighbourTile, destination);
                    neighbour.parentTile = current;

                    if( !openSet.IsItemInQ(neighbourTile))
                    {
                        openSet.Add(neighbourTile);
                    }
                    else
                    {
                        openSet.Update(neighbourTile);
                    }
                }
            }
        }
        return null;
    }
    private Ground GetTileFromIndex(Vector2 index)
    {
        return tilemap[(int)index.x, (int)index.y].GetComponent<Ground>();
    }
    private List<Ground> GetNeighbourTiles(Ground currentTile)
    {

        List<Ground> neighbours = new List<Ground>();

        for( int i = -1; i <= 1; i++)
        {
            for( int j = -1; j <= 1; j++)
            {   
                //self
                if( i == 0 && j == 0)
                {
                    continue;
                }
                int xIndex = i + (int)currentTile.index.x;
                int yIndex = j + (int)currentTile.index.y;

                if(xIndex >= 0 && xIndex < tilemap.GetLength(0) && yIndex >= 0 && yIndex < tilemap.GetLength(1) )
                {
                    neighbours.Add(tilemap[xIndex, yIndex].GetComponent<Ground>());
                }
            }
        }
        return neighbours;
    }
    private int GetDistance(Ground currentTile, Ground nextTile)
    {   
        int xDistance = Mathf.Abs((int)currentTile.index.x - (int)nextTile.index.x);
        int yDistance = Mathf.Abs((int)currentTile.index.y - (int)nextTile.index.y);

        if( xDistance > yDistance )
        {
            return 14 * yDistance + 10 * (xDistance - yDistance);
        }
        return 14 * xDistance + 10 * ( yDistance - xDistance);
    }
    private List<Ground> GetPath(Ground sourceTile, Ground destinationTile)
    {
        List<Ground> path = new List<Ground>();
        Ground current = destinationTile;

        while( current != sourceTile )
        {
            path.Add(current);
            current = current.parentTile;
        }
        path.Reverse();
        return path;
    }
    private bool CheckDestinationCollusion(Ground destinationTile)
    {
        if( destinationTile.isOccupied == true && destinationTile.hasUnit == true)
        {
            return true;
        }
        return false;
    }
}
