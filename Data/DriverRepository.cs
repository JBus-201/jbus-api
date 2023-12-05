using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DriverRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // TODO register Driver
        public bool CreateDrive(DriverCreateDto driverDto)
        {
            throw new NotImplementedException();
        }

        public Driver GetDriverById(int id)
        {
            return _context
            .Drivers
            .Where(d => d.Id == id)
            .SingleOrDefault()!;
        }

        public DriverDto GetDriverDtoById(int id)
        {
            return _context
           .Drivers
           .Where(d => d.Id == id)
           .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
           .SingleOrDefault()!;
        }

        public IEnumerable<DriverDto> GetDrivers()
        {
            return _context
           .Drivers
           .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
           .ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(DriverDto driver)
        {
            _context.Entry(driver).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}