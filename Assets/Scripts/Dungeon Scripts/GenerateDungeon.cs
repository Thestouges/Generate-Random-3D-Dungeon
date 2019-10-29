﻿using System.Collections;
using System.Collections.Generic;
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
        
        for (int i = 0; i < DungeonSize; i++)
        {
            for (int j = 0; j < DungeonSize; j++)
            {
                for (int k = 0; k < DungeonSize; k++)
                {
                    if (DungeonMap[i][j][k] == true)
                    {
                        createRoom(i*2*RoomSize,j*2* RoomSize, k*2 * RoomSize);
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

    void createRoom(float posx, float posy, float posz)
    {
        GameObject room = Instantiate(DungeonRoomsType[8]);
        room.transform.position = new Vector3(posx, posy, posz);
        room.transform.localScale = new Vector3(RoomSize, RoomSize, RoomSize);
    }
}
