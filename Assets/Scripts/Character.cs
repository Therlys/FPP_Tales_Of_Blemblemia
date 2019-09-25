using System.Collections;
using UnityEngine;
using Utils;

public abstract class Character : MonoBehaviour
{
    private Tile currentTile = null;
    [SerializeField] private Vector2Int initialPosition;

    private readonly int range;
    public int Range => range;

    protected Character(int range)
    {
        this.range = range;
    }

    protected void Start()
    {
        StartCoroutine(InitPosition());
    }

    private void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    public void MoveTo(Tile tile)
    {
        if (currentTile != null) currentTile.UnlinkCharacter();
        currentTile = tile;
        if(currentTile != null && currentTile.LinkCharacter(this)) MoveTo(currentTile.Position);
    }

    private IEnumerator InitPosition()
    {
        yield return new WaitForEndOfFrame();
        MoveTo(Finder.GridController.GetTile(initialPosition.x, initialPosition.y));
    }
    
}
