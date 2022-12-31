using AutoMapper;
using DevIO.App.Controllers.Base;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces.Repository;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(
            IFornecedorRepository fornecedorRepository,
            IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.GetAllAsync()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await GetFornecedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            return View(fornecedorViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            await _fornecedorRepository.AddAsync(_mapper.Map<Fornecedor>(fornecedorViewModel));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await GetFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            return View(fornecedorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            await _fornecedorRepository.UpdateAsync(_mapper.Map<Fornecedor>(fornecedorViewModel));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await GetFornecedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            return View(fornecedorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await GetFornecedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            await _fornecedorRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        private async Task<FornecedorViewModel> GetFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.GetFornecedorEnderecoAsync(id));
        }

        private async Task<FornecedorViewModel> GetFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.GetFornecedorProdutosEnderecoAsync(id));
        }
    }
}
