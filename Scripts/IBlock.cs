using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBlock : Block
{

    new static Coord[,] spawnCoordinates;
    new static Vector2 spawnPosition;

    // Use this for initialization
    public override void Spawn()
    {
        blockStructure = BlockData.IBlockStructures;
        wallKickTests = BlockData.IBlocklWallKickData;
        blockStructureSize = 4;

        blockCoordinates = spawnCoordinates.Clone() as Coord[,];
        rotationState = 0;

        blockTransform = transform;

        blockTransform.localPosition = spawnPosition;
    }  

    /// <summary>
    /// Creates set of coordinates that this 4x4 block will spawn at
    /// </summary>
    /// <param name="_spawnPosition">Transform position of the Point</param>
    new public static void CreateSpawnCoordinates(Vector2 _spawnPosition)
    {
        spawnCoordinates = new Coord[4, 4];

        //The block's pivot brick (middle brick) will centre on spawnpostion
        Coord _pivotSquareCoordinate = new Coord(_spawnPosition);

        //calculate bottom left (bL) coOrdinate of block
        int _bLx = _pivotSquareCoordinate.x - 2, _bLy = _pivotSquareCoordinate.y - 2;

        //itterate through block and create CoOrdinate based on its relative postion to bL      
        for (int _x = 0; _x < 4; _x++)
        {
            for (int _y = 0; _y < 4; _y++)
            {
                spawnCoordinates[_x, _y] = new Coord(_bLx + _x, _bLy + _y);
            }
        }

        spawnPosition = _spawnPosition;
    }

    public override bool CheckForGameOver()
    {
        for (int _x = 0; _x < blockStructureSize; _x++)
        {
            for (int _y = 0; _y < blockStructureSize; _y++)
            {
                if (blockStructure[0, _y, _x])
                {
                    Coord _coOrdToCheck = blockCoordinates[_x, _y];
                    _coOrdToCheck += Coord.down;
                    if (Grid.CheckForGameOver(_coOrdToCheck))
                        return true;
                }
            }
        }
        return false;
    }

    public override void PutBlockOnGrid()
    {
        ApplyNewCoOrdSet(Coord.down);
        blockTransform.localPosition += new Vector3(0f, -10f);
    }
}
