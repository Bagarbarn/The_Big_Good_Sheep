using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnReset : MonoBehaviour {

    public float tick_max;
    public float tick_min;

    public float time_maxDifficulty;

    public float wave_spawnChance;
    public int wave_minSize;
    public int wave_maxSize;

    public int customer_spawnRate;

    public int fauxCustomer_spawnChance;

    public int object_spawnRate;

    // Needs list of tsunami's and adding them
    // Needs list of obstacle course's and adding them

    public void ChangeVariables(SpawnManager spawn)
    {
        spawn.tick_max = this.tick_max;
        spawn.tick_rate = this.tick_max;
        spawn.tick_min = this.tick_min;

        spawn.time_maxDifficulty = this.time_maxDifficulty;

        spawn.wave_spawnChance = this.wave_spawnChance;
        spawn.wave_maxSize = this.wave_maxSize;
        spawn.wave_minSize = this.wave_minSize;

        spawn.customer_spawnRate = this.customer_spawnRate;
        spawn.fauxCustomer_spawnChance_normal = this.fauxCustomer_spawnChance;
        spawn.object_spawnRate = this.object_spawnRate;
    }
}
