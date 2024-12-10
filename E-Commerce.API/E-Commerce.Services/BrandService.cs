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
		private readonly IMedia _media;
		public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IMedia media)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_media = media;
		}

		public async Task<bool> AddBrandOrCategory(CreateBrandTypeDto entity)
		{
			var brand = new ProductBrand
			{
				Name = entity.Name,
				ImageUrl = _media.UploadFile(entity.image, "Brands")
			};
			_unitOfWork.Repository<ProductBrand, int>().Update(brand);
			var state = await _unitOfWork.CompleteAsync();
			return state > 0;
		}

		public async Task<bool> DeleteBrandOrCategory(int Id)
		{
			var productBrand = await _unitOfWork.Repository<ProductBrand, int>().GetByIdAsync(Id);
			if (productBrand == null)
			{
				return false;
			}
			_unitOfWork.Repository<ProductBrand, int>().Delete(_mapper.Map<ProductBrand>(productBrand));
			return await _unitOfWork.CompleteAsync() > 0;
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

		public async Task<bool> UpdateBrandOrCategory(int Id, CreateBrandTypeDto brand)
		{
			var productBrand = await _unitOfWork.Repository<ProductBrand, int>().GetByIdAsync(Id);
			if (productBrand == null)
			{
				return false;
			}

			if (productBrand.ImageUrl != null)
			{
				_media.DeleteFile("Brands", productBrand.ImageUrl.Split('/')[^1]);
			}

			productBrand.Name = brand.Name;
			productBrand.ImageUrl = _media.UploadFile(brand.image, "Brands");

			_unitOfWork.Repository<ProductBrand, int>().Update(productBrand);
			await _unitOfWork.CompleteAsync();
			return true;
		}
	}
}
