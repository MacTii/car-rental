using Application.Mapper.DTOs;
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
        public IEnumerable<CarDTO> GetCars();
        public CarDTO GetCar(int carID);
        public void AddCar(CarDTO carDTO);
        public void UpdateCar(int carID, CarDTO carDTO);
        public void DeleteCar(int carID);
    }
}
