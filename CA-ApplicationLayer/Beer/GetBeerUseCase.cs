using CL_EnterpriseLayer;

namespace CA_ApplicationLayer
{
    public class GetBeerUseCase<TEntity, TOutput>
    {

        private readonly IRepository<TEntity> _repository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetBeerUseCase(IRepository<TEntity> repository, IPresenter<TEntity, TOutput> presenter)
        {
            _repository = repository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var beers =  await _repository.GetAllAsync();

            return _presenter.Present(beers);
        }

    }
}
