﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PieceSelector
{
    private Camera _mainCamera;
    private InputReader _inputReader;
    private const string PieceTag = "Piece";

    public event Action<Piece> OnPieceSelected;

    public PieceSelector(InputReader inputReader)
    {
        _mainCamera = Camera.main;
        _inputReader = inputReader;
        SubscribeToInputEvents();
    }

    private void SubscribeToInputEvents()
    {
        _inputReader.GameplayInput.GamePlay.Select.performed += OnSelectPerformed;
        _inputReader.GameplayInput.Enable();
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        Piece selectedPiece = GetClickedPiece();
        if (selectedPiece != null)
        {
            OnPieceSelected?.Invoke(selectedPiece);
        }
    }

    private Piece GetClickedPiece()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(PieceTag))
            {
                return hit.collider.GetComponent<Piece>();
            }
        }

        return null;
    }
}