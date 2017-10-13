using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject wallBlock;
    public int gridWidth, gridHeight;
    public Transform gridWallOriginPos, gameOriginPos;
    public Block[] blocks;
    static Vector2 previewBoxPosition = new Vector2(285f, 50f);
    static Block activeBlock, nextBlock;
    float gravity = 1f, lastFall = 0f, holdTime = 0f;
    const float buttonHoldDelay = .25f;
    bool holdingDown = false;

    //230*300

    // Use this for initialization
    void Start()
    {
        enabled = false;

        Grid.CreateGrid(gridWidth, gridHeight, wallBlock);

        nextBlock = GetNewBlock();

        NewBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            holdTime = 0;
            holdingDown = true;
            BlockFall();
        }
        else if (Input.GetKey(KeyCode.DownArrow)&&holdingDown)
        {
            holdTime += Time.deltaTime;

            if (holdTime > buttonHoldDelay)
            {
                BlockFall();
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            holdingDown = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            holdTime = 0;
            holdingDown = true;
            Block.Move(Coord.right);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && holdingDown)
        {
            holdTime += Time.deltaTime;

            if (holdTime > buttonHoldDelay)
            {
                if(Block.Move(Coord.right))
                {

                }
                else
                {
                    holdingDown = false;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            holdingDown = false;
            holdTime = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            holdTime = 0;
            holdingDown = true;
            Block.Move(Coord.left);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && holdingDown)
        {
            holdTime += Time.deltaTime;

            if (holdTime > buttonHoldDelay)
            {
                if (Block.Move(Coord.left))
                {

                }
                else
                {
                    holdingDown = false;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            holdingDown = false;
            holdTime = 0;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (activeBlock.Rotate())
                Debug.Log("canRotate!");
        }

        if((Time.fixedTime - lastFall >= gravity)&&!holdingDown)
        {
            BlockFall();
        }
    }

    Block GetNewBlock()
    {
        GameObject _newBlock = Instantiate<GameObject>(blocks[Random.Range(0, blocks.Length)].gameObject, gameOriginPos, false);
        _newBlock.transform.localPosition = previewBoxPosition;
        return _newBlock.GetComponent<Block>();
    }

    /// <summary>
    /// 
    /// </summary>
    void NewBlock()
    {
        SpawnBlock();

        if (activeBlock.CheckForGameOver())
            GameOver();
        else
        {
            activeBlock.PutBlockOnGrid();
            EnableControls();
        }        
    }

    void BlockFall()
    {
        lastFall = Time.time;
        if (!Block.Move(Coord.down))
        {
            DisableControls();
            Block.SettleBlock();
            NewBlock();
            holdingDown = false;
        }
    }

    void SpawnBlock()
    {
        activeBlock = nextBlock;
        nextBlock = GetNewBlock();
        activeBlock.Spawn();
        
    }

    void GameOver() {
        DisableControls();
    }

    void EnableControls()
    {
        enabled = true;
        lastFall = Time.fixedTime;
    }

    void DisableControls()
    {
        enabled = false;
    }
}
