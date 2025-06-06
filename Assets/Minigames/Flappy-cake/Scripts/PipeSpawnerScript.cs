using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
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

    public void StartSpawning()
    {
        _spawnCoroutine = StartCoroutine(PipeSpawner());
    }

    private void StopSpawning()
    {
        _active = false;
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }

    private void MoveToScene(GameObject obj, string sceneName)
    {
        Scene additiveScene = SceneManager.GetSceneByName(sceneName);

        if (additiveScene.IsValid())
        {
            SceneManager.MoveGameObjectToScene(obj, additiveScene);
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found");
        }
    }

    #region ObjectPool
    private PipeLogic CreatePipe()
    {
        var pipeInstance = Instantiate(pipePrefab,new Vector2(transform.position.x, RandomPipePosition()), Quaternion.identity).GetComponent<PipeLogic>();
        pipeInstance.SetPool(_pipePool);
        pipeInstance.gameManager = _gameManagerScript; // adds reference to gameManager
        
        MoveToScene(pipeInstance.gameObject, "FlappyCake");
        
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
}
