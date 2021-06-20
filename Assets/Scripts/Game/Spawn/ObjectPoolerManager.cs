using System.Collections.Generic;
using UnityEngine;

namespace Game.Spawn
{
    /// <summary>
    /// Вложенный класс, для наполения списка элементов пула для разогрева, с указанием колличества единиц разогрева.
    /// Пул хранит в себе только не активные обьекты. Если обьект запрошен из пула, он выдается и больше в пуле не находится.
    /// Обьект можно вернуть в пул.
    /// </summary>
    [System.Serializable]
    public class ObjectPoolItem
    {
        [Tooltip("Префаб объекта, который нужно запулить")]
        public GameObject m_pooledObject;

        [Tooltip("Количество объектов, которые будут созданы")]
        public int m_pooledAmount;
    }

    /// <summary>
    /// Пул для обьектов.
    /// </summary>
    public class ObjectPoolerManager : MonoBehaviour
    {
        [Tooltip("Обьекты этого списка нужны только для разогрева пула. Обращение к пулу по префабу")] 
        [SerializeField]
        protected List<ObjectPoolItem> m_poolPrewarmItems;

        private Dictionary<string, Queue<GameObject>> m_pools = new Dictionary<string, Queue<GameObject>>();

        /// <summary>
        /// Родительский элемент для обьектов в пуле
        /// </summary>
        public Transform GetParent { get; set; }

        private void Start()
        {
            GetParent = transform;

            PrewarmPool();
        }

        // Разогрев пула
        protected void PrewarmPool()
        {
            foreach (ObjectPoolItem item in m_poolPrewarmItems)
            {
                CreateElementInPull(item.m_pooledObject, item.m_pooledAmount);
            }
        }

        private void CreateElementInPull(GameObject gObject, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject obj = CreateNewObject(gObject);
                ReturnObject(obj);
            }
        }

        protected GameObject CreateNewObject(GameObject gObject)
        {
            GameObject obj = Instantiate(gObject, GetParent);
            obj.SetActive(false);
            obj.name = gObject.name;

            return obj;
        }

        /// <summary>
        /// Получить обьект из пула по префабу обьекта. Если такой обьект (с таким же именем) Окажется в пуле, он будет получен, или создастся новый.
        /// Обьект помещается в указанного парента и активируется.
        /// Обьект необходимо будет вернуть в пул (ReturnObject), вместо удаления, или отключения.
        /// </summary>
        /// <param name="gObject">Префаб обьекта</param>
        /// <param name="parentForObject">Трансформ родителя для созданого обьекта</param>
        /// <param name="activate">fasle, если обьект нужен неактивированным</param>
        /// <returns></returns>
        public virtual GameObject GetObject(GameObject gObject, Transform parentForObject, bool activate = true)
        {
            GameObject resultObject;

            if (m_pools.TryGetValue(gObject.name, out Queue<GameObject> objectList))
            {
                resultObject = objectList.Count == 0 ? CreateNewObject(gObject) : objectList.Dequeue();
            }
            else
            {
                resultObject = CreateNewObject(gObject);
            }

            resultObject.transform.SetParent(parentForObject);

            if (activate)
            {
                resultObject.SetActive(true);
            }

            return resultObject;
        }

        /// <summary>
        /// Возврат обьекта в пул, для дальнейшего переиспользования
        /// </summary>
        /// <param name="gObject"></param>
        /// <param name="returnRoutine"> параметр не используется </param>
        public virtual void ReturnObject(GameObject gObject)
        {
            if (m_pools.TryGetValue(gObject.name, out Queue<GameObject> objectList))
            {
                objectList.Enqueue(gObject);
            }
            else
            {
                Queue<GameObject> newQueue = new Queue<GameObject>();
                newQueue.Enqueue(gObject);
                m_pools.Add(gObject.name, newQueue);
            }

            gObject.SetActive(false);
            gObject.transform.SetParent(GetParent);
        }

        /// <summary>
        /// Не безопасно! Вернет обьект из пула по имени. Обьект сначала ищется в списке на разогрев. Если обьект не найден, вернет null
        /// </summary>
        /// <param name="objectName">имя префаба</param>
        /// <returns></returns>
        public GameObject GetObject(string objectName, Transform parentForObject, bool activate = true)
        {
            // найти обьект в списке "на разогрев" по имени
            for (int i = 0; i < m_poolPrewarmItems.Count; i++)
            {
                if (m_poolPrewarmItems[i].m_pooledObject.name == objectName)
                {
                    return GetObject(m_poolPrewarmItems[i].m_pooledObject, parentForObject, activate);
                }
            }

            // иначе возвращать нечего
            return null;
        }
    }
}