using AutoMapper;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using E_Commerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
	public class BrandService : IBrandCategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<bool> AddBrandOrCategory(BrandTypeDTO entity)
		{
			var brand = _mapper.Map<ProductBrand>(entity);
			_unitOfWork.Repository<ProductBrand, int>().Update(brand);
			var state=await _unitOfWork.CompleteAsync();
			return state>0;
		}

		public async Task<bool>? DeleteBrandOrCategory(BrandTypeDTO brand)
		{
			var productBrand = await _unitOfWork.Repository<ProductBrand, int>().GetByIdAsync(brand.Id);
			if (productBrand == null)
			{
				return false;
			}
			_unitOfWork.Repository<ProductBrand, int>().Delete(_mapper.Map<ProductBrand>(productBrand));
			await _unitOfWork.CompleteAsync();
			return true;
		}

		public async Task<IEnumerable<BrandTypeDTO>>? GetAllBrandsOrCategoriesAsync()
		{
			var types = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
			if (types == null) return null!;

			return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandTypeDTO>>(types);
		}

		public async Task<BrandTypeDTO>? GetBrandOrCategoryByIdAsync(int Id)
		{
			var brand = await _unitOfWork.Repository<ProductBrand, int>().GetByIdAsync(Id);
			if (brand == null)
			{
				return null!;
			}

			return _mapper.Map<BrandTypeDTO>(brand);
		}

		public async Task<bool> UpdateBrandOrCategory(BrandTypeDTO brand)
		{
			var productBrand = await _unitOfWork.Repository<ProductBrand, int>().GetByIdAsync(brand.Id);
			if (productBrand == null)
			{
				return false;
			}
			productBrand.Name = brand.Name;
			 _unitOfWork.Repository<ProductBrand, int>().Update(productBrand);
			await _unitOfWork.CompleteAsync();
			return true;
		}
	}
}
