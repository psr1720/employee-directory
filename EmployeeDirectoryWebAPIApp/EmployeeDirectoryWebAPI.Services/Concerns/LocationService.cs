using AutoMapper;
using EmployeeDirectory.Repository.Contracts;
using EmployeeDirectory.Services.Contracts;
using System.Linq.Expressions;
using Entity = EmployeeDirectory.Models.Entity;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services.Concerns;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly RequestContext _requestContext;
    private readonly IMapper _mapper;

    public LocationService(ILocationRepository locationRepository, IMapper mapper, RequestContext requestContext)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
        _requestContext = requestContext;
    }

    public Pagination<List<ResponseDTO.Location>> GetAll(int page, int limit)
    {
        IQueryable<Entity.LocationDetails> query = _locationRepository.BuildQuery();

        Pagination<List<Entity.LocationDetails>> result = _locationRepository.GetAll(page, limit, query);
        List<ResponseDTO.Location> locationDTOs = _mapper.Map<List<ResponseDTO.Location>>(result.Data);

        Pagination<List<ResponseDTO.Location>> pagination = new Pagination<List<ResponseDTO.Location>>
        {
            Data = locationDTOs,
            TotalRecords = result.TotalRecords
        };
        return pagination;
    }

    public RequestDTO.Location Insert(RequestDTO.Location loc)
    {
        string creatorId = _requestContext.Id;

        Entity.Location newLoc = _mapper.Map<Entity.Location>(loc);

        newLoc.SetCreatedFields(creatorId);

        Entity.Location insertedLocation = _locationRepository.Insert(newLoc);
        RequestDTO.Location location = _mapper.Map<RequestDTO.Location>(insertedLocation);
        return location;
    } 

    public RequestDTO.Location Update(RequestDTO.Location loc)
    {
        string updatorId = _requestContext.Id;

        Entity.Location newLoc = _mapper.Map<Entity.Location>(loc);

        newLoc.SetUpdatedFields(updatorId);

        Entity.Location updatedLocation = _locationRepository.Update(newLoc);
        RequestDTO.Location location = _mapper.Map<RequestDTO.Location>(updatedLocation);
        return location;
    }

    public bool Delete(int id)
    {
        Expression<Func<Entity.Location, bool>> expression = x => x.Id == id;
        return _locationRepository.Delete(expression);
    }
}
