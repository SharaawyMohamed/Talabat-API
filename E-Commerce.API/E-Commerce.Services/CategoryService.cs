using AutoMapper;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;


namespace E_Commerce.Services
{
	public class CategoryService : IBrandCategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IMedia _media;
		public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IMedia media)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_media = media;
		}
		public async Task<IEnumerable<BrandTypeDTO>>? GetAllBrandsOrCategoriesAsync()
		{
			var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
			if (types == null) return null!;

			return _mapper.Map<IReadOnlyList<BrandTypeDTO>>(types);
		}
		public async Task<BrandTypeDTO>? GetBrandOrCategoryByIdAsync(int Id)
		{
			var category = await _unitOfWork.Repository<ProductType, int>().GetByIdAsync(Id);
			if (category == null) return null!;

			return _mapper.Map<BrandTypeDTO>(category);
		}
		public async Task<bool> DeleteBrandOrCategory(int Id)
		{
			var category = await _unitOfWork.Repository<ProductType, int>().GetByIdAsync(Id);
			if (category != null)
			{
				_unitOfWork.Repository<ProductType, int>().Delete(category);
				var deleted = await _unitOfWork.CompleteAsync();
				return deleted > 0;
			}
			return false;
		}
		public async Task<bool> AddBrandOrCategory(CreateBrandTypeDto entity)
		{
			var category = new ProductType {
				Name = entity.Name,
				ImageUrl = _media.UploadFile(entity.image,"Categories")
			};
			await _unitOfWork.Repository<ProductType, int>().AddAsync(_mapper.Map<ProductType>(entity));
			var state = await _unitOfWork.CompleteAsync();
			return state > 0;
		}
		public async Task<bool> UpdateBrandOrCategory(int Id,CreateBrandTypeDto category)
		{
			var productType = await _unitOfWork.Repository<ProductType, int>().GetByIdAsync(Id);
			if (productType == null) return false;

			if (productType.ImageUrl != null)
			{
				_media.DeleteFile("Categories", productType.ImageUrl.Split('/')[^1]);
			}

			productType.Name = category.Name;
			productType.ImageUrl = _media.UploadFile(category.image, "Categories");
			await _unitOfWork.CompleteAsync();
			return true;
		}

	}
}
