using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

public abstract class Tile : MonoBehaviour
{
    private Button tileButton;
    private readonly TileType tileType;
    private Image tileImage;
    private Character linkedCharacter;
    private GridController gridController;
    private bool IsPossibleAction => tileImage.sprite != gridController.NormalSprite;
    private bool IsWalkable => tileType != TileType.OBSTACLE;
    private bool IsAvailable => IsWalkable && !IsOccupiedByACharacter;
    private bool IsOccupiedByACharacter => linkedCharacter != null;
    private Vector3 positionInGrid;
    public Vector3 WorldPosition => transform.position;

    protected Tile(TileType tileType)
    {
        this.tileType = tileType;
    }
    
    protected virtual void Awake()
    {
        tileButton = GetComponent<Button>();
        tileImage = GetComponent<Image>();
        tileButton.onClick.AddListener(OnCellClick); 
        gridController = transform.parent.GetComponent<GridController>();
    }

    protected void Start()
    {
        int index = transform.GetSiblingIndex();
        positionInGrid.x = index % Finder.GridController.NbColumns;
        positionInGrid.y = index / Finder.GridController.NbLines;
    }

    private void OnCellClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (IsOccupiedByACharacter)
        {
            if (!gridController.ACharacterIsCurrentlySelected)
            {
                if (linkedCharacter is Ally && linkedCharacter.CanPlay)
                {
                    gridController.SelectCharacter(linkedCharacter);
                    DisplayPossibleActions();
                }
            } 
            else
            {
                if (linkedCharacter is Enemy && gridController.SelectedCharacter.CanPlay)
                {
                    if (IsPossibleAction)
                    {
                        gridController.SelectedCharacter.Attack(linkedCharacter);
                        if (linkedCharacter.Died())
                        {
                            linkedCharacter.Die();
                            gridController.SelectedCharacter.MoveTo(this);
                        }
                    }
                    gridController.DeselectCharacter();
                }
                else if (linkedCharacter.IsCurrentlySelected)
                {
                    gridController.DeselectCharacter();
                }
                else
                {
                    gridController.SelectCharacter(linkedCharacter);
                    DisplayPossibleActions();
                }
            }
        }
        else
        {
            if (gridController.ACharacterIsCurrentlySelected && IsPossibleAction) gridController.SelectedCharacter.MoveTo(this);
            gridController.DeselectCharacter();
        }
    }

    private void DisplayPossibleActions()
    {
        tileImage.sprite = gridController.SelectedSprite;
        for (int i = -linkedCharacter.Range; i <= linkedCharacter.Range; i++)
        {
            for(int j = -linkedCharacter.Range; j <= linkedCharacter.Range ; j++)
            {
                if (i != 0 || j != 0)
                {
                    if (positionInGrid.x + i >= 0 && positionInGrid.y + j >= 0 && positionInGrid.x + i < gridController.NbColumns && positionInGrid.y + j < gridController.NbLines)
                    {
                        if (Math.Abs(i) + Math.Abs(j) <= linkedCharacter.Range)
                        {
                            Tile tile = gridController.GetTile((int)positionInGrid.x + i, j + (int)positionInGrid.y);
                            if (tile.IsAvailable)
                            {
                                tile.DisplayMoveActionPossibility();
                            }
                            else
                            {
                                if (tile.linkedCharacter is Enemy)
                                {
                                    if(Math.Abs(i) == 1 && Math.Abs(j) == 0 || Math.Abs(i) == 0 && Math.Abs(j) == 1) tile.DisplayAttackActionPossibility();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    

    private void DisplayMoveActionPossibility()
    {
        tileImage.sprite = gridController.AvailabilitySprite;
    }

    private void DisplayAttackActionPossibility()
    {
        tileImage.sprite = gridController.AttackableTileSprite;
    }
    
    public void HideActionPossibility()
    {
        tileImage.sprite = gridController.NormalSprite;
    }

    public bool LinkCharacter(Character character)
    {
        if (!IsWalkable) return false;
        this.linkedCharacter = character;
        return character != null;
    }

    public bool UnlinkCharacter()
    {
        if (linkedCharacter == null) return false;
        linkedCharacter = null;
        return true;
    }
}

public enum TileType 
{
    EMPTY = 0,
    OBSTACLE = 1,
    FOREST = 2,
    FORTRESS = 3,
    DOOR = 4
}

