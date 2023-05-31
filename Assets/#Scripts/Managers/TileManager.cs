using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{

    public static TileManager instance;
    
    [SerializeField] private Transform tileRoot;

    private Tile[,] tiles = new Tile[10,10];
    private bool[,] tilesStatus = new bool[10, 10];
    private List<Tile> previewList = new List<Tile>();
    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        RegisterTiles();
    }

    //Filling a 2D array of tiles along with tileRoot's children
    private void RegisterTiles()
    {
        int index = 0;
        
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tiles[i, j] = tileRoot.GetChild(index).GetComponent<Tile>();
                tilesStatus[i, j] = false;
                index++;
            }
        }
    }

    public Vector3 GetTilePos(Vector2Int coord)
    {
        return tiles[coord.x, coord.y].transform.position;
    }
    public bool Preview(Shape shape)
    {
        Tile centerTile = GetClosestTile(shape.transform.position);
        if(!centerTile) return false;

        for (int i = 0; i < previewList.Count; i++)
        {
            if(!tilesStatus[previewList[i].tileCoord.x,previewList[i].tileCoord.y])
                previewList[i].GetComponent<Image>().color = new Color(0.3764706f,0.3764706f,0.3764706f,0.4313726f);;
        }
        previewList = new List<Tile>();
        bool[] pieces = shape.pieces;
        
        for (int i = 0; i < pieces.Length; i++)
        {
            if(!pieces[i]) continue;
            Vector2Int tileCoord = centerTile.tileCoord + new Vector2Int(-1,-1)+new Vector2Int(i / 3, i % 3);
            if (tileCoord.x < 0 || tileCoord.x > 9 || tileCoord.y < 0 || tileCoord.y > 9)
                return false;

            if(tilesStatus[tileCoord.x,tileCoord.y])
                return false;
        }

        for (int i = 0; i < pieces.Length; i++)
        {
            if(!pieces[i]) continue;
            Vector2Int tileCoord = centerTile.tileCoord + new Vector2Int(-1,-1)+ new Vector2Int(i / 3, i % 3);

            var tileImage = tiles[tileCoord.x,tileCoord.y].GetComponent<Image>();
            tileImage.color = new Color(0.7830189f,0.7830189f,0.7830189f,0.4313726f);
            previewList.Add(tiles[tileCoord.x,tileCoord.y]);
        }
        return true;
    }

    public FillInfo Fill(Shape shape)
    {
        FillInfo info = new FillInfo();
        List<int> paintedTiles = new List<int>();

        Tile centerTile = GetClosestTile(shape.transform.position);
        if(!centerTile) return info;
        var col = shape.pieceColor;
        bool[] pieces = shape.pieces;
        
        for (int i = 0; i < pieces.Length; i++)
        {
            if(!pieces[i]) continue;
            Vector2Int tileCoord = centerTile.tileCoord + new Vector2Int(-1,-1) + new Vector2Int(i / 3, i % 3);
            
            tiles[tileCoord.x,tileCoord.y].GetComponent<Image>().color = col;
            tilesStatus[tileCoord.x, tileCoord.y] = true;
            
            paintedTiles.Add(99-(tileCoord.x * 10 + tileCoord.y));
        }

        CheckFullColumRow(centerTile);
        info.paintedTiles = paintedTiles.ToArray();
        info.leftTopCoord = new Vector2Int(9,9) - centerTile.tileCoord;
        Debug.LogError(info.leftTopCoord);
        return info;
    }
    public void Fill(int[] paintedTiles, Vector2Int leftTopCoord,Color col)
    {
        for (int i = 0; i < paintedTiles.Length; i++)
        {
            Vector2Int coord = tileRoot.GetChild(paintedTiles[i]).GetComponent<Tile>().tileCoord;
            tiles[coord.x, coord.y].GetComponent<Image>().color = col;
            tilesStatus[coord.x, coord.y] = true;
        }

        CheckFullColumRow(leftTopCoord);
    }

    private void CheckFullColumRow(Tile centerTile)
    {
        List<int> destroyRow = new List<int>();
        List<int> destroyColum = new List<int>();

        var coord = centerTile.tileCoord + new Vector2Int(-1,-1);
        for (int j = 0; j < 3; j++)
        {
            var row = coord.x + j;
            if(row < 0 || row > 9) continue;
            int tileInRow = 0;
            
            for (int i = 0; i < 10; i++)
            {
                if (tilesStatus[row, i])
                    tileInRow++;
            }
            if(tileInRow == 10)
                destroyRow.Add(row);
        }
        for (int j = 0; j < 3; j++)
        {
            var colum = coord.y + j;
            if(colum < 0 || colum > 9) continue;
            int tileInColum = 0;
            
            for (int i = 0; i < 10; i++)
            {
                if (tilesStatus[i, colum])
                    tileInColum++;
            }
            if(tileInColum == 10)
                destroyColum.Add(colum);
        }

        for (int i = 0; i < destroyRow.Count; i++)
        {
            DestroyRow(destroyRow[i]);
        }

        for (int i = 0; i < destroyColum.Count; i++)
        {
            DestroyColum(destroyColum[i]);
        }
    }
    private void CheckFullColumRow(Vector2Int coord)
    {
        List<int> destroyRow = new List<int>();
        List<int> destroyColum = new List<int>();

        for (int j = 0; j < 3; j++)
        {
            var row = coord.x + j;
            if(row < 0 || row > 9) continue;
            int tileInRow = 0;
            
            for (int i = 0; i < 10; i++)
            {
                if (tilesStatus[row, i])
                    tileInRow++;
            }
            if(tileInRow == 10)
                destroyRow.Add(row);
        }
        for (int j = 0; j < 3; j++)
        {
            var colum = coord.y + j;
            if(colum < 0 || colum > 9) continue;
            int tileInColum = 0;
            
            for (int i = 0; i < 10; i++)
            {
                if (tilesStatus[i, colum])
                    tileInColum++;
            }
            if(tileInColum == 10)
                destroyColum.Add(colum);
        }

        for (int i = 0; i < destroyRow.Count; i++)
        {
            DestroyRow(destroyRow[i]);
        }

        for (int i = 0; i < destroyColum.Count; i++)
        {
            DestroyColum(destroyColum[i]);
        }
    }

    private void DestroyRow(int row)
    {
        for (int i = 0; i < 10; i++)
        {
            tiles[row,i].GetComponent<Image>().color = new Color(0.3764706f,0.3764706f,0.3764706f,0.4313726f);
            tilesStatus[row, i] = false;
        }
    }
    private void DestroyColum(int colum)
    {
        for (int i = 0; i < 10; i++)
        {
            tiles[i,colum].GetComponent<Image>().color = new Color(0.3764706f,0.3764706f,0.3764706f,0.4313726f);
            tilesStatus[i, colum] = false;
        }
    }

    Tile GetClosestTile(Vector3 pos)
    {
        pos += new Vector3(146, -146, 0);
        Transform tMin = null;
        float minDist = 95;
        foreach (Tile t in tiles)
        {
            float dist = Vector3.Distance(t.transform.position, pos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }

        if (tMin)
            return tMin.GetComponent<Tile>();
        
            return null;
    }
}































