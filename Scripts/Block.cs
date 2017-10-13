using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public enum BlockType { I, J, L, O, S, T, Z };

    public BlockType blockType;
    protected static int blockStructureSize;
    protected static bool[,,] blockStructure;
    protected static int[,,] wallKickTests;
    protected static Coord[,] spawnCoordinates;
    protected static Vector2 spawnPosition;
    public static Coord[,] blockCoordinates;
    protected static int rotationState;
    public static Transform blockTransform;
    static Coord wallKick;

    // Use this for initialization
    public virtual void Spawn()
    {
        switch (blockType)
        {
            case BlockType.J:
                blockStructure = BlockData.JBlockStructures;
                break;
            case BlockType.L:
                blockStructure = BlockData.LBlockStructures;
                break;
            case BlockType.S:
                blockStructure = BlockData.SBlockStructures;
                break;
            case BlockType.T:
                blockStructure = BlockData.TBlockStructures;
                break;
            case BlockType.Z:
                blockStructure = BlockData.ZBlockStructures;
                break;
            default:
                blockStructure = BlockData.ZBlockStructures;
                break;
        }

        blockCoordinates = spawnCoordinates.Clone() as Coord[,];

        blockStructureSize = 3;
        rotationState = 0;

        wallKickTests = BlockData.WallKickData;

        blockTransform = transform;

        blockTransform.localPosition = spawnPosition;
    }

    /// <summary>
    /// Creates set of coordinates that all 3x3 blocks will spawn at
    /// </summary>
    /// <param name="_spawnPosition">Transform position of the Point</param>
    public static void CreateSpawnCoordinates(Vector2 _spawnPosition)
    {
        spawnCoordinates = new Coord[3, 3];

        //The block's pivot brick (middle brick) will centre on spawnpostion
        Coord _pivotSquareCoordinate = new Coord(_spawnPosition);

        //calculate bottom left (bL) coOrdinate of block
        int _bLx = _pivotSquareCoordinate.x - 1, _bLy = _pivotSquareCoordinate.y - 1;

        

        //itterate through block and create CoOrdinate based on its relative postion to bL      
        for (int _x = 0; _x < 3; _x++)
        {
            for (int _y = 0; _y < 3; _y++)
            {
                spawnCoordinates[_x, _y] = new Coord(_bLx + _x, _bLy + _y);
            }
        }

        //as 3x3 blocks contain an odd number of blocks, we need to offset slightly to align to grid
        spawnPosition = (_spawnPosition += new Vector2(5f, 5f));
    }


    public static bool CheckNewCoOrdSet(Coord _offset, int _rotationState)
    {
        for (int _x = 0; _x < blockStructureSize; _x++)
        {
            for (int _y = 0; _y < blockStructureSize; _y++)
            {
                if (blockStructure[_rotationState, _y, _x])
                {
                    //Debug.Log(blockCoordinates[_x, _y].x + " " + blockCoordinates[_x, _y].y);
                    Coord _coOrdToCheck = blockCoordinates[_x, _y];
                    _coOrdToCheck += _offset;
                    if (!Grid.CanPlace(_coOrdToCheck))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public static void ApplyNewCoOrdSet(Coord _offset)
    {
        for (int _x = 0; _x < blockStructureSize; _x++)
        {
            for (int _y = 0; _y < blockStructureSize; _y++)
            {
                Coord _newCoord = blockCoordinates[_x, _y];
                _newCoord += _offset;
                blockCoordinates[_x, _y] = _newCoord;                
            }
        }
    }

    public virtual bool CheckForGameOver()
    {
        for (int _x = 0; _x < blockStructureSize; _x++)
        {
            for (int _y = 0; _y < blockStructureSize; _y++)
            {
                if (blockStructure[0, _y, _x])
                {
                    Coord _coOrdToCheck = blockCoordinates[_x, _y];
                    _coOrdToCheck += Coord.spawn;
                    if (Grid.CheckForGameOver(_coOrdToCheck))
                        return true;
                }
            }
        }
        return false;
    }

    public static bool CheckIfCanRotate()
    {
        int _rotationStateToTest = (rotationState==3?0:rotationState+1);
        for(int x = 0; x < 5; x++)
        {
            Coord _wallKick = new Coord(wallKickTests[rotationState, x, 0], wallKickTests[rotationState, x, 1]);
            if (CheckNewCoOrdSet(_wallKick, _rotationStateToTest))
            {               
                wallKick = _wallKick;
                Debug.Log(wallKick.x + " " + wallKick.y);
                return true;
            }
        }
        return false;
    }

    public virtual void PutBlockOnGrid()
    {
        ApplyNewCoOrdSet(Coord.spawn);
        blockTransform.localPosition += new Vector3(0f, -20f);
    }

    public static bool Move(Coord _dir)
    {
        if (CheckNewCoOrdSet(_dir, rotationState))
        {
            ApplyNewCoOrdSet(_dir);
            blockTransform.localPosition += (new Vector3(_dir.x, _dir.y)*10);
            return true;
        }
        else
            return false;
    }

    public virtual bool Rotate() {
        
        if (CheckIfCanRotate()) {
            rotationState = (rotationState == 3 ? 0 : rotationState + 1);
            ApplyNewCoOrdSet(wallKick);
            if (!(wallKick.x == 0 && wallKick.y == 0)){
                blockTransform.localPosition += (new Vector3(wallKick.x, wallKick.y) * 10);
            }
            blockTransform.Rotate(0, 0, -90);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SettleBlock()
    {
        Grid.AddBricksToGrid(blockTransform);
        Destroy(blockTransform.gameObject);
    }
}
