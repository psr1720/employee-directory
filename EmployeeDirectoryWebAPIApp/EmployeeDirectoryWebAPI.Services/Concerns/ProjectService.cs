using AutoMapper;
using EmployeeDirectory.Models;
using EmployeeDirectory.Repository.Contracts;
using EmployeeDirectory.Services.Contracts;
using System.Linq.Expressions;
using Entity = EmployeeDirectory.Models.Entity;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;

namespace EmployeeDirectory.Services.Concerns;

public class ProjectService: IProjectService
{
    private IProjectRepository _projectRepository;
    private readonly RequestContext _requestContext;
    private IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper, RequestContext requestContext) 
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _requestContext = requestContext;
    }

    public Pagination<List<ResponseDTO.Project>> GetAll(int page, int limit)
    {
        IQueryable<Entity.ProjectDetails> query = _projectRepository.BuildQuery();

        Pagination<List<Entity.ProjectDetails>> result = _projectRepository.GetAll(page, limit, query);
        List<ResponseDTO.Project> projectDTOs = _mapper.Map<List<ResponseDTO.Project>>(result.Data);

        Pagination<List<ResponseDTO.Project>> pagination = new Pagination<List<ResponseDTO.Project>>
        {
            Data = projectDTOs,
            TotalRecords = result.TotalRecords
        };
        return pagination;
    }

    public RequestDTO.Project Insert (RequestDTO.Project proj)
    {
        string creatorId = _requestContext.Id;

        Entity.Project project = _mapper.Map<Entity.Project>(proj);

        project.SetCreatedFields(creatorId);

        Entity.Project newProj = _projectRepository.Insert(project);
        RequestDTO.Project insertedProject = _mapper.Map<RequestDTO.Project>(newProj);
        return insertedProject;
    }

    public RequestDTO.Project Update(RequestDTO.Project proj)
    {
        string updatorId = _requestContext.Id;

        Entity.Project project = _mapper.Map<Entity.Project>(proj);

        project.SetUpdatedFields(updatorId);

        Entity.Project newProj = _projectRepository.Update(project);
        RequestDTO.Project updatedProject = _mapper.Map<RequestDTO.Project>(newProj);
        return updatedProject;
    }

    public bool Delete(int id)
    {
        Expression<Func<Entity.Project, bool>> expression = x => x.Id == id;
        return _projectRepository.Delete(expression);
    }
}
