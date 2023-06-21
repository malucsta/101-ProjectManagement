using HotChocolate.Execution.Configuration;
using ProjectManagement.API.GraphQL.Queries;

namespace ProjectManagement.API.GraphQL;

public static class GraphQLConfiguration
{
    public static IServiceCollection ConfigureGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueries();

        return services;
    }

    private static IRequestExecutorBuilder AddQueries(this IRequestExecutorBuilder builder)
    {
        builder
            .AddQueryType<Instructions>()
            .AddQueryType<PersonQueries>();

        return builder;
    }
}
