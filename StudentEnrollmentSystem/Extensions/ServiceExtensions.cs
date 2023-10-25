using FluentValidation;
using StudentEnrollmentSystem.DTO;
using StudentEnrollmentSystem.Interfaces;
using StudentEnrollmentSystem.Repository;
using StudentEnrollmentSystem.UOW;

namespace StudentEnrollmentSystem.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICourse, CourseRepository>();
            services.AddScoped<IValidator<SignUpDTO>, UserSignUpValidator>();
            services.AddScoped<IAccount, AccountRepository>();
            services.AddScoped<IAdmin, AdminRepository>();
            services.AddScoped<IStudent, StudentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
