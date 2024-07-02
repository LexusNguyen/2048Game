using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }

    private void Start()
    {
        if (rows == null || rows.Length == 0)
        {
            Debug.LogError("Rows array is not initialized or empty.");
            return;
        }

        for (int y = 0; y < rows.Length; y++)
        {
            if (rows[y].cells == null || rows[y].cells.Length == 0)
            {
                Debug.LogError($"Cells array in row {y} is not initialized or empty.");
                continue;
            }

            for (int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        } 
        else
        {
            return null;
        }
    }

    public TileCell GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.y);
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates; 
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates);
    }

    public TileCell GetRandomEmptyCell()
    {
        int idx = Random.Range(0, cells.Length);
        int startingIdx = idx;

        while (cells[idx].occupied)
        {
            idx++;

            if (idx >= cells.Length)
            {
                idx = 0;
            }

            if (idx == startingIdx)
            {
                return null;
            }
        }

        return cells[idx];
    }
}
