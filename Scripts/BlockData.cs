class BlockData
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Block Structures and Rotation Variation Data
    // array indexes represent rotation state 
    // [0] = default, [1] = 90 degrees, [2] = 180 degrees, [3] = 180 degrees;
    // note: as array is initialised top to bottom, coordinates are mirrored horizontally i.e 0,0 is top left rather than bottom left
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static bool[,,] IBlockStructures = new bool[,,] {

        /*  . . . .
            # # # #
            . . . .
            . . . . */
            
        //Default Rotation
        {   {false, false, false, false},
            {false, false, false, false},
            {true, true, true, true},
            {false, false, false, false}},

        //90 deg 
        {   {false, false, true, false},
            {false, false, true, false},
            {false, false, true, false},
            {false, false, true, false}},

        //180 deg
        {   {false, false, false, false},
            {true, true, true, true},
            {false, false, false, false},
            {false, false, false, false}},

        //270 deg
        {   {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false}}        
    };

    public static bool[,,] JBlockStructures = new bool[,,] {
            
        /*  # . .
            # # #
            . . .  */
       
        //Default Rotation
        {   {false, false, false},
            {true, true, true},
            {true, false, false}},

        //90 deg 
        {   {false, true, false},
            {false, true, false},
            {false, true, true}},

        //180 deg 
        {   {false, false, true},
            {true, true, true},
            {false, false, false}},

        //270 deg
        {   {true, true, false},
            {false, true, false},
            {false, true, false}}
    };

    public static bool[,,] LBlockStructures = new bool[,,] {    
       
        /*  . . #
            # # #
            . . .  */      
             
        //Default Rotation
        {   {false, false, false},
            {true, true, true},
            {false, false, true}},

        //90 deg 
        {   {false, true, true},
            {false, true, false},
            {false, true, false}},

        //180 deg 
        {   {true, false, false},
            {true, true, true},
            {false, false, false}},

        //270 deg
        {   {false, true, false},
            {false, true, false},
            {true, true, false}}
    };

    public static bool[,,] OBlockStructures = new bool[,,] {    
      
        /*  # # 
            # #  */    
            
        {   {true, true},
            {true, true}}
    };

    public static bool[,,] SBlockStructures = new bool[,,] {

        /*  . # #
            # # .
            . . . */

       
        //Default Rotation
        {   {false, false, false},
            {true, true, false},
            {false, true, true}},

        //90 deg 
        {   {false, false, true},
            {false, true, true},
            {false, true, false}},

        //180 deg 
        {   {true, true, false},
            {false, true, true},
            {false, false, false}},

        //270 deg
        {   {false, true, false},
            {true, true, false},
            {true, false, false}}
    };

    public static bool[,,] ZBlockStructures = new bool[,,] {
       
        /*  # # .
            . # #
            . . .   */
       
        //Default Rotation
        {   {false, false, false},
            {false, true, true},
            {true, true, false}},

        //90 deg 
        {   {false, true, false},
            {false, true, true},
            {false, false, true}},

        //180 deg 
        {   {false, true, true},
            {true, true, false},
            {false, false, false}},

        //270 deg
        {   {true, false, false},
            {true, true, false},
            {false, true, false}}
    };

    public static bool[,,] TBlockStructures = new bool[,,] {    
       
        /*  . # .
            # # #
            . . .   */

        //Default Rotation
        {   {false, false, false},
            {true, true, true},
            {false, true, false}},

        //90 deg 
        {   {false, true, false},
            {false, true, true},
            {false, true, false}},

        //180 deg 
        {   {false, true, false},
            {true, true, true},
            {false, false, false}},
        //270 deg

        {   {false, true, false},
            {true, true, false},
            {false, true, false}}
    };

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Wall Kick Test Data
    // This is the order in which we test each of the coordinates with a kick e.g. (-1, 2) would indicate a kick of 1 cell left and 2 cells up
    // If all 5 tests fail, we can not rotate.
    // the different array indexes represent rotation state we want to go to e.g. [0] = default, [1] = 90 degrees
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public static int[,,] WallKickData = new int[,,]
    {
         { { 0, 0 }, { -1, 0 }, { -1, 1 }, { 0, -2 }, { -1, -2 } },
         { { 0, 0 }, { 1, 0 }, { 1, -1 }, { 0, 2 }, { 1, 2 } },
         { { 0, 0 }, { 1, 0 }, { 1, 1 }, { 0, -2 }, { 1, -2 } },
         { { 0, 0 }, { -1, 0 }, { -1, -1 }, { 0, 2 }, { -1, 2 } }
    };

    /// <summary>
    /// I block requires a diffrent rule set
    /// </summary>
    public static int[,,] IBlocklWallKickData = new int[,,]
    {
        { { 0, 0 }, { -1, 0 }, { -1, 1 }, { 0, -2 }, { -1, -2 } },
        { { 0, 0 }, { 1, 0 }, { 1, -1 }, { 0, 2 }, { 1, 2 } },
        { { 0, 0 }, { 1, 0 }, { 1, 1 }, { 0, -2 }, { 1, -2 } },
        { { 0, 0 }, { -1, 0 }, { -1, -1 }, { 0, 2 }, { -1, 2 } }
    };
}

