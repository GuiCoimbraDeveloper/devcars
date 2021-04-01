using DevCars.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.Api.Persistence
{
    public class DevCarsDbContext
    {
        public DevCarsDbContext()
        {
            Cars = new List<Car> {
                new Car(1,"123abc","HONDA","CIVIC",2021,100000,"CINZA",new DateTime(2021,1,1)),
                new Car(2,"456abc","TOYOTA","COROLA",2021,95000,"AZUL",new DateTime(2021,1,1)),
                new Car(3,"789abc","chevrolet","onix",2021,85000,"Branco",new DateTime(2021,2,1)),
                new Car(3,"789abc","HYUNDAI","HB20",2021,85000,"Branco",new DateTime(2021,2,1)),
            };
            Customers = new List<Customer>
            {
                new Customer(1,"LUCIANO","1234567",new DateTime(1990,1,1)),
                new Customer(2,"GUSTAVO","7654345",new DateTime(1990,1,1)),
                new Customer(3,"GABRIEL","9876345",new DateTime(1990,1,1)),
            };
        }
        public List<Car> Cars { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
