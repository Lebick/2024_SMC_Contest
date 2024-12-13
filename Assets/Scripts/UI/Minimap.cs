using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Minimap : Singleton<Minimap>
{
    public Tilemap miniMapTile;
    private Tilemap lastestMap;
    private Tilemap currentMap;

    public Transform[] allMap;

    public Tile whiteTile;
    public GameObject enemyPos;
    public List<Controller> enemys = new();
    public List<GameObject> enemyPoses = new();

    private Transform player;
    public Transform playerPos;

    private void Start()
    {
        player = UsefulObjectManager.instance.player.transform;
    }

    private void Update()
    {
        FindCurrentMap();

        SetMiniMapTile();

        GetEnemyPos();
        SetEnemyPos();
        SetPlayerPos();
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

    private void GetEnemyPos()
    {
        if (currentMap.transform.parent.Find("Enemys") == null) return;

        int currentEnemyCount = currentMap.transform.parent.Find("Enemys").GetComponentsInChildren<Controller>().ToList().Count;
        enemys = currentMap.transform.parent.Find("Enemys").GetComponentsInChildren<Controller>().ToList();

        if (enemys.Count > enemyPoses.Count)
        {
            GameObject pos = Instantiate(enemyPos, transform);
            enemyPoses.Add(pos);
        }
        else if(enemys.Count < enemyPoses.Count)
        {
            Destroy(enemyPoses[0]);
            enemyPoses.RemoveAt(0);
        }

        for (int i=0; i<enemys.Count; i++)
        {
            try
            {
                enemyPoses[i].transform.localPosition = enemys[i].transform.localPosition;
            }
            catch { }
            
        }
    }

    private void SetEnemyPos()
    {
        for(int i=0; i< enemyPoses.Count; i++)
        {
            try
            {
                enemyPoses[i].transform.localPosition = enemys[i].transform.localPosition;
            }
            catch
            {

            }
        }
    }

    private void SetPlayerPos()
    {
        playerPos.localPosition = player.position - currentMap.transform.position;
    }
}
