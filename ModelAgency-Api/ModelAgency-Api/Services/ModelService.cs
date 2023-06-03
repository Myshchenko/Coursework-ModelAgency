using ModelAgency_Api.Models;
using ModelAgency_Api.Repositories;

namespace ModelAgency_Api.Services
{
    public interface IModelService
    {
        Task<List<Model>> GetModels();
    }

    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }


        public async Task<List<Model>> GetModels()
        {
            return await _modelRepository.GetModels();
        }
    }
}
