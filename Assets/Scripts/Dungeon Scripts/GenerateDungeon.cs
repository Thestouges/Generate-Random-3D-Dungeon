using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    public GameObject playerObject;
    public List<GameObject> DungeonRoomsType;
    public int DungeonSize = 5;
    public float RoomSize = 1;
    public int TotalRooms = 5;

    List<List<List<bool>>> DungeonMap;

    int startx = 0;
    int starty = 0;
    int startz = 0;

    int roomCounter = 1;

    struct cubePlacement
    {
        public GameObject cube;
        public Vector3 rotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        DungeonMap = new List<List<List<bool>>>();
        createDungeonMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createDungeonMap()
    {
        for (int i = 0; i < DungeonSize; i++)
        {
            List<List<bool>> dungeonI = new List<List<bool>>();
            for (int j = 0; j < DungeonSize; j++)
            {
                List<bool> dungeonJ = new List<bool>();
                for (int k = 0; k < DungeonSize; k++)
                {
                    bool dungeonK = false;
                    dungeonJ.Add(dungeonK);
                }
                dungeonI.Add(dungeonJ);
            }
            DungeonMap.Add(dungeonI);
        }

        //random starting room
        startx = Random.Range(0, DungeonSize);
        starty = Random.Range(0, DungeonSize);
        startz = Random.Range(0, DungeonSize);

        DungeonMap[startx][starty][startz] = true;
        
        while (roomCounter < TotalRooms)
        {
            generateDungeon(startx, starty, startz);
        }
        

        /*
        DungeonMap[5][5][5] = true;
        DungeonMap[6][5][5] = true;
        DungeonMap[7][5][5] = true;
        DungeonMap[4][5][5] = true;
        DungeonMap[3][5][5] = true;
        DungeonMap[5][6][5] = true;
        DungeonMap[5][7][5] = true;
        DungeonMap[5][4][5] = true;
        DungeonMap[5][3][5] = true;
        DungeonMap[5][5][6] = true;
        DungeonMap[5][5][7] = true;
        DungeonMap[5][5][4] = true;
        DungeonMap[5][5][3] = true;

        //corner
        DungeonMap[7][7][7] = true;
        DungeonMap[3][7][7] = true;
        DungeonMap[7][3][7] = true;
        DungeonMap[7][7][3] = true;
        DungeonMap[3][3][7] = true;
        DungeonMap[3][7][3] = true;
        DungeonMap[7][3][3] = true;
        DungeonMap[3][3][3] = true;

        //
        DungeonMap[7][6][7] = true;
        DungeonMap[7][7][6] = true;
        DungeonMap[7][6][3] = true;
        DungeonMap[7][3][6] = true;
        DungeonMap[7][4][7] = true;
        DungeonMap[7][7][4] = true;
        DungeonMap[7][4][3] = true;
        DungeonMap[7][3][4] = true;

        DungeonMap[3][6][3] = true;
        DungeonMap[3][3][6] = true;
        DungeonMap[3][6][7] = true;
        DungeonMap[3][7][6] = true;
        DungeonMap[3][4][3] = true;
        DungeonMap[3][3][4] = true;
        DungeonMap[3][4][7] = true;
        DungeonMap[3][7][4] = true;


        DungeonMap[4][7][3] = true;
        DungeonMap[4][3][7] = true;
        DungeonMap[4][7][7] = true;
        DungeonMap[4][3][3] = true;
        DungeonMap[6][7][3] = true;
        DungeonMap[6][3][7] = true;
        DungeonMap[6][7][7] = true;
        DungeonMap[6][3][3] = true;
        */




        for (int i = 0; i < DungeonSize; i++)
        {
            for (int j = 0; j < DungeonSize; j++)
            {
                for (int k = 0; k < DungeonSize; k++)
                {
                    if (DungeonMap[i][j][k] == true)
                    {
                        createRoom(i*2*RoomSize,j*2* RoomSize, k*2 * RoomSize, i, j, k);
                    }
                }
            }
        }

        playerObject.transform.position = new Vector3(startx, starty, startz)*RoomSize*2;
    }

    void generateDungeon(int posx, int posy, int posz)
    {
        if (roomCounter >= TotalRooms)
            return;

        int num = Random.Range(0, 6);

        //foward room
        if (num == 0 && posx > 0)
        {
            setRoomTrue(posx, posy, posz);
            generateDungeon(posx - 1, posy, posz);
        }
        //right room
        else if (num == 1 && posy > 0)
        {
            setRoomTrue(posx, posy, posz);
            generateDungeon(posx, posy - 1, posz);
        }
        //up room
        else if (num == 2 && posz > 0)
        {
            setRoomTrue(posx, posy, posz);
            generateDungeon(posx, posy, posz-1);
        }
        //back room
        else if (num == 3 && posx < DungeonSize - 2)
        {
            setRoomTrue(posx, posy, posz);
            generateDungeon(posx + 1, posy, posz);
        }
        //left room
        else if (num == 4 && posy < DungeonSize - 2)
        {
            setRoomTrue(posx, posy, posz);
            generateDungeon(posx, posy + 1, posz);
        }
        //down room
        else if (num == 5 && posz < DungeonSize - 2)
        {
            setRoomTrue(posx, posy, posz);
            generateDungeon(posx, posy, posz + 1);
        }
    }

    void setRoomTrue(int posx, int posy, int posz)
    {
        if (DungeonMap[posx][posy][posz] == false)
        {
            DungeonMap[posx][posy][posz] = true;
            roomCounter++;
        }
    }

    void createRoom(float posx, float posy, float posz, int mapx, int mapy, int mapz)
    {
        cubePlacement cube = roomCube(mapx, mapy, mapz);

        GameObject room = Instantiate(DungeonRoomsType[8]);
        room.transform.position = new Vector3(posx, posy, posz);
        //room.transform.eulerAngles = cube.rotation;
        room.transform.localScale = new Vector3(RoomSize, RoomSize, RoomSize);
    }

    cubePlacement roomCube(int x, int y, int z)
    {
        int openings = 0;

        cubePlacement result = new cubePlacement();

        bool[] sides = Enumerable.Repeat(false, 6).ToArray(); ;

        try
        {
            if (DungeonMap[x + 1][y][z] == true) //front
            {
                sides[0] = true;
                openings++;
            }
        }
        catch
        {

        }

        try
        {
            if (DungeonMap[x - 1][y][z] == true) //back
            {
                sides[1] = true;
                openings++;
            }
        }
        catch
        {

        }

        try
        {
            if (DungeonMap[x][y + 1][z] == true) //left
            {
                sides[2] = true;
                openings++;
            }
        }
        catch
        {

        }

        try
        {
            if (DungeonMap[x][y - 1][z] == true) //right
            {
                sides[3] = true;
                openings++;
            }
        }
        catch
        {

        }

        try
        {
            if (DungeonMap[x][y][z + 1] == true) //up
            {
                sides[4] = true;
                openings++;
            }
        }
        catch
        {

        }

        try
        {
            if (DungeonMap[x][y][z - 1] == true) //down
            {
                sides[5] = true;
                openings++;
            }
        }
        catch
        {

        }


        //check
        switch (openings)
        {
            case 1:
                result.cube = DungeonRoomsType[0];
                break;
            case 2:
                if(sides[0] == sides[1] && sides[2] == sides[3] && sides[4] == sides[5])
                {
                    result.cube = DungeonRoomsType[2];
                }
                else
                {
                    result.cube = DungeonRoomsType[1];
                }
                break;
            case 3:
                if (sides[0] != sides[1] && sides[2] != sides[3] && sides[4] != sides[5])
                {
                    result.cube = DungeonRoomsType[3];
                }
                else
                {
                    result.cube = DungeonRoomsType[4];
                }
                break;
            case 4:
                if (sides[0] == sides[1] && sides[2] == sides[3] && sides[4] == sides[5])
                {
                    result.cube = DungeonRoomsType[5];
                }
                else
                {
                    result.cube = DungeonRoomsType[6];
                }
                break;
            case 5:
                result.cube = DungeonRoomsType[7];
                break;
            default:
                result.cube = DungeonRoomsType[8];
                break;
        }

        result.rotation = getCubeRotation(sides, openings);

        return result;
    }

    Vector3 getCubeRotation(bool[] sides, int openings)
    {
        Vector3 result = new Vector3(0, 0, 0);

        switch (openings)
        {
            case 1:
                if(sides[0] == true)
                {
                    result.z = -90;
                }
                if(sides[1] == true)
                {
                    result.z = 90;
                }
                if (sides[3] == true)
                {
                    result.x = 180;
                }
                if (sides[4] == true)
                {
                    result.x = 90;
                }
                if (sides[5] == true)
                {
                    result.x = -90;
                }
                break;
            case 2:
                if(sides[0] == true || sides[1] == true)
                {
                    result.z = 90;
                }
                else if (sides[2] == true || sides[3] == true)
                {
                    result.z = 0;
                    if(sides[5] == true)
                    {
                        result.y = 180;
                    }

                    if (sides[3] == true)
                    {
                        result.z = 180;
                        result.y += 180;
                    }
                }
                else if (sides[4] == true || sides[5] == true)
                {
                    result.x = 90;
                }
                break;
        }

        return result;
    }
}
