using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private List<GameObject> terrainPrefabs;
    [SerializeField] private GameObject player;

    private List<List<GameObject>> grid;
    private GameController gameController;

    private void Start()
    {
        gameController = new GameController(this, new MapBuilder());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            gameController.MoveCharacterLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            gameController.MoveCharacterRight();
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            gameController.MoveCharacterUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            gameController.MoveCharacterDown();
    }    

    public void ShowWinFeedback()
    {
        Debug.Log("YOU WIN!!!");
    }

    private void MovePlayerToCell(GameObject gridCell)
    {
        player.transform.SetParent(gridCell.transform);
        player.transform.localPosition = Vector3.zero;
    }

    public void MovePlayerToCell(int row, int column)
    {
        MovePlayerToCell(grid[column][row]);
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

        MovePlayerToCell(characterPosition.x, characterPosition.y);
    }
}
