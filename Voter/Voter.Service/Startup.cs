using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Voter.Core.Abstractions.Services;
using Voter.Core.Services;
using Voter.Core.Services.Pipeline;
using Voter.Core.Validators;
using Voter.Dal;
using Voter.Dto.Requests.Poll;
using Voter.Dto.Requests.Question;
using Voter.Dto.Requests.User;
using Voter.Dto.Requests.Variant;
using Voter.Service.Authorization;
using Voter.Service.Mappings;

namespace Voter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAutoMapper(x => x.AddProfile<PollMappingProfile>());

            services.AddMediatR(Assembly.GetAssembly(typeof(RequestLoggerPipelineBehavior<AddQuestionRequest, Unit>)));

            RegisterPollPipeline(services);
            RegisterQuestionPipeline(services);
            RegisterVariantPipeline(services);
            RegisterUserPipeline(services);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggerPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidatorPipelineBehavior<,>));

            services.AddDbContext<VoteDbContext>(options => options.UseMySql(Configuration.GetConnectionString("VoteDatabase")));

            services.AddScoped<IUserService, UserService>();

            services.AddRazorPages();

            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");
            app.UseStaticFiles();

            app.UseSession();
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("UserToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Main}/{id?}");
            });
        }

        private void RegisterPollPipeline(IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<GetPollListRequest>, GetPollListRequestValidator>();
            services.AddTransient<AbstractValidator<GetPollRequest>, GetPollRequestValidator>();
            services.AddTransient<AbstractValidator<AddPollRequest>, AddPollRequestValidator>();
            services.AddTransient<AbstractValidator<UpdatePollRequest>, UpdatePollRequestValidator>();
            services.AddTransient<AbstractValidator<DeletePollRequest>, DeletePollRequestValidator>();

            services.AddTransient<AbstractValidator<SurveyRequest>, SurveyRequestValidator>();
            services.AddTransient<AbstractValidator<GetStatsRequest>, GetStatsRequestValidator>();
        }

        private void RegisterQuestionPipeline(IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<GetQuestionRequest>, GetQuestionRequestValidator>();
            services.AddTransient<AbstractValidator<AddQuestionRequest>, AddQuestionRequestValidator>();
            services.AddTransient<AbstractValidator<UpdateQuestionRequest>, UpdateQuestionRequestValidator>();
            services.AddTransient<AbstractValidator<DeleteQuestionRequest>, DeleteQuestionRequestValidator>();
        }

        private void RegisterVariantPipeline(IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<AddVariantRequest>, AddVariantRequestValidator>();
            services.AddTransient<AbstractValidator<UpdateVariantRequest>, UpdateVariantRequestValidator>();
            services.AddTransient<AbstractValidator<DeleteVariantRequest>, DeleteVariantRequestValidator>();
        }

        private void RegisterUserPipeline(IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<SignInRequest>, SignInRequestValidator>();
            services.AddTransient<AbstractValidator<SignUpRequest>, SignUpRequestValidator>();
        }
    }
}
