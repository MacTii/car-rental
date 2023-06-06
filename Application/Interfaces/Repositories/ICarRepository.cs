using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ICarRepository
    {
        public IEnumerable<Car> GetAll();
        public Car GetByID(int carID);
        public void Insert(Car user);
        public void Update(int carID, Car car);
        public void Delete(int carID);
        public void Save();
    }
}
