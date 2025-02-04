using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    [Range(0f, 300f)]
    public float snapDistance;
    public List<BookScript> books = new();
    public List<Transform> bookTargets = new();
    public GameObject booksParent;
    public GameObject booksSlots;
    [SerializeField] public List<GameObject> heldBook;
    
    private readonly Dictionary<BookScript, Transform> _bookTargetDict = new();

    public void SwapBooks(GameObject book1, GameObject book2)
    {
        BookScript script1 = book1.GetComponent<BookScript>();
        BookScript script2 = book2.GetComponent<BookScript>();
        
        Vector3 book1Pos = book1.transform.position;
        Vector3 book2Pos = book2.transform.position;
        
        book1.transform.position = book2Pos;
        book2.transform.position = book1Pos;
        
        script1.SetDragging(false);
        script2.SetDragging(true);
        
        heldBook.Clear();
        heldBook.Add(book2);
        
        script1.TrySnapToTarget();
        script2.TrySnapToTarget();
        
        Debug.Log(script1.name + " | " + script2.name);
    }
    
    private void BooksStartup()
    {
        for (int i = 0; i < books.Count; i++)
        {
            _bookTargetDict.Add(books[i], bookTargets[i]);
            books[i].ManagerReference(this);
            books[i].SetSnapTarget(_bookTargetDict[books[i]]);
        }
    }

    private void BooksPlacement()
    {
        List<Transform> avaliblePositions = new(bookTargets); // clones targets list 
        
        foreach (BookScript book in books)
        {
            if (avaliblePositions.Count == 0)
            {
                Debug.LogWarning("Not enough targets to place book");
                return;
            }
            
            int index = Random.Range(0, avaliblePositions.Count);
            book.gameObject.transform.position = avaliblePositions[index].position;
            avaliblePositions.RemoveAt(index);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        BooksStartup();
        BooksPlacement();
    }
}
