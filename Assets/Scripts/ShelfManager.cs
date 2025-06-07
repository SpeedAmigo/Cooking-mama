using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShelfManager : MonoBehaviour
{
    [SerializeField] private List<BookScript> books = new();
    [SerializeField] private List<Transform> targets = new();
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private UnityEvent checkForComplete;
    
    private readonly List<BookScript> _targetOrder = new();
    [HideInInspector] public bool completed;
    [HideInInspector] public Camera minigameCamera;

    private void Start()
    {
        for (int i = 0; i < books.Count; i++)
        {
            var bookScript = books[i];
            bookScript.shelfManager = this;
            _targetOrder.Add(bookScript);
        }
        
        Shuffle(books);
        SetBooksOnPositions(books);
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    private void SetBooksOnPositions(List<BookScript> books)
    {
        for (int i = 0; i < books.Count; i++)
        {
            BookScript book = books[i];
            Transform target = targets[i];
            
            book.transform.position = target.position;
        }
    }

    public void IsCorrectOrder()
    {
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i] != _targetOrder[i])
            {
                completed = false;
                return;
            }
        }
        completed = true;
        checkForComplete?.Invoke();
    }
    
    private void Update()
    {
        for (int i = 0; i < books.Count; i++)
        {
            BookScript book = books[i];
            Transform target = targets[i];

            if (!book.GetComponent<BookScript>().isDragging)
            {
                Vector3 position = book.gameObject.transform.position;
                book.gameObject.transform.position = Vector3.Lerp(position, target.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void UpdateBookOrder()
    {
        books.Sort((a,b) => a.gameObject.transform.position.x.CompareTo(b.gameObject.transform.position.x));
    }
}
