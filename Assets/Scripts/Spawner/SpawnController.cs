﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SupremumStudio
{
	public class SpawnController : MonoBehaviour
	{
		public Spawner Spawner;

		private PoolObject enemyPool;
		private GameObject _modelBuilding;
//		[SerializeField] private float rate;
		private int curent;

		public int Curent
		{
			get { return curent; }
			set { curent = value; }
		}

		private void Awake()
		{
			enemyPool = new PoolObject(Spawner.EnemyPrefab,Spawner.CountEnemy,new GameObject("Enemies"));
			_modelBuilding = Instantiate(Spawner.ModelPrefab, transform.position, Quaternion.identity);
			_modelBuilding.transform.SetParent(transform);	

		}


		IEnumerator CreateEnemy()
		{
			
			for (int i = curent; i < Spawner.CountEnemy; i++)
			{
				enemyPool.GetPoolObject();
				curent++;
				yield return new WaitForSeconds(Spawner.RateInstantce);
			}
			Debug.Log("End Coroutine");
		}

		public void StartWave()
		{
			StartCoroutine(CreateEnemy());
		}

		private void OnEnable()
		{
			Time.timeScale = 1;
			StartWave();
			_modelBuilding.SetActive(true);
		}

		private void OnDisable()
		{
			Time.timeScale = 0;
			_modelBuilding.SetActive(false);			
		}
		
	}
}