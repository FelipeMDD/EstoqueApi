﻿using EstoqueApi.Models;
using EstoqueApi.Repositories;
using MediatR;

public class LogErrorMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly LogErroRepository _logErroRepository;

    public LogErrorMiddleware(LogErroRepository logErroRepository)
    {
        _logErroRepository = logErroRepository;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var error = new LogErro
            {
                Texto = ex.Message,
                EndPoint = ex.StackTrace
            };

            await _logErroRepository.InsereLogErroAsync(error);
            throw; 
        }
    }
}