using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace gm_codex.Presentation.DependencyInjection;

public sealed class TypeRegistrar : ITypeRegistrar, IDisposable
{
    private readonly IServiceCollection _builder;

    public TypeRegistrar(IServiceCollection builder)
    {
        _builder = builder;
    }

    public ITypeResolver Build()
    {
        var provider = _builder.BuildServiceProvider();
        return new TypeResolver(provider);
    }

    public void Register(Type service, Type implementation)
    {
        _builder.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        _builder.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        _builder.AddSingleton(service, _ => factory());
    }

    public void Dispose()
    {
    }

    private sealed class TypeResolver : ITypeResolver
    {
        private readonly IServiceProvider _provider;
        public TypeResolver(IServiceProvider provider) => _provider = provider;

        public object? Resolve(Type? type)
        {
            return type is null ? null : _provider.GetService(type);
        }

        [UsedImplicitly]
        public void Dispose()
        {
            if (_provider is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
