﻿using E_commerceWebApi.Application.Common.Handler;

namespace E_commerceWebApi.Application.Shared.Validations
{
    public interface IPasswordValidation
    {
        Task<Result<string>> Passwordvalidate();
    }
}
