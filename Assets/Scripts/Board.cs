using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board instance;
    public List<GameObject> characters = new List<GameObject>();
    public GameObject tile;
    public int xSize, ySize;

    private GameObject[,] tiles;

    public bool IsShifting { get; set; }
    
    void Start()
    {
        instance = GetComponent<Board>();     // 7
        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
    }

    private void CreateBoard(float xOffset, float yOffset)
    {
        tiles = new GameObject[xSize, ySize];     // 9

        float startX = transform.position.x;     // 10
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++)
        {      // 11
            for (int y = 0; y < ySize; y++)
            {
                    GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
                    newTile.transform.parent = transform; // 1
                    GameObject newSprite = characters[Random.Range(0, characters.Count)]; // 2
                    newTile.GetComponent<SpriteRenderer>().sprite = newSprite.GetComponent<SpriteRenderer>().sprite; // 3
                    newTile.name = x + "-" + y;
                    newTile.transform.position = new Vector2(x, y);
                    newTile.gameObject.tag = newSprite.gameObject.tag;
                    tiles[x, y] = newTile;
            }
        }
    }
    //Thay ??i sprites cho hàng
    public Sprite ChangeSprites(string NameOfGameObject , Sprite CurrentSprite,GameObject CurrentTile)
    {
        int a = 0;
        int b = 0;
        Sprite sprite = CurrentTile.GetComponent<SpriteRenderer>().sprite;
        for (int x = 0; x < ySize; x++)
        {
            for (int y = 0; y < xSize; y++)
            {
                if (y < xSize - 1)
                {
                    if (tiles[x, y].name == NameOfGameObject)
                    {
                        sprite = tiles[x, y + 1].GetComponent<SpriteRenderer>().sprite;
                        a = x;
                        b = y;
                        break;
                    }
                }
            }
        }
        tiles[a,b+1].GetComponent<SpriteRenderer>().sprite = CurrentSprite;
        CurrentSprite = sprite;
        return CurrentSprite;
    }
    //
    
    //Thêm các tile vào danh sách ?? xóa
    public void DeleteTileInList(string NameOfGameObject)
    {
        //Debug.Log(NameOfGameObject);
        List<GameObject> List_ColumDelete = new List<GameObject> ();
        List<GameObject> List_RowDelete = new List<GameObject> ();

        bool isCheckRemove = false;
        bool trueTile = false;

        bool isCheckRemove1=false;
        bool trueTile1=false;
        for (int x = 0; x < ySize; x++)
        {
            for (int y = 0; y < xSize ; y++)
            {
                if (tiles[x,y].name == NameOfGameObject)
                {
                    #region Get up from click locaiton
                    //Up
                    if (y < xSize-1)
                    {
                        for (int i = y; i < xSize - 1 && tiles[x, i].tag == tiles[x, i + 1].tag; i++)
                        {
                            List_ColumDelete.Add(tiles[x, i]);
                            if (i<xSize-2 && tiles[x, i+1].tag != tiles[x, i + 2].tag)
                            {
                                List_ColumDelete.Add(tiles[x, i+1]);
                            }
                            else if(i+2 == xSize)
                            {
                                List_ColumDelete.Add(tiles[x, i + 1]);
                            }
                            
                        }
                    }
                    #endregion
                    #region Get down from click location
                    //Down
                    if (y > 0)
                    {
                        for (int i = y; i > 0 && tiles[x, i].tag == tiles[x, i - 1].tag; i--)
                        {
                            for (int a = 0; a < List_ColumDelete.Count; a++)
                            {
                                if (tiles[x, i].name == List_ColumDelete[a].name && !isCheckRemove && !trueTile)
                                {
                                    List_ColumDelete.Remove(List_ColumDelete[a]);
                                    isCheckRemove = true;
                                }
                            }
                            List_ColumDelete.Add(tiles[x, i]);
                            // l?y ra th?ng cu?i cùng c?a match
                            if (i > 1 && tiles[x, i - 1].tag != tiles[x, i - 2].tag && List_ColumDelete.Count>1)
                            {
                                List_ColumDelete.Add(tiles[x, i - 1]);
                            }
                            else if(i-1==0 && tiles[x, i].tag == tiles[x, i - 1].tag)
                            {
                                List_ColumDelete.Add(tiles[x, i - 1]);
                            }
                        }
                    }
                    #endregion
                    #region Get left from click locaiton
                    //Right
                    if (x < xSize - 1)
                    {
                        for (int i = x; i < xSize - 1 && tiles[i, y].tag == tiles[i+1, y].tag; i++)
                        {
                            List_RowDelete.Add(tiles[i, y]);
                            if (i < xSize - 2 && tiles[i+1, y].tag != tiles[i+2, y].tag)
                            {
                                List_RowDelete.Add(tiles[i+1, y]);
                            }
                            else if (i + 2 == xSize)
                            {
                                List_RowDelete.Add(tiles[i+1, y]);
                            }
                        }
                    }
                    #endregion
                    #region Get right from click location
                    //Left
                    if (y > 0)
                    {
                        for (int i = x; i > 0 && tiles[i, y].tag == tiles[i - 1,y].tag; i--)
                        {
                            for (int a = 0; a < List_RowDelete.Count; a++)
                            {
                                if (tiles[i, y].name == List_RowDelete[a].name && !isCheckRemove1 && !trueTile1)
                                {
                                    List_RowDelete.Remove(List_RowDelete[a]);
                                    isCheckRemove1 = true;
                                }
                            }
                            List_RowDelete.Add(tiles[i, y]);
                            // l?y ra th?ng cu?i cùng c?a match
                            if (i > 1 && tiles[i - 1, y].tag != tiles[i - 2,y].tag && List_RowDelete.Count > 1)
                            {
                                List_RowDelete.Add(tiles[i - 1,y]);
                            }
                            else if (i - 1 == 0 && tiles[i, y].tag == tiles[i - 1,y].tag)
                            {
                                List_RowDelete.Add(tiles[i - 1,y]);
                            }
                        }
                    }
                    #endregion
                    
                    #region Delete tile in list when tile small
                    //column
                    int xColumnDelete = List_ColumDelete.Count;
                    for (int i = 0; i < xColumnDelete; i++)
                    {
                        if (List_ColumDelete.Count < 3)
                        {
                            List_ColumDelete.Remove(List_ColumDelete[0]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    //row
                    int xRowDelete = List_RowDelete.Count;
                    for (int i = 0; i < xRowDelete; i++)
                    {
                        //Debug.Log(List_ColumDelete.Count);
                        if (List_RowDelete.Count < 3)
                        {
                            List_RowDelete.Remove(List_RowDelete[0]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    //
                    #region Display tile in list
                    for (int p = 0; p < List_ColumDelete.Count; p++)
                    {
                        Debug.Log(List_ColumDelete[p].name);
                    }
                    for (int p = 0; p < List_RowDelete.Count; p++)
                    {
                        Debug.Log(List_RowDelete[p].name);
                    }
                    #endregion
                    //
                    #region Get tile null
                    //column
                    for (int i = 0; i < List_ColumDelete.Count; i++)
                    {
                        List_ColumDelete[i].GetComponent<SpriteRenderer>().sprite = null;
                    }
                    //row
                    for (int i = 0; i < List_RowDelete.Count; i++)
                    {
                        List_RowDelete[i].GetComponent<SpriteRenderer>().sprite = null;
                    }
                    #endregion
                    //Debug.Log("T?ng ph?n t? trong danh sách :" + List_ColumDelete.Count);
                }
            }
        }
        //set thành null
        //SpritesNull(List_ColumDelete,List_RowDelete);
    }
    public void SpritesNull(List<GameObject> List_ColumDelete, List<GameObject> List_RowDelete)
    {
        //Column
        for (int x = 0; x < ySize; x++)
        {
            for (int y = 0; y < xSize; y++)
            {
                if (y > 0)
                {
                    if (tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite == null && tiles[x, y].GetComponent<SpriteRenderer>().sprite != null)
                    {
                        int columDie = y;
                        for (int j = 0; j < List_ColumDelete.Count; j++)
                        {
                            if (columDie > 0)
                            {
                                string tagName = tiles[x, columDie].tag;
                                //thay tag name
                                tiles[x, columDie].tag = tiles[x, columDie - 1].tag;
                                tiles[x, columDie - 1].tag = tagName;
                                //thay sprite
                                tiles[x, columDie - 1].GetComponent<SpriteRenderer>().sprite = tiles[x, columDie].GetComponent<SpriteRenderer>().sprite;
                                tiles[x, columDie].GetComponent<SpriteRenderer>().sprite = null;
                                columDie--;
                            }
                        }
                    }
                }
            }
        }
        //Row
        for (int x = 0; x < ySize; x++)
        {
            for (int y = 0; y < xSize; y++)
            {
                if (y > 0)
                {
                    if (tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite == null && tiles[x, y].GetComponent<SpriteRenderer>().sprite != null)
                    {
                        int RowDie = y;
                        if (RowDie > 0)
                        {
                            string tagName = tiles[x, RowDie].tag;
                            //thay tag name
                            tiles[x, RowDie].tag = tiles[x, RowDie - 1].tag;
                            tiles[x, RowDie - 1].tag = tagName;
                            //thay sprite
                            tiles[x, RowDie - 1].GetComponent<SpriteRenderer>().sprite = tiles[x, RowDie].GetComponent<SpriteRenderer>().sprite;
                            tiles[x, RowDie].GetComponent<SpriteRenderer>().sprite = null;
                        }
                        
                    }
                }
            }
        }
    }

}
