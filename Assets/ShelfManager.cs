using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    [Range(0f, 100f)]
    public float snapDistance;
    public List<BookScript> books = new();
    public List<Transform> bookTargets = new();
    public GameObject booksParent;
    public GameObject booksSlots;
    [SerializeField] public List<GameObject> heldBook;
    
    private readonly Dictionary<BookScript, Transform> _bookTargetDict = new();
    
    private readonly Vector3 _offset = new(0f, 1f, 0f);

    public void GrabBook(GameObject book)
    {
        heldBook.Add(book);
    }

    private void ReleaseBook()
    {
        if (heldBook.Count == 0) return;
        
        BookScript bookScript = heldBook[0].GetComponent<BookScript>();
        TrySnapToTarget(bookScript);
        
        heldBook.RemoveAt(0);
    }

    private void BookHandler()
    {
        if (heldBook.Count == 0) return;
        
        heldBook[0].transform.position = Input.mousePosition + _offset;
    }

    private void TrySnapToTarget(BookScript book)
    {
        if (GetDistance(book) > snapDistance) return;
        
        book.transform.position = _bookTargetDict[book].position;
        book.SetNewParent(_bookTargetDict[book]);
        
        if (_bookTargetDict[book].childCount <= 1) return;
        
        SwapBooks(_bookTargetDict[book].GetChild(0).gameObject, book.gameObject);
    }
    
    private float GetDistance(BookScript book)
    {
        return Vector3.Distance(book.transform.position, _bookTargetDict[book].position);
    }
    
    private void SwapBooks(GameObject bookIndex0, GameObject bookIndex1)
    {
        BookScript script0 = bookIndex0.GetComponent<BookScript>();
        BookScript script1 = bookIndex1.GetComponent<BookScript>();
        
        GrabBook(bookIndex0);
        
        script0.SetNewParent(script1.GetPreviousParent());
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
            book.SetNewParent(avaliblePositions[index]);
            avaliblePositions.RemoveAt(index);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        BooksStartup();
        BooksPlacement();
    }
    
    void Update()
    {
        BookHandler();
        
        if (Input.GetMouseButtonDown(0))
        {
            ReleaseBook();
        }
    }
}
