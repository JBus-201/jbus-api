using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IPointRepository
    {
        void Update(PointDto point);
        IEnumerable<PointDto> GetPoints();
        PointDto GetPointById(int id);
        Point CreatePoint(PointCreateDto pointDto);
        bool SaveChanges();
    }
}