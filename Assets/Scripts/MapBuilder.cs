using System.Collections.Generic;
using UnityEngine;

public enum TerrainType
{
    START = 0,
    GRASS,
    TREE,
    FINISH
}

public class MapBuilder
{
    public static int GridWidth = 3;
    public static int GridHeight = 4;
    public static float ObstacleProbability = .2f;
    public static Vector2Int StartPosition = Vector2Int.zero;

    public virtual Vector2Int GetStartPosition()
    {
        return StartPosition;
    }

    public virtual List<List<TerrainType>> GenerateMap()
    {
        var map = new List<List<TerrainType>>();

        for (var width = 0; width < GridWidth; width++)
        {
            var row = new List<TerrainType>();
            for (var height = 0; height < GridHeight; height++)
            {
                if(Random.Range(0f, 1f) <= ObstacleProbability)
                    row.Add(TerrainType.TREE);
                else
                    row.Add(TerrainType.GRASS);
            }
            map.Add(row);
        }
        map[StartPosition.x][StartPosition.y] = TerrainType.START;
        map[GridWidth-1][GridHeight-1] = TerrainType.FINISH;
            
        return map;
    }
}
