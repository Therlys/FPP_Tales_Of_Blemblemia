using System;
using System.Collections;
using UnityEngine;
using Utils;

public abstract class Character : MonoBehaviour
{
    private Tile currentTile = null;
    [SerializeField] private Vector2Int initialPosition;
    private int healthPoints = 6;
    private bool canPlay = false;
    private int movesLeft = 3;
    private GridController gridController;
    public bool IsCurrentlySelected => gridController.SelectedCharacter == this;

    public int MovesLeft
    {
        get => movesLeft;
        set => movesLeft = value;
    }

    public bool CanPlay
    {
        get => canPlay;
        set => canPlay = value;
    }

    private readonly int range;
    public int Range => range;

    protected Character(int range)
    {
        this.range = range;
    }

    private void Awake()
    {
        gridController = Finder.GridController;
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
        MovesLeft -= 1;
        if (currentTile != null) currentTile.UnlinkCharacter();
        currentTile = tile;
        if(currentTile != null && currentTile.LinkCharacter(this)) MoveTo(currentTile.WorldPosition);
    }
    

    private IEnumerator InitPosition()
    {
        yield return new WaitForEndOfFrame();
        MovesLeft += 1;
        MoveTo(Utils.Finder.GridController.GetTile(initialPosition.x, initialPosition.y));
    }

    public void Attack(Character character)
    {
        MovesLeft -= 1;
        character.healthPoints -= 2;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public bool Died()
    {
        return healthPoints <= 0;
    }

}
