using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    [SerializeField] private List<BookScript> books = new();
    [SerializeField] private List<Transform> targets = new();
    [SerializeField] private float moveSpeed = 1f;
    
    [SerializeField] private List<BookScript> targetOrder = new();

    private void Start()
    {
        for (int i = 0; i < books.Count; i++)
        {
            var bookScript = books[i];
            bookScript.shelfManager = this;
            targetOrder.Add(bookScript);
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

    public bool IsCorrectOrder()
    {
        for (int i = 0; i < books.Count; i++)
        {
            if (books[i] != targetOrder[i]) return false;
        }
        Debug.Log("CorrectOrder!");
        return true;
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
