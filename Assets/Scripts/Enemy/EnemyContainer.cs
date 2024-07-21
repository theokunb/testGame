using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy container", menuName = "Enemy container", order = 51)]
public class EnemyContainer : ScriptableObject
{
    [SerializeField] private List<EnemyProbability> _probabilities;

    public BaseEnemy GetRandomEnemy()
    {
        var rand = UnityEngine.Random.Range(0, 100);
        var currentProbability = 0;

        foreach (var element in _probabilities)
        {
            if (rand >= currentProbability && rand < currentProbability + element.probability)
            {
                return element.prefab;
            }

            currentProbability += element.probability;
        }

        return null;
    }
}

[Serializable]
public class EnemyProbability
{
    public BaseEnemy prefab;
    public int probability;
}
