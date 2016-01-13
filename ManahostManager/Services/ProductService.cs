using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class ProductService : AbstractService<Product, ProductDTO>
    {
        private new IProductRepository repo;

        public ProductService(IProductRepository repo, IValidation<Product> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(ProductDTO dto, int id, Client currentClient)
        {
            orig = repo.GetProductById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(ProductDTO dto)
        {
            if (dto.BillItemCategory != null)
                repo.includes.Add("BillItemCategory");
            if (dto.ProductCategory != null)
                repo.includes.Add("ProductCategory");
            if (dto.Supplier != null)
                repo.includes.Add("Supplier");
            if (dto.Tax != null)
                repo.includes.Add("Tax");
            if (dto.Documents != null)
                repo.includes.Add("Documents");
        }

        
        public IDocumentRepository DocumentRepository { get { return GetService<IDocumentRepository>(); } private set { } }

        
        public IHomeConfigRepository HomeConfigRepository { get { return GetService<IHomeConfigRepository>(); } private set { } }

        
        public IAbstractService<BillItemCategory, BillItemCategoryDTO> BillItemCategoryService { get { return GetService<IAbstractService<BillItemCategory, BillItemCategoryDTO>>(); } private set { } }
        
        public IAbstractService<ProductCategory, ProductCategoryDTO> ProductCategoryService { get { return GetService<IAbstractService<ProductCategory, ProductCategoryDTO>>(); } private set { } }
        
        public IAbstractService<Supplier, SupplierDTO> SupplierService { get { return GetService<IAbstractService<Supplier, SupplierDTO>>(); } private set { } }
        
        public IAbstractService<Tax, TaxDTO> TaxService { get { return GetService<IAbstractService<Tax, TaxDTO>>(); } private set { } }
        
        public IAdditionnalDocumentMethod DocumentService { get { return GetService<IAdditionnalDocumentMethod>(); } private set { } }

        protected override Product DoPostPutDto(Client currentClient, ProductDTO dto, Product entity, string path, object param)
        {
            if (entity == null)
                entity = new Product();
            else
            {
                if (dto.RefHide == null)
                    dto.RefHide = entity.RefHide;
            }
            GetMapper.Map<ProductDTO, Product>(dto, entity);
            if (dto.BillItemCategory != null)
                entity.BillItemCategory = BillItemCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.BillItemCategory, currentClient, path);
            if (dto.ProductCategory != null)
                entity.ProductCategory = ProductCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.ProductCategory, currentClient, path);
            if (dto.Supplier != null)
                entity.Supplier = SupplierService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Supplier, currentClient, path);
            if (dto.Tax != null)
                entity.Tax = TaxService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Tax, currentClient, path);
            if (dto.Documents != null)
            {
                DocumentRepository.DeleteRange(entity.Documents.Where(d => !dto.Documents.Any(x => x.Id == d.Id)));
                dto.Documents.ForEach(document =>
        {
            if (entity.Documents.Count != 0 && document.Id != 0 &&
            entity.Documents.Find(p => p.Id == document.Id) != null)
                return;
            Document toAdd = DocumentService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, document, currentClient, path);

            if (toAdd != null)
                entity.Documents.Add(toAdd);
        });
            }
            return entity;
        }

        protected override void SetDefaultValues(Product entity)
        {
            entity.RefHide = entity.RefHide ?? true;
            entity.IsUnderThreshold = false;
            if (entity.Threshold != null && entity.Stock != null)
            {
                if (entity.Stock < entity.Threshold)
                    entity.IsUnderThreshold = true;
            }
        }

        protected override void DoPost(Client currentClient, Product entity, object param)
        {
            HomeConfig config;

            HomeConfigRepository.includes.Add("DefaultBillItemCategoryProduct");
            config = HomeConfigRepository.GetHomeConfigById(entity.HomeId, currentClient.Id);
            if (config != null && config.DefaultBillItemCategoryProduct != null && entity.BillItemCategory == null)
                entity.BillItemCategory = config.DefaultBillItemCategoryProduct;
        }

        protected override void DoPut(Client currentClient, Product entity, object param)
        {
            if (entity.Stock < entity.Threshold)
                entity.IsUnderThreshold = true;
        }
    }
}