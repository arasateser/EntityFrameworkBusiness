using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        //ILogger _logger;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //[SecuredOperation("product.add,admin")]
        //[ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.GetAll")]
        public IResult Add(Product product)
        {
            var result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId)
                                      , CheckIfProductNameExists(product.ProductName)
                                      , CheckIfCategoryCountIsLimitExceded(product.CategoryId));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {

            if (DateTime.Now.Hour >= 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            if (DateTime.Now.Hour >= 17)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour >= 17)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var categoryCount = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            int maxPerCategoryCount = 10;

            if (categoryCount >= maxPerCategoryCount)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var categoryCount = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            int maxPerCategoryCount = 10;

            if (categoryCount >= maxPerCategoryCount)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult("ARAS ATEŞER");
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var newProductNameCount = _productDal.GetAll(p => p.ProductName == productName).Any();

            if (newProductNameCount)
            {
                return new ErrorResult(Messages.ProductNameExistsError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryCountIsLimitExceded(int categoryId)
        {
            var resut = _categoryService.GetAll();
            if (resut.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice < 100)
            {
                throw new Exception("AddTransactionalTestException");
            }
            Add(product);
            return null;
        }
    }
}