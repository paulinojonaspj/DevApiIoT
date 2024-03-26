namespace IOTBack.Model.Consumo
{
    public interface IConsumo
    {
        Task<bool> Add(Consumo entidade);
        Task<bool> Remover(Consumo entidade);
        Task<bool> Alterar(Consumo entidade);

        //Tudo que retorna recomenda-se utilizar Task async
        Task<IEnumerable<Consumo>> GetAll();

        Task<Consumo?> Get(int id);

        Task<IEnumerable<Consumo>> GetPaginacao(int pageNumber, int pageQuantity);
    }
}
