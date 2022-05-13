using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite!=null&& !BoardFinal.instance.isCheckGameOver)
        {
            DeleteTile();
        }
        //Debug.Log(BoardFinal.instance.isCheckGameOver);
    }
    public GameObject GetPosition()
    {
        return gameObject;
    }
    private void DeleteTile()
    {
        BoardFinal boardFinal = FindObjectOfType<BoardFinal>();
        boardFinal.Flood((int)transform.position.x, (int)transform.position.y,gameObject.tag, Color.red);
        StartCoroutine(boardFinal.DownTile());
        StartCoroutine(boardFinal.UpAllTile());
        StartCoroutine(restartgame(boardFinal));

    }
    private IEnumerator restartgame(BoardFinal boardFinal)
    {
        yield return new WaitForSeconds(.9f);
        if (boardFinal.CheckGameOver())
        {
            Debug.Log("GameOver");
            UiManagerment.instance.CanVas.GetComponent<Canvas>().sortingOrder = 2;
            UiManagerment.instance.btnRestart.SetActive(true);
            BoardFinal.instance.isCheckGameOver = true;
        }
    }

	
}
