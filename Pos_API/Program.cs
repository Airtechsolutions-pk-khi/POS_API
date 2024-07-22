using DataAccess.Data.IDataModel;
using DataAccess.Data.DataModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Pos_API.Middleware;
using DataAccess.Services.Service;
using DataAccess.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region JWT Implementation
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey
		(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = false,
		ValidateIssuerSigningKey = true
	};
});
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Caching
builder.Services.AddMemoryCache();
#endregion

#region Cors
builder.Services.AddCors();
#endregion

#region Services
builder.Services.AddScoped<IGenericCrudService, GenericCrudService>();
builder.Services.AddScoped<IAuthData, AuthData>();
builder.Services.AddScoped<ICategoryData, CategoryData>();
builder.Services.AddScoped<ISubCategoryData, SubCategoryData>();
builder.Services.AddScoped<IItemData, ItemData>();
builder.Services.AddScoped<IOrderData, OrderData>();
builder.Services.AddScoped<IWaiterData, WaiterData>();
builder.Services.AddScoped<ITablesData, TablesData>();
builder.Services.AddScoped<ICustomerData, CustomerData>();
builder.Services.AddScoped<IReportData, ReportData>();
builder.Services.AddScoped<IRefundOrderData, RefundOrderData>();
builder.Services.AddScoped<IAdminData, AdminData>();
#endregion

var app = builder.Build();

#region Swagger for Development only
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
#endregion
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region Swagger for Production and Development
app.UseSwagger();

//app.useswaggerui(c =>
//{
//	c.swaggerendpoint("/swagger/v1/swagger.json", "api documentation");
//	c.routeprefix = string.empty; // serve the swagger ui at the root url
//});

app.UseSwaggerUI();
#endregion

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<JWTMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
