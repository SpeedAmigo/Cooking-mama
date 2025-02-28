using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class PipeSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameManagerScript _gameManagerScript;
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float yRange;
    
    private ObjectPool<PipeLogic> _pipePool;
    
    private Coroutine _spawnCoroutine; 
    private bool _active = true;

    private IEnumerator PipeSpawner()
    {
        while (_active)
        {
            var pipe = _pipePool.Get();
            pipe.transform.position = new Vector2(transform.position.x, RandomPipePosition());
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private float RandomPipePosition()
    {
        return Random.Range(-yRange, yRange);
    }

    private void StopSpawning()
    {
        _active = false;
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }

    #region ObjectPool
    private PipeLogic CreatePipe()
    {
        var pipeInstance = Instantiate(pipePrefab,new Vector2(transform.position.x, RandomPipePosition()), Quaternion.identity).GetComponent<PipeLogic>();
        pipeInstance.SetPool(_pipePool);
        pipeInstance.gameManager = _gameManagerScript; // adds reference to gameManager
        return pipeInstance;
    }

    private void GetPipe(PipeLogic pipe)
    {
        pipe.gameObject.SetActive(true);
    }

    private void ReleasePipe(PipeLogic pipe)
    {
        pipe.gameObject.SetActive(false);
    }

    private void DestroyPipe(PipeLogic pipe)
    {
        Destroy(pipe.gameObject);
    }
    #endregion
    void Awake()
    {
        _pipePool = new ObjectPool<PipeLogic>(CreatePipe, GetPipe, ReleasePipe, DestroyPipe, false, 2, 5);
    }
    void Start()
    {
       _spawnCoroutine = StartCoroutine(PipeSpawner());
    }
}
