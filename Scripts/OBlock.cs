using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBlock : Block
{

    new static Coord[,] spawnCoordinates;
    new static Vector2 spawnPosition;

    // Use this for initialization
    public override void Spawn()
    {
        blockStructure = BlockData.OBlockStructures;
        blockStructureSize = 2;

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
        spawnCoordinates = new Coord[2, 2];

        //The block's pivot brick (middle brick) will centre on spawnpostion
        Coord _pivotSquareCoordinate = new Coord(_spawnPosition);

        //calculate bottom left (bL) coOrdinate of block
        int _bLx = _pivotSquareCoordinate.x - 1, _bLy = _pivotSquareCoordinate.y;

        //itterate through block and create CoOrdinate based on its relative postion to bL      
        for (int _x = 0; _x < 2; _x++)
        {
            for (int _y = 0; _y < 2; _y++)
            {
                spawnCoordinates[_x, _y] = new Coord(_bLx + _x, _bLy + _y);
            }
        }

        spawnPosition = _spawnPosition;
    }


    override public bool Rotate()
    {
        return true;
    }
}
