using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    public Vector2 index;
    public List<GameObject> path;
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
        path = new List<GameObject>();
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
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
    }
    protected void OnMouseExit()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void LevelUp( int increasedLevel )
    {
        level+= increasedLevel;
        IEnumerator coroutine = UpdateStatus(increasedLevel);
        StartCoroutine(coroutine);
    }
    //Movement handlers
    public void MoveOnNewPath(List<GameObject> newPath)
    {   
        if( newPath != path || newPath != null)
        {
            path = newPath;
            StopCoroutine("MoveOnPath");
            StartCoroutine("MoveOnPath");
        }
        
    }
    IEnumerator MoveOnPath( )
    {
        int currentIndex = 0;
        Vector2 currentPosition = path[currentIndex].transform.position;
        // free the first tile
        TileManager.instance.tilemap[(int)index.x, (int)index.y].GetComponent<Ground>().isOccupied = false;
        TileManager.instance.tilemap[(int)index.x, (int)index.y].GetComponent<Ground>().hasUnit = false;
        transform.SetParent(null);
        while ( true )
        {
            if( (Vector2) transform.position == currentPosition)
            {   
                path[currentIndex].GetComponent<Ground>().isOccupied = false;
                path[currentIndex].GetComponent<Ground>().hasUnit = false;
                transform.SetParent(null);

                currentIndex++;
                if (currentIndex >= path.Count)
                {
                    if(path[currentIndex - 1].transform.childCount > 0)
                    {
                        path[currentIndex - 1].transform.GetChild(0).GetComponent<Unit>().LevelUp(level);
                        Destroy(gameObject);
                    }
                    path[currentIndex-1].GetComponent<Ground>().isOccupied = true;
                    path[currentIndex-1].GetComponent<Ground>().hasUnit = true;
                    transform.SetParent(path[currentIndex - 1].transform);
                    yield break;
                }

                currentPosition = path[currentIndex].transform.position;

                path[currentIndex].GetComponent<Ground>().isOccupied = true;
                path[currentIndex].GetComponent<Ground>().hasUnit = true;
                transform.SetParent(path[currentIndex].transform);

                index = path[currentIndex].GetComponent<Ground>().index;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentPosition, speed);
            yield return null;
        }
    }
    IEnumerator UpdateStatus( int level )
    {
        status.text = unitName + " leveled up by " + level;
        yield return new WaitForSeconds(1.0f);
        status.text = "";
    }
}
