using System.Collections.Generic;
using UnityEngine;

public class MapBuilder
{
    public virtual Vector2Int GetStartPosition()
    {
        return Configurations.StartPosition;
    }

    public virtual List<List<TerrainType>> GenerateMap()
    {
        var map = new List<List<TerrainType>>();

        for (var width = 0; width < Configurations.GridWidth; width++)
        {
            var row = new List<TerrainType>();
            for (var height = 0; height < Configurations.GridHeight; height++)
            {
                if(Random.Range(0f, 1f) <= Configurations.ObstacleProbability)
                    row.Add(TerrainType.TREE);
                else
                    row.Add(TerrainType.GRASS);
            }
            map.Add(row);
        }
        map[Configurations.StartPosition.x][Configurations.StartPosition.y] = TerrainType.START;
        map[Configurations.GridWidth -1][Configurations.GridHeight -1] = TerrainType.FINISH;
            
        return map;
    }
}
