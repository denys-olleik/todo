﻿using FluentValidation.Results;

namespace ToDo.Models.Account
{
  public class LoginViewModel
  {
    public string? Email { get; set; }
    public string? Password { get; set; }
    public ValidationResult? ValidationResult { get; set; }
  }
}