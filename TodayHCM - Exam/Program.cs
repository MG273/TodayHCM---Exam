var builder = WebApplication.CreateBuilder(args);

//add limiter for brute force
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

//add mime type for all controllers
builder.Services.AddControllers(options => {

    //responsbody mime type
    options.Filters.Add(new ProducesAttribute("application/json"));
    //requestbody mime type
    options.Filters.Add(new ConsumesAttribute("application/json")); 

    //Authorization policy
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddXmlSerializerFormatters();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("Custom-Header");
    logging.ResponseHeaders.Add("Custom-Header");
    logging.MediaTypeOptions.AddText("application/json");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

//add custom repository To IoC Container
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddScoped<IPersonsRepository, PersonRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionRepository>();

//add custom Service To IoC Container
builder.Services.AddScoped<IPersonAdderService, PersonAdderService>();
builder.Services.AddScoped<IProductAdderService, ProductAdderService>();
builder.Services.AddScoped<IProductGetterService, ProductGetterService>();
builder.Services.AddScoped<ITransactionAdderService, TransactionAdderService>();
builder.Services.AddScoped<ITransactionGetterService, TransactionGetterService>();


//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "TodayHCM Exam Web API", Version = "1.0" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                            Enter 'Bearer' [space] and then your token in the text input below.
                            Example: Bearer 12345abcdef",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
});

//CORS enabling
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//add DB Context
builder.Services.AddDbContext<TodayHcmExamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//add Identity DB Context
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
    .AddEntityFrameworkStores<TodayHcmExamContext>()
    .AddDefaultTokenProviders();

//jwt
ConfigureJwtAuthentication(builder.Services, builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHsts();
app.UseHttpsRedirection();
app.UseHttpLogging();
app.UseStaticFiles();

//creates endpoint for swagger.json
app.UseSwagger(); 
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TodayHCM - Exam 1.0");
    options.RoutePrefix = String.Empty;
    options.DisplayRequestDuration();
}); //creates swagger UI for testing all Web API endpoints / action methods

app.UseHttpLogging();
app.UseRouting();

// Enable CORS
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var jwtSettings = configuration.GetSection("Jwt");
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

    services.AddAuthorization();
}
