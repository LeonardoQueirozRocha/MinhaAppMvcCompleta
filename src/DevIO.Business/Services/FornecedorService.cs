using DevIO.Business.Interfaces.Notifications;
using DevIO.Business.Interfaces.Repository;
using DevIO.Business.Interfaces.Services;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using DevIO.Business.Services.Base;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(
            IFornecedorRepository fornecedorRepository,
            IEnderecoRepository enderecoRepository,
            INotifier notifier) : base(notifier)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<IEnumerable<Fornecedor>> GetAllAsync()
        {
            return await _fornecedorRepository.GetAllAsync();
        }

        public async Task<Fornecedor> GetFornecedorEnderecoAsync(Guid id)
        {
            return await _fornecedorRepository.GetFornecedorEnderecoAsync(id);
        }

        public async Task<Fornecedor> GetFornecedorProdutosEnderecoAsync(Guid id)
        {
            return await _fornecedorRepository.GetFornecedorProdutosEnderecoAsync(id);
        }

        public async Task AddAsync(Fornecedor fornecedor)
        {
            if (!Validate(new FornecedorValidator(), fornecedor) ||
                !Validate(new EnderecoValidator(), fornecedor.Endereco)) return;

            if (_fornecedorRepository.SearchAsync(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _fornecedorRepository.AddAsync(fornecedor);
        }

        public async Task UpdateAsync(Fornecedor fornecedor)
        {
            if (!Validate(new FornecedorValidator(), fornecedor)) return;

            if (_fornecedorRepository.SearchAsync(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _fornecedorRepository.UpdateAsync(fornecedor);
        }

        public async Task UpdateEnderecoAsync(Endereco endereco)
        {
            if (!Validate(new EnderecoValidator(), endereco)) return;

            await _enderecoRepository.UpdateAsync(endereco);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (_fornecedorRepository.GetFornecedorProdutosEnderecoAsync(id).Result.Produtos.Any())
            {
                Notify("O fornecedor possui produtos cadastrados!");
                return;
            }

            await _fornecedorRepository.DeleteAsync(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
