using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup() { }
    private Transform FindClosest() {  return transform; }
    private bool IsPossibleToAttack() {  return false; }

    private IEnumerator SearchTarget()
    {
        yield return null;
    }

    public bool Upgrade()
    {
        return true;
    }
    public void Sell() { }
}
