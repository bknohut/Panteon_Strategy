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
            List<GameObject> path = FindPath(player.GetComponent<Tank>().index, targetIndex);
            if( path != null )
            {
                player.gameObject.GetComponent<Unit>().MoveOnNewPath(path);
            }
            

            player = null;
            hasTarget = false;
        }
    }
    private List<GameObject> FindPath( Vector2 startIndex, Vector2 targetIndex)
    {
        GameObject source = GetTileFromIndex(startIndex);
        GameObject destination = GetTileFromIndex(targetIndex);

        List<GameObject> openSet = new List<GameObject>();
        HashSet<GameObject> closedSet = new HashSet<GameObject>();

        openSet.Add(source);

        while(openSet.Count > 0 )
        {
            GameObject current = openSet[0];
            for( int i = 0; i < openSet.Count; i++)
            {
                if( openSet[i].GetComponent<Ground>().fCost < current.GetComponent<Ground>().fCost || openSet[i].GetComponent<Ground>().fCost == current.GetComponent<Ground>().fCost && openSet[i].GetComponent<Ground>().hCost < current.GetComponent<Ground>().hCost)
                {
                    current = openSet[i];
                }
            }
            openSet.Remove(current);
            closedSet.Add(current);
            
            if( current == destination )
            {
                return GetPath(source, destination);
            }
            foreach( GameObject neighbourTile in GetNeighbourTiles( current ))
            {
                if (neighbourTile != destination )
                {
                    if (neighbourTile.GetComponent<Ground>().isOccupied == true || closedSet.Contains(neighbourTile))
                    {
                        continue;
                    }
                }
                else
                {
                    if( neighbourTile.GetComponent<Ground>().hasUnit == false && neighbourTile.GetComponent<Ground>().isOccupied == true || closedSet.Contains(neighbourTile))
                    {
                        continue;
                    }
                }
                int movementCost = current.GetComponent<Ground>().gCost + GetDistance(current, neighbourTile);
                if( movementCost < neighbourTile.GetComponent<Ground>().gCost || !openSet.Contains(neighbourTile))
                {
                    Ground neighbour = neighbourTile.GetComponent<Ground>();
                    neighbour.gCost = movementCost;
                    neighbour.hCost = GetDistance( neighbourTile, destination);
                    neighbour.hCost = GetDistance(neighbourTile, destination);
                    neighbour.parentTile = current;

                    if( !openSet.Contains(neighbourTile))
                    {
                        openSet.Add(neighbourTile);
                    }
                }
            }
        }
        return null;
    }
    private GameObject GetTileFromIndex(Vector2 index)
    {
        return tilemap[(int)index.x, (int)index.y];
    }
    private List<GameObject> GetNeighbourTiles( GameObject currentTile)
    {

        List<GameObject> neighbours = new List<GameObject>();

        for( int i = -1; i <= 1; i++)
        {
            for( int j = -1; j <= 1; j++)
            {   
                //self
                if( i == 0 && j == 0)
                {
                    continue;
                }
                int xIndex = i + (int)currentTile.GetComponent<Ground>().index.x;
                int yIndex = j + (int)currentTile.GetComponent<Ground>().index.y;

                if(xIndex >= 0 && xIndex < tilemap.GetLength(0) && yIndex >= 0 && yIndex < tilemap.GetLength(1) )
                {
                    neighbours.Add(tilemap[xIndex, yIndex]);
                }
            }
        }
        return neighbours;
    }
    private int GetDistance( GameObject currentTile, GameObject nextTile)
    {   
        int xDistance = Mathf.Abs((int)currentTile.GetComponent<Ground>().index.x - (int)nextTile.GetComponent<Ground>().index.x);
        int yDistance = Mathf.Abs((int)currentTile.GetComponent<Ground>().index.y - (int)nextTile.GetComponent<Ground>().index.y);

        if( xDistance > yDistance )
        {
            return 14 * yDistance + 10 * (xDistance - yDistance);
        }
        return 14 * xDistance + 10 * ( yDistance - xDistance);
    }
    private List<GameObject> GetPath( GameObject sourceTile, GameObject destinationTile)
    {
        List<GameObject> path = new List<GameObject>();
        GameObject current = destinationTile;

        while( current != sourceTile )
        {
            path.Add(current);
            current = current.GetComponent<Ground>().parentTile;
        }
        path.Reverse();
        return path;
    }
    private bool CheckDestinationCollusion( GameObject destinationTile)
    {
        if( destinationTile.GetComponent<Ground>().isOccupied == true && destinationTile.GetComponent<Ground>().hasUnit == true)
        {
            return true;
        }
        return false;
    }
}
