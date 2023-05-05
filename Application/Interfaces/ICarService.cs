using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICarService
    {
        public IEnumerable<Car> GetCars();
        public Car GetCar(int carID);
        public void AddCar(Car car);
        public void UpdateCar(int carID, Car car);
        public void DeleteCar(int carID);
    }
}
