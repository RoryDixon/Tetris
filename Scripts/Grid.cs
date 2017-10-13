using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    const int maxWidth = 21, maxHeight = 28;

    //Calculate halfway point on x axis and round up to nearest int     
    public static int gridMidPoint = Mathf.CeilToInt((float)(maxWidth * 0.5f));

    //we overwrite this when creating grid
    static Vector2 blockSpawnPoint = new Vector2(0, 0);

    static Transform[,] grid = new Transform[maxWidth, maxHeight];

    static Transform gridTransform;

    // Use this for initialization
    void Awake()
    {
        gridTransform = transform;
    }

    public static void CreateGrid(int _width, int _height, GameObject _wallBlock)
    {

        int _originX, _originY;

        //To give grids some consistency, place grids at the same height (3) until size exceeds screen height
        if (_height <= 26)
            _originY = 3;
        else
        {
            _originY = maxHeight - _height;
        }

        blockSpawnPoint.x = gridMidPoint * 10f;
        blockSpawnPoint.y = (_originY + _height) * 10f;

        Debug.Log(blockSpawnPoint);

        Block.CreateSpawnCoordinates(blockSpawnPoint);
        IBlock.CreateSpawnCoordinates(blockSpawnPoint);
        OBlock.CreateSpawnCoordinates(blockSpawnPoint);

        _originX = gridMidPoint - Mathf.CeilToInt((float)(_width * 0.5));

        for (int x = 0; x < maxWidth; x++)
        {
            for (int y = 0; y < maxHeight; y++)
            {
                if (x < _originX || x > (_originX + (_width - 1)) || y < _originY || y > (_originY + (_height - 1)))
                {
                    GameObject wall = Instantiate<GameObject>(_wallBlock, gridTransform, false);
                    wall.transform.localPosition = new Vector2(x * 10f, y * 10f);
                    wall.name = x.ToString() + " " + y.ToString();
                    grid[x, y] = wall.transform;
                }
                else
                    grid[x, y] = null;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Create one block border around Grid
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        float _xPos = -20f, _ypos = -10f;

        //Bottom Border
        for (int x = 0; x <= maxWidth + 1; x++)
        {
            _xPos += 10f;
            Instantiate<GameObject>(_wallBlock, gridTransform, false).transform.localPosition = new Vector2(_xPos, _ypos);
        }

        //RightBorder
        for (int y = 0; y <= maxHeight; y++)
        {
            _ypos += 10f;
            Instantiate<GameObject>(_wallBlock, gridTransform, false).transform.localPosition = new Vector2(_xPos, _ypos);
        }

        //TopBorder
        for (int x = 0; x <= maxWidth; x++)
        {
            _xPos -= 10f;
            Instantiate<GameObject>(_wallBlock, gridTransform, false).transform.localPosition = new Vector2(_xPos, _ypos);
        }

        //LeftBorder
        for (int y = 0; y <= maxHeight - 1; y++)
        {
            _ypos -= 10f;
            Instantiate<GameObject>(_wallBlock, gridTransform, false).transform.localPosition = new Vector2(_xPos, _ypos);
        }

    }

    public static void AddBricksToGrid(Transform block)
    {
        List<Transform> bricks = new List<Transform>();
        foreach (Transform child in block)
            bricks.Add(child);

        for (int b = 0; b<4; b++)
        {
            bricks[b].SetParent(gridTransform,true);
            Coord _brickGridCoord = new Coord(bricks[b].localPosition);
            grid[_brickGridCoord.x, _brickGridCoord.y] = bricks[b];            
        }
    }

    public static bool CanPlace(Coord _gridPlace)
    {
        if (_gridPlace.x < 0 || _gridPlace.x > maxWidth || _gridPlace.y < 0 || _gridPlace.y > maxHeight)
            return false;
        else if (grid[_gridPlace.x, _gridPlace.y] == null)
            return true;
        else
        {
            grid[_gridPlace.x, _gridPlace.y].name = "error";
            return false;
        }
    }

    public static bool CheckForGameOver(Coord _gridPlace)
    {
        if (grid[_gridPlace.x, _gridPlace.y] == null)
            return false;
        else if (grid[_gridPlace.x, _gridPlace.y].tag == "Brick")
            return true;
        else
            return false;
    }
}

/// <summary>
/// Structure for representing grid Coordinates
/// </summary>
public struct Coord
{
    public int x, y;

    public static Coord down = new Coord(0, -1);
    public static Coord left = new Coord(-1, 0);
    public static Coord right = new Coord(1, 0);
    public static Coord spawn = new Coord(0, -2);

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public Coord(Vector2 _position)
    {
        x = Mathf.FloorToInt(_position.x * 0.1f);
        y = Mathf.FloorToInt(_position.y * 0.1f);
    }

    public static Coord operator +(Coord _co1, Coord _co2)
    {
        return new Coord(_co1.x + _co2.x, _co1.y + _co2.y);
    }       
}


