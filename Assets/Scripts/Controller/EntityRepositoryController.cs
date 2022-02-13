using System.Collections.Generic;
using UnityEngine;

public class EntityRepositoryController : MonoBehaviour
{
    private List<EntityController> _entities;

    public List<EntityController> Entities
    {
        get { return _entities; }
    }
    
    void Start()
    {
        _entities = new List<EntityController>();
    }
}
