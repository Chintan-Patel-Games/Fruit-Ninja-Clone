using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(EdgeCollider2D))]
public class Blade : MonoBehaviour
{
    [SerializeField] private float minSliceVelocity = 0.1f; // Minimum velocity to create slice effect
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    private List<Vector2> mousePositions;

    private bool isSlicing;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        mousePositions = new List<Vector2>();

        // Initially disable the line and collider until slicing starts
        lineRenderer.enabled = false;
        edgeCollider.enabled = false;
    }

    private void Update()
    {
        // Detect if the player is pressing and moving the mouse to slice
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (isSlicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        isSlicing = true;
        mousePositions.Clear();
        lineRenderer.enabled = true;
        edgeCollider.enabled = true;
        UpdateSlicing();
    }

    private void StopSlicing()
    {
        isSlicing = false;
        lineRenderer.enabled = false;
        edgeCollider.enabled = false;
    }

    private void ContinueSlicing()
    {
        // Only add new points if the mouse moved enough
        if (Vector3.Distance(mousePositions[mousePositions.Count - 1], Camera.main.ScreenToWorldPoint(Input.mousePosition)) > minSliceVelocity)
        {
            UpdateSlicing();
        }
    }

    private void UpdateSlicing()
    {
        // Convert mouse position to world position and add it to the line renderer and edge collider
        Vector2 newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositions.Add(newMousePosition);

        lineRenderer.positionCount = mousePositions.Count;
        lineRenderer.SetPosition(mousePositions.Count - 1, newMousePosition);

        // Update the edge collider with the latest mouse positions
        if (mousePositions.Count > 1)
        {
            edgeCollider.SetPoints(mousePositions);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit") || other.CompareTag("Bomb"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null)
            {
                fruit.Slice(); // Call the slicing method on the fruit
            }
        }
    }
}