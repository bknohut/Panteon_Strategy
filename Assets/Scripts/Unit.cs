using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Unit base class
public class Unit : MonoBehaviour {

    public Vector2 index;
    public List<Ground> path;
    public int level;

    protected float speed = 0.04f;
    protected Sprite splash;
    protected Image information;
    protected Button spawn;
    protected Text nameArea;
    protected Text status;
    protected string unitName;

    protected virtual void Start()
    {
        path = new List<Ground>();
        level = 1;

        information = UIManager.instance.informationImage;
        nameArea = UIManager.instance.informationText;
        spawn = UIManager.instance.spawn;
        status = UIManager.instance.status;
        
    }

    // Mouse clicks
    protected void OnMouseOver()
    {   
        //leftclick 
        // select unit
        if( Input.GetMouseButtonDown(0))
        {
            information.gameObject.SetActive(true);
            information.sprite = splash;

            spawn.gameObject.SetActive(false);

            nameArea.text = "LEVEL " + level + " " +unitName;

            GameManager.instance.sourceUnit = transform;
        }
        // rightclick
        else if( Input.GetMouseButtonDown(1))
        {
            // move unit to unit
            GameManager.instance.destinationIndex = index;
            GameManager.instance.HandleMovement();
        }
    }
    protected void OnMouseEnter()
    {   
        // highlight unit when mouse is over
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
    }
    protected void OnMouseExit()
    {   
        // reset the highlight
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
    }
    // level up the unit
    public void LevelUp( int increasedLevel )
    {
        level+= increasedLevel;
        IEnumerator coroutine = UpdateStatus(increasedLevel);
        StartCoroutine(coroutine);
    }
    //Movement handlers
    public void MoveOnNewPath(List<Ground> newPath)
    {   
        // when there is a new path stop the previous to start the new
        if( newPath != path || newPath != null)
        {
            path = newPath;
            StopCoroutine("MoveOnPath");
            StartCoroutine("MoveOnPath");
        }
        
    }
    // move along the path
    IEnumerator MoveOnPath( )
    {
        int currentIndex = 0;
        Vector2 currentPosition = path[currentIndex].transform.position;
        // free the first tile
        TileManager.instance.tilemap[(int)index.x, (int)index.y].GetComponent<Ground>().isOccupied = false;
        TileManager.instance.tilemap[(int)index.x, (int)index.y].GetComponent<Ground>().hasUnit = false;
        transform.SetParent(null);
        // move
        while ( true )
        {   
            if( (Vector2) transform.position == currentPosition)
            {   
                // reset the flags after passing the tile
                path[currentIndex].isOccupied = false;
                path[currentIndex].hasUnit = false;
                transform.SetParent(null);

                currentIndex++;
                // path end
                if (currentIndex >= path.Count)
                {   
                    // if there is a unit collusion on the destination, level up
                    if(path[currentIndex - 1].transform.childCount > 0)
                    {
                        path[currentIndex - 1].transform.GetChild(0).GetComponent<Unit>().LevelUp(level);
                        Destroy(gameObject);
                    }
                    // set the flags of the destination tile
                    path[currentIndex-1].isOccupied = true;
                    path[currentIndex-1].hasUnit = true;
                    transform.SetParent(path[currentIndex - 1].transform);
                    yield break;
                }
                // set the tile flags when on the tile
                currentPosition = path[currentIndex].transform.position;

                path[currentIndex].isOccupied = true;
                path[currentIndex].hasUnit = true;
                transform.SetParent(path[currentIndex].transform);

                index = path[currentIndex].index;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentPosition, speed);
            yield return null;
        }
    }
    //status text
    IEnumerator UpdateStatus( int level )
    {
        status.text = unitName + " leveled up by " + level;
        yield return new WaitForSeconds(1.0f);
        status.text = "";
    }
}
