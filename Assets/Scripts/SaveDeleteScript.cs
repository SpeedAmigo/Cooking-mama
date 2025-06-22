using UnityEngine;

public class SaveDeleteScript : MonoBehaviour
{
    public void DeleteSave()
    {
        ES3.DeleteDirectory(Application.persistentDataPath);
    }
}
