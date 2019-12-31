using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool displayGridGizmos;
    public float nodeRadius;
    int width;
    int height;

    Node[,] grid;

    public void CreateGrid(byte[,] level, int _width, int _height,List<TileTypes> tileTypes)
    {
        width = _width;
        height = _height;
        grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tileTypes[level[x,y]].isWalkable)
                {
                    grid[x, y] = new Node(true, new Vector2(x + .5f,y + .5f),x,y);
                }
                else
                {
                    grid[x, y] = new Node(false, new Vector2(x + .5f, y + .5f),x,y);
                }
            }
        }
    }

    public int MaxSize
    {
        get
        {
            return width * height;
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbors = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0 || x ==1 && y==1 || x==-1 && y ==1 || x==-1 && y==-1 || x==1 && y==-1)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < grid.GetLength(0) && checkY >= 0 && checkY < grid.GetLength(1))
                {
                    neighbors.Add(grid[checkX,checkY]);
                }
            }
        }
        return neighbors;
    }


    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        int xx = Mathf.Clamp(Mathf.FloorToInt(worldPos.x),0,width);
        int yy = Mathf.Clamp(Mathf.FloorToInt(worldPos.y),0,height);
        //Mathf.Clamp(xx, 0, width);
       // Mathf.Clamp(yy, 0, height);
        return grid[xx, yy];
    }


    private void OnDrawGizmos()
    {
        if (grid != null && displayGridGizmos)
        {
            foreach (Node n in grid)
            {
                if (n.walkable == true)
                {
                    Gizmos.color = Color.white;
                }
                else
                    Gizmos.color = Color.red;
                Gizmos.DrawSphere(n.worldPosition, nodeRadius);
            }
        }
        
    }

}
