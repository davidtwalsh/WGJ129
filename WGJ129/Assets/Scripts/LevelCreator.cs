using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public bool autoUpdate;

    public Texture2D levelTexture;

    public List<TileTypes> tileTypes;

    public Grid grid;
    

    Dictionary<string, byte> hexToIndex;
    byte[,] level;

    private void Awake()
    {
        level = new byte[levelTexture.width, levelTexture.height];
        hexToIndex = new Dictionary<string, byte>();

        foreach (TileTypes t in tileTypes)
        {
            hexToIndex.Add(t.hex, t.index);
        }

        for (int x = 0; x < levelTexture.width; x++)
        {
            for (int y = 0; y < levelTexture.height; y++)
            {
                Color color = levelTexture.GetPixel(x, y);
                string hex = ColorUtility.ToHtmlStringRGB(color);
                byte index = hexToIndex[hex];

                level[x, y] = index;
            }
        }

        for (int x = 0; x < levelTexture.width; x++)
        {
            for (int y = 0; y < levelTexture.height; y++)
            {
                byte cell = level[x, y];
                GameObject obj;

                if (cell == 0) //isAir
                {
                    continue;
                }


                if (tileTypes[cell].isWall == true)
                {
                    if (tileTypes[level[x, y - 1]].isWalkable || tileTypes[level[x,y-1]].index == 5)
                    {
                        obj = Instantiate(tileTypes[cell].prefab[0], new Vector3(x, y), Quaternion.identity);
                        obj.transform.parent = this.transform;
                    }
                    else
                    {
                        obj = Instantiate(tileTypes[cell].prefab[1], new Vector3(x, y), Quaternion.identity);
                        obj.transform.parent = this.transform;
                    }
                }
                else
                {
                    obj = Instantiate(tileTypes[cell].prefab[0], new Vector3(x, y), Quaternion.identity);
                    obj.transform.parent = this.transform;
                }

            }
        }

        grid.CreateGrid(level, levelTexture.width, levelTexture.height, tileTypes);
    }

    public void GenerateMap()
    {
        level = new byte[levelTexture.width, levelTexture.height];
        hexToIndex = new Dictionary<string, byte>();

        foreach (TileTypes t in tileTypes)
        {
            hexToIndex.Add(t.hex, t.index);
        }

        for (int x = 0; x < levelTexture.width; x++)
        {
            for (int y = 0; y < levelTexture.height; y++)
            {
                Color color = levelTexture.GetPixel(x, y);
                string hex = ColorUtility.ToHtmlStringRGB(color);
                byte index = hexToIndex[hex];

                level[x, y] = index;
            }
        }

        for (int x = 0; x < levelTexture.width; x++)
        {
            for (int y = 0; y < levelTexture.height; y++)
            {
                byte cell = level[x, y];
                GameObject obj; 

                if (cell == 0) //isAir
                {
                    continue;
                }


                if (tileTypes[cell].isWall == true)
                {
                    if (tileTypes[level[x, y - 1]].isWalkable)
                    {
                        obj = Instantiate(tileTypes[cell].prefab[0], new Vector3(x, y), Quaternion.identity);
                        obj.transform.parent = this.transform;
                    }
                    else
                    {
                        obj = Instantiate(tileTypes[cell].prefab[1], new Vector3(x, y), Quaternion.identity);
                        obj.transform.parent = this.transform;
                    }
                }
                else
                {
                    obj = Instantiate(tileTypes[cell].prefab[0], new Vector3(x, y), Quaternion.identity);
                    obj.transform.parent = this.transform;
                }

            }
        }

        grid.CreateGrid(level, levelTexture.width, levelTexture.height,tileTypes);
    }

}

[System.Serializable]
public class TileTypes
{
    public byte index;
    public string name;
    public string hex;
    public GameObject[] prefab;
    public bool isWalkable;
    public bool isWall;
}
