using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private List<GameObject> terrainPrefabs;
    [SerializeField] private GameObject player;

    private List<List<GameObject>> grid;
    private Vector2Int characterPosition;
    private List<List<TerrainType>> map;

    private void Start()
    {
        MapBuilder mapBuilder = new MapBuilder();
        map = mapBuilder.GenerateMap();
        characterPosition = mapBuilder.GetStartPosition();

        InitializeMap(map, characterPosition);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var posibleNewPosition = new Vector2Int(characterPosition.x - 1, characterPosition.y);
            if (posibleNewPosition.x >= 0 && map[posibleNewPosition.y][posibleNewPosition.x] != TerrainType.TREE)
            {
                characterPosition = posibleNewPosition;

                GameObject gridCell = grid[characterPosition.y][characterPosition.x];
                player.transform.SetParent(gridCell.transform);
                player.transform.localPosition = Vector3.zero;

                if (map[characterPosition.y][characterPosition.x] == TerrainType.FINISH)
                    Debug.Log("YOU WIN!!!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var posibleNewPosition = new Vector2Int(characterPosition.x + 1, characterPosition.y);
            if (posibleNewPosition.x < map[posibleNewPosition.y].Count && map[posibleNewPosition.y][posibleNewPosition.x] != TerrainType.TREE)
            {
                characterPosition = posibleNewPosition;

                GameObject gridCell = grid[characterPosition.y][characterPosition.x];
                player.transform.SetParent(gridCell.transform);
                player.transform.localPosition = Vector3.zero;

                if (map[characterPosition.y][characterPosition.x] == TerrainType.FINISH)
                    Debug.Log("YOU WIN!!!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var posibleNewPosition = new Vector2Int(characterPosition.x, characterPosition.y + 1);
            if (posibleNewPosition.y < map.Count && map[posibleNewPosition.y][posibleNewPosition.x] != TerrainType.TREE)
           {
                characterPosition = posibleNewPosition;

                GameObject gridCell = grid[characterPosition.y][characterPosition.x];
                player.transform.SetParent(gridCell.transform);
                player.transform.localPosition = Vector3.zero;

                if (map[characterPosition.y][characterPosition.x] == TerrainType.FINISH)
                    Debug.Log("YOU WIN!!!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var posibleNewPosition = new Vector2Int(characterPosition.x, characterPosition.y - 1);
            if (posibleNewPosition.y >= 0 && map[posibleNewPosition.y][posibleNewPosition.x] != TerrainType.TREE)
           {
                characterPosition = posibleNewPosition;

                GameObject gridCell = grid[characterPosition.y][characterPosition.x];
                player.transform.SetParent(gridCell.transform);
                player.transform.localPosition = Vector3.zero;

                if (map[characterPosition.y][characterPosition.x] == TerrainType.FINISH)
                    Debug.Log("YOU WIN!!!");
            }
        }
    }

    public void InitializeMap(List<List<TerrainType>> map, Vector2Int characterPosition)
    {
        grid = new List<List<GameObject>>();

        for (var row = 0; row < map.Count; row++)
        {
            var gridRow = new List<GameObject>();
            for (var column = 0; column < map[row].Count; column++)
            {
                var terrainType = map[row][column];

                var gridCell = Instantiate(terrainPrefabs[(int)terrainType], transform);
                gridCell.transform.localPosition = new Vector3(column * gridCellSize, row * gridCellSize, 1);
                gridRow.Add(gridCell);
            }
            grid.Add(gridRow);
        }

        GameObject startGridCell = grid[characterPosition.y][characterPosition.x];
        player.transform.SetParent(startGridCell.transform);
        player.transform.localPosition = Vector3.zero;
    }
}
