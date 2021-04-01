﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Api.Entities
{
    public class Car
    {
        public Car(int id, string vinCode, string brand, string model, int year, decimal price, string color, DateTime productionDate)
        {
            Id = id;
            VinCode = vinCode;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
            Color = color;
            ProductionDate = productionDate;

            Status = CarStatusEnum.Available;
            RegisteredAt = DateTime.Now;
        }

        public int Id { get; private set; }
        public string VinCode { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }
        public string Color { get; private set; }
        public DateTime ProductionDate { get; set; }
        public CarStatusEnum Status { get; set; }
        public DateTime RegisteredAt { get; set; }

        public void Update(string color, decimal price)
        {
            Color = color;
            Price = price;
        }

        public void SetAsSuspended()
        {
            Status = CarStatusEnum.Suspend;
        }
    }
}
