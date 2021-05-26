using System;
using Integration.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Demo.DDD.ApplicationServices.Commands;
using Demo.DDD.ApplicationServices.EventHandlers;
using Demo.DDD.Domain;
using Demo.DDD.Domain.Events;

namespace Demo.DDD.ApplicationServices
{
    public static class ConfigureServicesExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<UpdatePhoneNumberCommand, Unit>, UpdatePhoneNumberCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, Guid>, CreateUserCommandHandler>();
            services.AddScoped<INotificationHandler<UserCreatedEvent>, UserCreatedEventHandler>();
            services.AddScoped<INotificationHandler<PhoneNumberUpdatedEvent>, PhoneNumberUpdatedEventHandler>();
        }
    }
}