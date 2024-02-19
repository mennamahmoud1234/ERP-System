using AutoMapper;
using ERP.Core.Dtos;
using ERP.Core.Entities;
using ERP.Core.Identity;

namespace ERP.APIs.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductDto>().ForMember(d => d.Category, o => o.MapFrom(s => s.SubCategory.SubCategoryName));
            CreateMap<ParentCategory, CategoryToReturnDto>().ForMember(c => c.ParentCategoryId, pc => pc.MapFrom(pc => pc.Id)).ReverseMap();
            CreateMap<SubCategory, SubCategoryToReturnDto>().ForMember(c => c.SubCategoryId, pc => pc.MapFrom(pc => pc.Id)).ReverseMap();
            CreateMap<SubCategory, ParentCategoryDto>().ReverseMap();
            CreateMap<Supplier, SupplierDto>().ForMember(a => a.AddedBy, o => o.MapFrom(u => u.Employee.UserName));
            CreateMap<SupplierDto, Supplier>();

            CreateMap<Replenishment, ReplenishmentProductStockOutDto>()
                       .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                       .ForMember(dest => dest.ProductBarcode, opt => opt.MapFrom(src => src.Product.ProductBarcode))
                       .ForMember(dest => dest.ProductOnHand, opt => opt.MapFrom(src => src.Product.ProductOnHand))
                       .ForMember(dest => dest.ProductSellPrice, opt => opt.MapFrom(src => src.Product.ProductSellPrice))
                       .ReverseMap();

            CreateMap<InventoryOrder, OrderDto>()
                                                 .ForMember(dest => dest.AccEmployeeId, opt => opt.MapFrom(src => src.AccEmployee))
                                               .ReverseMap();




            CreateMap<Product, ProductDetailsDto>().ForMember(d => d.Category, o => o.MapFrom(s => s.SubCategory.SubCategoryName)).ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.UserName)).ReverseMap();

            CreateMap<Product, ProductReturnDto>().ReverseMap();
            //CreateMap<InventoryOrderProduct,OrderStatusDto> ().ForMember(d => d.Status, o => o.MapFrom(s => s.InventoryOrder.Status)).ReverseMap();

            //CreateMap<Transfer, TransfersReturnDto>().ForMember(d => d.TransferProducts, o => o.Ignore());
            CreateMap<Transfer, TransfersReturnDto>()
            .PreserveReferences()
            .ReverseMap();

            CreateMap<TransferProduct, TransferProductDto>().ReverseMap();
            CreateMap<ParentCategory, ParentCategoryDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
            CreateMap<InventoryOrder, InventoryOrderDto>().ReverseMap();
            CreateMap<InventoryOrder, InventoryOrderDetailsDto>().ForMember(d => d.InventoryEmployee, o => o.MapFrom(s => s.InventoryEmp.UserName))
                                                                 .ForMember(d => d.AccEmployee, o => o.MapFrom(s => s.AccEmp.UserName))
                                                                 .ForMember(d => d.Product, o => o.MapFrom(s => s.Product.ProductName))
                                                                 .ReverseMap();


            CreateMap<ScmOrderProduct, ScmOrderDto>().ForMember(d => d.Reference, o => o.MapFrom(s => s.ScmOrder.Reference))
                                                     .ForMember(d => d.AccEmployeeId, o => o.MapFrom(s => s.ScmOrder.AccEmployeeId))
                                                     .ReverseMap();



            CreateMap<ScmOrder, ScmOrderToReturnDto>().ReverseMap();

            CreateMap<ScmOrderProduct, ScmOrderProductsReturnDto>()
                                                          .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName));


            CreateMap<ScmOrder, OrderStatusDto>()
                                            .ForMember(d => d.OrderId, o => o.MapFrom(s => s.Id))
                                             .ReverseMap();



            CreateMap<InventoryOrder, InventoryOrderToReturnDto>().ForMember(d => d.InventoryEmployee, o => o.MapFrom(s => s.InventoryEmp.UserName))
                                      
                                                                .ForMember(d => d.Product, o => o.MapFrom(s => s.Product.ProductName))
                                                                .ReverseMap();
            CreateMap<TaxDto, Tax>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.UserName))
                                            .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.SupplierName))
                                            .ForMember(d => d.Tax, o => o.MapFrom(s => s.Tax.TaxName))
                                            .PreserveReferences()
                                            .ReverseMap();
  


            CreateMap<Invoice,CreateInvoiceDto>().ReverseMap();
            CreateMap<Invoice, CreateInvoiceToReturnDto>().ReverseMap();

            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Payment, PaymentReturnDto>().ReverseMap();
            CreateMap<Payment, PaymentDetailsReturnDto>().ForMember(d => d.Employee, o => o.MapFrom(s => s.Employee.UserName))
                                            .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.SupplierName))
                                            .ReverseMap();

            CreateMap<Department ,DepartmentToReturnDto>().ForMember(d => d.ParentDepartmentName, o => o.MapFrom(s => s.ParentDepartment.DepartmentName)).ReverseMap();

            CreateMap<Employee, EmployeeData>().ReverseMap();

            CreateMap<JobPosition, JobPositionData>().ReverseMap();

            CreateMap<Department, DepartmentData>().ReverseMap();

            CreateMap<Department, DepartmentDto>().ReverseMap();
      
                 CreateMap<Department, CreatedDepartmentReturnDto>().ReverseMap();

        }
    }
}
