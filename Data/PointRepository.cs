using System.Drawing;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class PointRepository : IPointRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PointRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public PointDto GetPointById(int id)
        {
            return _context
            .Points
            .Where(p => p.Id == id)
            .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
            .SingleOrDefault();
        }

        public IEnumerable<PointDto> GetPoints()
        {
            return _context
                .Points
                .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(PointDto point)
        {
            _context.Entry(point).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}