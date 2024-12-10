using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Minimap : MonoBehaviour
{
    public Tilemap miniMapTile;
    private Tilemap lastestMap;
    private Tilemap currentMap;

    public Transform[] allMap;

    public Tile whiteTile;

    private void Update()
    {
        FindCurrentMap();

        SetMiniMapTile();

        SetEnemyPos();
    }

    private void FindCurrentMap()
    {
        currentMap = allMap.OrderBy(a => Vector3.Distance(Camera.main.transform.position, a.position)).First().GetComponent<Tilemap>();
    }

    private void SetMiniMapTile()
    {
        if(lastestMap != currentMap)
        {
            lastestMap = currentMap;
            BoundsInt bounds = miniMapTile.cellBounds;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                    miniMapTile.SetTile(new Vector3Int(x, y, 0), null);
        }
         
        List<Vector3Int> fillPos = GetFilledTilePositions(currentMap);

        foreach (Vector3Int pos in fillPos)
        {
            miniMapTile.SetTile(pos, whiteTile);
        }
    }

    private List<Vector3Int> GetFilledTilePositions(Tilemap tilemap)
    {
        List<Vector3Int> filledPositions = new List<Vector3Int>();

        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(position))
                {
                    filledPositions.Add(position);
                }
            }
        }

        return filledPositions;
    }

    private void SetEnemyPos()
    {

    }
}
