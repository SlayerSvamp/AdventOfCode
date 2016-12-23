using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class OldSolutions
    {
        enum PoweringType { Plutonium, Promethium, Cobalt, Ruthenium, Curium }
        enum ItemType { Generator, Microchip }
        class Item
        {
            public PoweringType Powering { get; set; }
            public ItemType Type { get; set; }
            public bool IsPartnerOnFloor(Floor floor)
            {
                return floor.Items.Any(item => item.Type != Type && item.Powering == Powering);
            }
            public bool CanLeaveFloor(Floor floor)
            {
                if (Type == ItemType.Microchip)
                {
                    return true;
                }
                if (floor.Items.Any(item => item.Type == ItemType.Microchip && item.Powering == Powering))
                {
                    return false;
                }
                return true;
            }
            public bool CanMoveToFloor(Floor floor)
            {
                if (Type == ItemType.Microchip)
                {
                    if (floor.Items.Any(item => item.Type == ItemType.Generator && item.Powering == Powering))
                    {
                        return true;
                    }
                    else if (floor.Items.Any(item => item.Type == ItemType.Generator))
                    {
                        return false;
                    }
                    return true;
                }
                else //generator
                {
                    if (floor.Items.Any(item => item.Type == ItemType.Microchip && item.Powering != Powering))
                    {
                        if (floor.Items.Where(item => item.Type == ItemType.Microchip).Any(item => !item.IsPartnerOnFloor(floor) && item.Powering != Powering))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
        class Floor
        {
            public int FloorNumber { get; set; }
            public Floor OneFloorUp { get; set; }
            public Floor OneFloorDown { get; set; }
            public List<Item> Items { get; set; }
        }

        static List<Floor> CreateFloors()
        {
            var floors = new List<Floor>();
            var first = new Floor()
            {
                FloorNumber = 1,
                Items = new List<Item>() {
                    new Item() { Powering = PoweringType.Promethium, Type = ItemType.Generator},
                    new Item() { Powering = PoweringType.Promethium, Type = ItemType.Microchip}}
            };
            var second = new Floor()
            {
                FloorNumber = 2,
                Items = new List<Item>() {
                    new Item() { Powering = PoweringType.Ruthenium, Type = ItemType.Generator},
                    new Item() { Powering = PoweringType.Cobalt, Type = ItemType.Generator},
                    new Item() { Powering = PoweringType.Plutonium, Type = ItemType.Generator},
                    new Item() { Powering = PoweringType.Curium, Type = ItemType.Generator}}
            };
            var third = new Floor()
            {
                FloorNumber = 3,
                Items = new List<Item>() {
                    new Item() { Powering = PoweringType.Ruthenium, Type = ItemType.Microchip},
                    new Item() { Powering = PoweringType.Cobalt, Type = ItemType.Microchip},
                    new Item() { Powering = PoweringType.Plutonium, Type = ItemType.Microchip},
                    new Item() { Powering = PoweringType.Curium, Type = ItemType.Microchip}}
            };
            var fourth = new Floor() { FloorNumber = 4, Items = new List<Item>() };
            first.OneFloorUp = second;
            second.OneFloorDown = first;
            second.OneFloorUp = third;
            third.OneFloorDown = second;
            third.OneFloorUp = fourth;
            fourth.OneFloorDown = third;
            floors.AddRange(new Floor[] { first, second, third, fourth });
            return floors;
        }
    }
}
