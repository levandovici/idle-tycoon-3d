using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Balance
{
    public static Price NewTerrain = new Price(gold: 5);

    public static BuildingBalance Factory = new BuildingBalance(new Price(planks: 10, bricks: 20),
        new BuildingLevel(new Price(planks: 15, bricks: 25), new Price(gold: 10), 2f), new BuildingLevel(new Price(planks: 25, bricks: 40), new Price(gold: 20), 2f),
        new BuildingLevel(new Price(planks: 50, bricks: 100), new Price(gold: 30), 2f), new BuildingLevel(new Price(planks: 100, bricks: 200), new Price(gold: 40), 2f),
        new BuildingLevel(new Price(), new Price(gold: 50), 2f)); //gold (planks & bricks)

    public static BuildingBalance House = new BuildingBalance(new Price(bricks: 15),
        new BuildingLevel(new Price(bricks: 25), new Price(money: 100), 1f), new BuildingLevel(new Price(bricks: 50), new Price(money: 250), 1f),
        new BuildingLevel(new Price(bricks: 100), new Price(money: 500), 1f), new BuildingLevel(new Price(bricks: 200), new Price(money: 1000), 1f),
        new BuildingLevel(new Price(), new Price(money: 2000), 1f)); //money (bricks)

    public static BuildingBalance Production = new BuildingBalance(new Price(money: 200),
        new BuildingLevel(new Price(money: 500), new Price(planks: 5, bricks: 5), 5f), new BuildingLevel(new Price(money: 1000), new Price(planks: 5, bricks: 5), 5f),
        new BuildingLevel(new Price(money: 2500), new Price(planks: 5, bricks: 5), 5f), new BuildingLevel(new Price(money: 5000), new Price(planks: 5, bricks: 5), 5f),
        new BuildingLevel(new Price(), new Price(planks: 5, bricks: 5), 5f)); //planks & bricks (money)

    public static BuildingBalance Warehouse = new BuildingBalance(new Price(planks: 5, money: 100),
        new BuildingLevel(new Price(planks: 15, money: 250), new Price(containers: 3), 4f), new BuildingLevel(new Price(planks: 25, money: 500), new Price(containers: 3), 4f),
        new BuildingLevel(new Price(planks: 50, money: 1000), new Price(containers: 3), 4f), new BuildingLevel(new Price(planks: 100, money: 2000), new Price(containers: 3), 4f),
        new BuildingLevel(new Price(), new Price(containers: 3), 4f)); //containers (money & planks)
}
