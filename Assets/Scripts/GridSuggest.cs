using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSuggest : MonoBehaviour
{
    public GameObject tile;
    public int xSize;
    public GameObject[,] tiles;
    static public GridSuggest instance;
    //List<GameObject> list = new List<GameObject>();
    BoardFinal boardFinal ;
    int NumberCharacter;
    private void Awake()
    {
        instance = this;
        boardFinal = FindObjectOfType<BoardFinal>();
        NumberCharacter = boardFinal.GetCharacter().Count-1;
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
    }
    private void CreateBoard()
    {
        tiles = new GameObject[xSize, 1];
        float startX = transform.position.x;
        float startY = transform.position.y;
        for (int x = 0; x < xSize; x++)
        {
            GameObject newTile = Instantiate(tile, new Vector2(0 , 0), tile.transform.rotation);
            newTile.transform.parent = transform;
            StartCoroutine(CreateAfterClick());
            newTile.name = x + "-";
            newTile.transform.position = new Vector2(x+11, 0);
            tiles[x, 0] = newTile;
        }
    }
    //hàm này cho t?o l?i g?i ý sau m?i l?n click
    public IEnumerator CreateAfterClick()
    {
        GameObject newSprite;
        yield return new WaitForSeconds(.5f);
        for (int x = 0; x < xSize; x++)
        {
            newSprite = boardFinal.GetCharacter()[Random.Range(0, NumberCharacter)];
            //Debug.Log("Hello"+MyNumberCharacter);
            tiles[x, 0].GetComponent<SpriteRenderer>().sprite = newSprite.GetComponent<SpriteRenderer>().sprite;
            tiles[x, 0].gameObject.tag = newSprite.gameObject.tag;
        }
    }
    public List<GameObject> GetListSugget()
    {
        List<GameObject> list = new List<GameObject>();
        for (int x = 0; x < xSize; x++)
        {
            list.Add(tiles[x, 0]);
        }
        //Debug.Log("T?ng s? list:" + list.Count);

        return list;
    }

    public int MyNumberCharacter{ get => NumberCharacter; set => NumberCharacter = value; }
}
