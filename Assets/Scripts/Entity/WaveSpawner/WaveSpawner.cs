using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventSystemNS;

namespace EntityNS {
    public class WaveSpawner : MonoBehaviour {
        public Wave[] Waves;

        public Wave CurrentWave {
            get {
                if (_waveIndex >= Waves.Length) {
                    return null;
                }
                return Waves[_waveIndex];
            }
        }
        public Wave NextWave {
            get {
                if (_waveIndex + 1 >= Waves.Length) {
                    return null;
                }
                return Waves[_waveIndex + 1];
            }
        }
        private int _waveIndex;

        private HashSet<Entity> _spawnedEntities = new HashSet<Entity>();

        private void Awake() {
            EventManager.AddListener(EntityDeathEvent.NAME, OnEntityDeath);
            EventManager.AddListener(PlayerTriggerEnterEvent.NAME, OnPlayerTrigger);

            Wave current = CurrentWave;
            if (current != null && current.Condition.onWaveDone) {
                SpawnWave();
            }
        }

        public void SpawnWave() {
            SpawnWave(CurrentWave);
        }

        public void SpawnWave(Wave wave) {
            EntitySpawn[] entities = wave.Entities;
            for (int i = 0; i < entities.Length; i++) {
                EntitySpawn spawn = entities[i];
                Entity entity = Instantiate(
                    spawn.entity,
                    spawn.transform.position,
                    Quaternion.identity,
                    this.gameObject.transform
                );
                _spawnedEntities.Add(entity);
            }
        }

        private void OnEntityDeath(System.EventArgs args) {
            EntityDeathEvent evt = args as EntityDeathEvent;
            if (_spawnedEntities.Contains(evt.Entity)) {
                _spawnedEntities.Remove(evt.Entity);
                if (_spawnedEntities.Count == 0) {
                    Wave next = NextWave;
                    if (next != null && next.Condition.onWaveDone) {
                        _waveIndex++;
                        SpawnWave();
                    }
                }
            }
        }

        private void OnPlayerTrigger(System.EventArgs args) {
            Wave next = NextWave;
            if (next == null) {
                return;
            }

            PlayerTriggerEnterEvent evt = args as PlayerTriggerEnterEvent;
            if (evt.Trigger == next.Condition.trigger) {
                _waveIndex++;
                SpawnWave();
            }
        }
    }
}
