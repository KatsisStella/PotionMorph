using PotionMorph.Map;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Map
{
    internal class GlassDispenser : MonoBehaviour, IMachine
    {
        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private GameObject _cupPrefab;

        private void Awake()
        {
            SpawnCup();
        }

        public void Unregister(IProp prop)
        {
            StartCoroutine(WaitAndRespawn());
        }

        private IEnumerator WaitAndRespawn()
        {
            yield return new WaitForSeconds(1f);
            SpawnCup();
        }

        private void SpawnCup()
        {
            var go = Instantiate(_cupPrefab, _spawnPoint.transform.position, Quaternion.identity);
            var prop = go.GetComponent<IProp>();
            prop.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            prop.AssociatedMachine = this;
        }
    }
}
