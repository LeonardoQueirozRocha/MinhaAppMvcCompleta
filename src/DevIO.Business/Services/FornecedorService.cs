using DevIO.Business.Interfaces.Services;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        public async Task AddAsync(Fornecedor fornecedor)
        {
            if (!Validate(new FornecedorValidator(), fornecedor) &&
                !Validate(new EnderecoValidator(), fornecedor.Endereco)) return;

            return;
        }

        public async Task UpdateAsync(Fornecedor fornecedor)
        {
            if (!Validate(new FornecedorValidator(), fornecedor)) return;
        }

        public async Task UpdateEnderecoAsync(Endereco endereco)
        {
            if (!Validate(new EnderecoValidator(), endereco)) return;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
