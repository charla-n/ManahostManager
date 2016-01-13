using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class ProductBookingService : AbstractService<ProductBooking, ProductBookingDTO>
    {
        private new IProductBookingRepository repo;

        public ProductBookingService(IProductBookingRepository repo, IValidation<ProductBooking> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(ProductBookingDTO dto, int id, Client currentClient)
        {
            orig = repo.GetProductBookingById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(ProductBookingDTO dto)
        {
            if (dto.Product != null)
                repo.includes.Add("Product");
            repo.includes.Add("Booking");
        }

        protected override void DeleteIncludeProps()
        {
            repo.includes.Add("Product");
        }

        protected override void SetDefaultValues(ProductBooking entity)
        {
            entity.PriceHT = entity.PriceHT ?? 0M;
            entity.PriceTTC = entity.PriceTTC ?? entity.PriceHT;
        }

        
        public IAbstractService<Booking, BookingDTO> BookingService { get { return GetService<IAbstractService<Booking, BookingDTO>>(); } private set { } }

        
        public IAbstractService<Product, ProductDTO> ProductService { get { return GetService<IAbstractService<Product, ProductDTO>>(); } private set { } }

        protected override ProductBooking DoPostPutDto(Client currentClient, ProductBookingDTO dto, ProductBooking entity, string path, object param)
        {
            if (entity == null)
                entity = new ProductBooking();
            else
            {
                if (dto.PriceHT == null)
                    dto.PriceHT = entity.PriceHT;
                if (dto.PriceTTC == null)
                    dto.PriceTTC = entity.PriceTTC;
            }
            GetMapper.Map(dto, entity);
            if (dto.Booking != null && dto.Booking.Id != 0)
                entity.Booking = BookingService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Booking, currentClient, path);
            if (dto.Product != null)
                entity.Product = ProductService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Product, currentClient, path);
            if (entity.Product != null && entity.Product.Duration == null)
            {
                if (entity.Product.Stock != null)
                {
                    entity.Product.Stock -= entity.Quantity;
                    SetThresholdFlag(entity);
                }
            }
            return entity;
        }

        public virtual ProductBookingDTO Compute(Client currentClient, int id, object param)
        {
            repo.includes.Add("Product");
            repo.includes.Add("Product.Tax");
            ProcessDTOPostPut(null, id, currentClient);
            ValidateOrig();
            ComputePriceAndDuration(orig);
            repo.Update(orig);
            repo.Save();
            return GetMapper.Map<ProductBooking, ProductBookingDTO>(orig);
        }

        private void SetThresholdFlag(ProductBooking entity)
        {
            if (entity.Product.Stock < 0)
                entity.Product.Stock = 0;
            if (entity.Product.Threshold != null && entity.Product.Stock < entity.Product.Threshold)
                entity.Product.IsUnderThreshold = true;
            else if (entity.Product.Threshold != null && entity.Product.Stock >= entity.Product.Threshold)
                entity.Product.IsUnderThreshold = false;
        }

        protected override void DoPut(Client currentClient, ProductBooking entity, object param)
        {
            if (entity.Product.Duration == null)
            {
                ProductBooking beforeSave = repo.GetProductBookingById(entity.Id, currentClient.Id);

                if (entity.Product.Stock != null)
                {
                    entity.Product.Stock += beforeSave.Quantity - entity.Quantity;
                    SetThresholdFlag(entity);
                }
            }
        }

        protected override void DoDelete(Client currentClient, int id, object param)
        {
            if (orig.Product.Duration == null)
            {
                if (orig.Product.Stock != null)
                {
                    orig.Product.Stock += orig.Quantity;
                    SetThresholdFlag(orig);
                    repo.Update<Product>(orig.Product);
                    repo.Save();
                }
            }
        }

        private void ComputePriceAndDuration(ProductBooking entity)
        {
            Product associatedProduct = entity.Product;
            Tax associatedTax = associatedProduct.Tax == null ? null : associatedProduct.Tax;

            entity.PriceHT = associatedProduct.PriceHT * entity.Quantity;
            if (associatedTax != null)
                entity.PriceTTC = ComputePrice.ComputePriceFromPercentOrAmount((decimal)entity.PriceHT, (EValueType)associatedTax.ValueType,
                    (EValueType)associatedTax.ValueType == EValueType.AMOUNT ? Decimal.Multiply((decimal)associatedTax.Price, (decimal)entity.Quantity) : (decimal)associatedTax.Price);
            else
                entity.PriceTTC = entity.PriceHT;
            if (associatedProduct.Duration != null)
                entity.Duration = entity.Quantity * associatedProduct.Duration;
        }
    }
}