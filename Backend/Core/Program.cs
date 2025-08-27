using AutoMapper;
using BusinessObject.Data;
using BusinessObject.Models;
using BusinessObject.ViewModels.Account;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.OrderDetail;
using BusinessObject.ViewModels.Product;
using BusinessObject.ViewModels.ProductCategory;
using Core.GraphQL.Mutations;
using Core.GraphQL.Queries;
using Core.GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;
using Repository.Repositories;
using Services.Interfaces;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Register AutoMapper with runtime mapping (no profiles needed)
var mapperConfig = new MapperConfiguration(mc =>
{
    // AutoMapper will use convention-based mapping for properties with matching names
    mc.CreateMap<Order, OrderResponseWithDetails>();
    mc.CreateMap<OrderDetail, OrderDetailResponse>();
    mc.CreateMap<OrderResponseWithDetails, Order>();
    mc.CreateMap<OrderDetailResponse, OrderDetail>();
    mc.CreateMap<Product, ProductResponse>();
    mc.CreateMap<Account, AccountResponse>();
    mc.CreateMap<ProductCategory, ProductCategoryResponse>();
    mc.CreateMap<Product, ProductOrderResponse>();
    mc.CreateMap<Account, AccountOrderResponse>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Register data context
builder.Services.AddDbContext<FarmProductsApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IImportedStockRepository, ImportedStockRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImportedStockService, ImportedStockService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Add GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<OrderQueries>()
    .AddTypeExtension<ProductQueries>()
    .AddTypeExtension<AccountQueries>()
    .AddTypeExtension<CategoryQueries>()
    .AddTypeExtension<OrderMutations>()
    .AddType<Order>()
    .AddType<OrderDetail>()
    .AddType<Account>()
    .AddType<Product>()
    .AddType<ProductCategory>()
    .AddType<ImportedStock>()
    .AddType<PaginatedOrderType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map GraphQL endpoint
app.MapGraphQL("/graphql");

// Use CORS policy
app.UseCors("AllowAllOrigins");

app.Run();
