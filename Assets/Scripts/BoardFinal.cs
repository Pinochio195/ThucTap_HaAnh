using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardFinal : MonoBehaviour
{
    public static BoardFinal instance;
    public List<GameObject> characters = new List<GameObject>();
    public GameObject tile;
    public int xSize, ySize;
    private GameObject[,] tiles;
    private int count;
    public bool isCheckGameOver = false;
    private bool isCheckOverPointToPlusSpites=false;
    [SerializeField] TextMeshProUGUI Points;
    private int Points_=0;
   
    void Start()
    {
        instance = GetComponent<BoardFinal>();     // 7
        Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
    }
    
    private void CreateBoard(float xOffset, float yOffset)
    {
        tiles = new GameObject[xSize, ySize];

        float startX = transform.position.x; 
        float startY = transform.position.y;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
                newTile.transform.parent = transform; 
                GameObject newSprite = characters[Random.Range(0, GridSuggest.instance.MyNumberCharacter)];
                //Debug.Log(GridSuggest.instance.MyNumberCharacter);
                if (y<5)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = newSprite.GetComponent<SpriteRenderer>().sprite;
                    newTile.gameObject.tag = newSprite.gameObject.tag;
                }
                else
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = null;
                    newTile.gameObject.tag = "NullSprites";
                }
                newTile.name = x + "-" + y;
                newTile.transform.position = new Vector2(x, y);
                tiles[x, y] = newTile;
            }
        }
    }
    public IEnumerator UpAllTile()
    {
        yield return new WaitForSeconds(1f);
        if (!CheckGameOver())
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = ySize-1; y >= 0; y--)
                {
                    if (tiles[x, y].GetComponent<SpriteRenderer>().sprite != null && tiles[x, y + 1].GetComponent<SpriteRenderer>().sprite == null)
                    {
                        //thay tag name
                        tiles[x, y + 1].tag = tiles[x, y].tag;
                        tiles[x, y].tag = "NullSprites";
                        //thay sprite
                        tiles[x, y + 1].GetComponent<SpriteRenderer>().sprite = tiles[x, y].GetComponent<SpriteRenderer>().sprite;
                        tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
                        StartCoroutine(CreateFirstRow());
                        StartCoroutine(GridSuggest.instance.CreateAfterClick());
                    }
                }
            }
        }
    }
    public IEnumerator CreateFirstRow()
    {
        GridSuggest gridSuggest = FindObjectOfType<GridSuggest>();
        yield return new WaitForSeconds(.3f);
        for (int x = 0; x < xSize; x++)
        {
            tiles[x, 0].GetComponent<SpriteRenderer>().sprite = gridSuggest.GetListSugget()[x].GetComponent<SpriteRenderer>().sprite;
            tiles[x, 0].tag = gridSuggest.GetListSugget()[x].tag;
        }
    }

    public bool CheckGameOver()
    {
        for (int x = 0; x < xSize; x++)
        {
            if (tiles[x, ySize - 1].GetComponent<SpriteRenderer>().sprite != null)
            {
                return true;
            }
        }
        return false;
    }
    public void Flood(int x, int y, string CurrentSprite, Color newColor)
    {
        WaitForSeconds wait = new WaitForSeconds(.001f);
        if (x >= 0 && x < xSize && y >= 0 && y < ySize && count <= 162)
        {
            ++count;
            if (tiles[x, y].tag == CurrentSprite)
            {
                //yield return wait;
                tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
                tiles[x, y].tag = "NullSprites";
                Points_ += + 10;
                Flood(x + 1, y, CurrentSprite, newColor);
                Flood(x - 1, y, CurrentSprite, newColor);
                Flood(x, y + 1, CurrentSprite, newColor);
                Flood(x, y - 1, CurrentSprite, newColor);
            }
        }
        else if (count > 162)
        {
            //yield return wait;
            count = 0;
        }
        if (Points_>50 && !isCheckOverPointToPlusSpites)
        {
            GridSuggest.instance.MyNumberCharacter++;
            isCheckOverPointToPlusSpites=true;
            //Debug.Log("T?ng thêm 1 sprites");
        }
        else if (Points_ > 10000 && isCheckOverPointToPlusSpites)
        {
            isCheckOverPointToPlusSpites = false;
        }
        Points.text = Points_.ToString() + "Points";
    }
    public IEnumerator DownTile()
    {
        yield return new WaitForSeconds(0.25f);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (y > 0 )
                {
                    if (tiles[x,y].GetComponent<SpriteRenderer>().sprite != null && tiles[x, y-1].GetComponent<SpriteRenderer>().sprite == null)
                    {
                        while (y > 0 && tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite == null)
                        {
                            //thay tag name
                            tiles[x, y - 1].tag = tiles[x, y].tag;
                            tiles[x, y].tag = "NullSprites";
                            //thay sprite
                            tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite = tiles[x, y].GetComponent<SpriteRenderer>().sprite;
                            tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
                            if (y>0)
                            {
                                y--;
                            }
                        }
                    }
                }
            }
        }
       

    }
    //t?o g?i ý //kh?i t?o danh sách m?i r?i ??y sang bên kia tránh b? trùng hàng ??u 
   
    //tr? v? danh sách sprites
    public List<GameObject> GetCharacter()
    {
        return characters;
    }

}
