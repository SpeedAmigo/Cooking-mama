using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    [SerializeField] private List<Transform> books = new();
    [SerializeField] private List<Transform> targets = new();
    [SerializeField] private float moveSpeed = 1f;

    private void Start()
    {
        for (int i = 0; i < books.Count; i++)
        {
            var bookScript = books[i].GetComponent<BookScript>();
            bookScript.shelfManager = this;
        }
    }
    
    private void Update()
    {
        for (int i = 0; i < books.Count; i++)
        {
            Transform book = books[i];
            Transform target = targets[i];

            if (!book.GetComponent<BookScript>().isDragging)
            {
                book.position = Vector3.Lerp(book.position, target.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void UpdateBookOrder()
    {
        books.Sort((a,b) => a.position.x.CompareTo(b.position.x));
    }
}
