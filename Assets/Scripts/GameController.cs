using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    private Vector2Int characterPosition;
    private List<List<TerrainType>> map;

    private GameView gameView;

    public GameController(GameView view, MapBuilder mapBuilder)
    {
        gameView = view;

        map = mapBuilder.GenerateMap();
        characterPosition = mapBuilder.GetStartPosition();

        gameView.InitializeMap(map, characterPosition);
    }

    public void MoveCharacterRight()
    {
        if (IsValidPosition(characterPosition.x + 1, characterPosition.y))
        {
            MoveCharacterToPosition(characterPosition.x + 1, characterPosition.y);
        }
    }

    public void MoveCharacterLeft()
    {
        if (IsValidPosition(characterPosition.x - 1, characterPosition.y))
        {
            MoveCharacterToPosition(characterPosition.x - 1, characterPosition.y);
        }
    }

    public void MoveCharacterUp()
    {
        if (IsValidPosition(characterPosition.x, characterPosition.y + 1))
        {
            MoveCharacterToPosition(characterPosition.x, characterPosition.y + 1);
        }
    }

    public void MoveCharacterDown()
    {
        if (IsValidPosition(characterPosition.x, characterPosition.y - 1))
        {
            MoveCharacterToPosition(characterPosition.x, characterPosition.y - 1);
        }
    }

    private void MoveCharacterToPosition(int newX, int newY)
    {
        characterPosition = new Vector2Int(newX, newY);
        gameView.MovePlayerToCell(characterPosition.x, characterPosition.y);
        CheckIfWin();
    }
    private void CheckIfWin()
    {
        if (map[characterPosition.y][characterPosition.x] == TerrainType.FINISH)
            gameView.ShowWinFeedback();
    }

    private bool IsValidPosition(int x, int y)
    {
        return PositionExistsInMap(x, y) && ThereIsNoTreeInPosition(x, y);
    }

    private bool ThereIsNoTreeInPosition(int x, int y)
    {
        return map[y][x] != TerrainType.TREE;
    }

    private bool PositionExistsInMap(int x, int y)
    {
        return y >= 0 &&
               y < map.Count &&
               x >= 0 &&
               x < map[y].Count;
    }
}
