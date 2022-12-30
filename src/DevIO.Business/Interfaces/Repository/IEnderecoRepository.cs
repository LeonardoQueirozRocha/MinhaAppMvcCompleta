using DevIO.Business.Interfaces.Repository.Base;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces.Repository
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> GetEnderecoByFornecedorAsync(Guid fornecedorId);
    }
}
