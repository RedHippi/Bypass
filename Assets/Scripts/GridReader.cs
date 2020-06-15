using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class GridReader : MonoBehaviour
{
    //Please set this!!!! TODO find a way to alert the developer of this fact
    public TextAsset file;

    [SerializeField] private GameObject[] MapTiles;
    [SerializeField] private GameObject BackGround;

    private float TileSize
    {
        get { return MapTiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    void Start()
    {

        CreateMap();

    }

    private void CreateMap()
    {
        string[] Map = ReadLevelText();

        int MapX = Map[0].Length;
        int MapY = Map.Length;

        Bounds back = BackGround.GetComponent<SpriteRenderer>().sprite.bounds;
        float MapStartX = (back.min.x * BackGround.transform.localScale.x) + BackGround.transform.position.x + TileSize / 2;
        float MapStartY = (back.max.y * BackGround.transform.localScale.y) + BackGround.transform.position.y - TileSize / 2;
        Vector2 MapStart = new Vector2(MapStartX, MapStartY);

        for(int y = 0; y < MapY; y++)
        {
            char[] newTiles = Map[y].ToCharArray();
            for(int x = 0; x < MapX; x++)
            {
                PlaceTile(newTiles[x], x, y, MapStart);
            }
        }
    }

    private void PlaceTile(char type, int x, int y, Vector2 MapStart)
    {
        int index = int.Parse(type.ToString());
        GameObject newTile = Instantiate(MapTiles[index]);

        newTile.transform.position = new Vector2(MapStart.x + (x * TileSize), MapStart.y - (y * TileSize));
    }

    private string[] ReadLevelText()
    {
        string input = file.text.Replace(Environment.NewLine, string.Empty);

        return input.Split('~');
    }
    
}
